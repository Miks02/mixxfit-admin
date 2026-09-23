using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Mixxfit.Admin.Common;

namespace Mixxfit.Admin.Api
{
    public sealed class MixxFitApiClient : IDisposable
    {
        private static MixxFitApiClient? _instance;

        public static MixxFitApiClient Instance => _instance
            ?? throw new InvalidOperationException("Call MixxFitApiClient.Initialize() first in Program.cs.");

        public static void Initialize(string baseUrl) => _instance ??= new MixxFitApiClient(baseUrl);

        internal static readonly JsonSerializerOptions JsonOptions =
            new(JsonSerializerDefaults.Web) { Converters = { new JsonStringEnumConverter() } };

        private readonly HttpClient _http;
        private string? _accessToken;

        public Func<Task<bool>>? RefreshCallback { get; set; }

        private MixxFitApiClient(string baseUrl)
        {
            if (!baseUrl.EndsWith('/')) baseUrl += '/';

            _http = new HttpClient(new HttpClientHandler { CookieContainer = new CookieContainer() })
            {
                BaseAddress = new Uri(baseUrl),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        public void SetAccessToken(string? token)
        {
            _accessToken = token;
            _http.DefaultRequestHeaders.Authorization =
                token is null ? null : new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<(T? Data, ProblemDetails? Problem)> SendAsync<T>(
            HttpMethod method, string url, object? body = null, bool allowRefresh = true)
        {
            var (response, problem) = await SendCoreAsync(method, url, body, allowRefresh);
            if (problem is not null) return (default, problem);

            using (response)
                return (await response!.Content.ReadFromJsonAsync<T>(JsonOptions), null);
        }

        public async Task<ProblemDetails?> SendAsync(
            HttpMethod method, string url, object? body = null, bool allowRefresh = true)
        {
            var (response, problem) = await SendCoreAsync(method, url, body, allowRefresh);
            response?.Dispose();
            return problem;
        }

        private async Task<(HttpResponseMessage? Response, ProblemDetails? Problem)> SendCoreAsync(
            HttpMethod method, string url, object? body, bool allowRefresh)
        {
            HttpResponseMessage response;

            try
            {
                response = await SendOnceAsync(method, url, body);
            }
            catch (HttpRequestException)
            {
                return (null, new ProblemDetails { Status = 503, Detail = "API is unavailable at the moment." });
            }
            catch (TaskCanceledException)
            {
                return (null, new ProblemDetails { Status = 408, Detail = "Request timed out." });
            }

            if (response.StatusCode == HttpStatusCode.Unauthorized
                && allowRefresh
                && _accessToken is not null
                && RefreshCallback is not null)
            {
                response.Dispose();

                if (await RefreshCallback())
                    return await SendCoreAsync(method, url, body, allowRefresh: false);

                return (null, new ProblemDetails
                {
                    Status = 401,
                    ErrorCode = "Auth.SessionExpired",
                    Detail = "Session has expired."
                });
            }

            if (response.IsSuccessStatusCode)
                return (response, null);

            var problem = await ReadProblemAsync(response);
            var status = (int)response.StatusCode;
            response.Dispose();

            return (null, problem ?? new ProblemDetails { Status = status });
        }

        private Task<HttpResponseMessage> SendOnceAsync(HttpMethod method, string url, object? body)
        {
            var request = new HttpRequestMessage(method, url.TrimStart('/'));

            if (body is not null)
                request.Content = JsonContent.Create(body, body.GetType(), options: JsonOptions);

            return _http.SendAsync(request);
        }

        private static async Task<ProblemDetails?> ReadProblemAsync(HttpResponseMessage response)
        {
            try { return await response.Content.ReadFromJsonAsync<ProblemDetails>(JsonOptions); }
            catch { return null; }
        }

        public void Dispose() => _http.Dispose();
    }
}
