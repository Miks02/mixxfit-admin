using System;
using System.Collections.Generic;
using System.Text;
using Mixxfit.Admin.Api;
using Mixxfit.Admin.Common;
using Mixxfit.Admin.Features.Auth.Login;

namespace Mixxfit.Admin.Features.Auth
{
    public class AuthService(MixxFitApiClient apiClient)
    {
        public bool IsAdmin { get; private set; }

        public async Task<ProblemDetails?> LoginAsync(LoginRequest request)
        {
            var (data, problem) = await apiClient.SendAsync<AuthResponse>(HttpMethod.Post, "auth/login", request, allowRefresh: false);
            if (problem is not null) return problem;

            apiClient.SetAccessToken(data!.AccessToken);
            IsAdmin = data.User?.Roles?.Any(r => r.Equals("Admin", StringComparison.OrdinalIgnoreCase)) ?? false;
            return null;
        }

        public async Task<bool> TryRefreshAsync()
        {
            var (data, problem) = await apiClient.SendAsync<AuthResponse>(
                HttpMethod.Post, "auth/refresh-token", null, allowRefresh: false);

            if (problem is not null)
            {
                apiClient.SetAccessToken(null);
                IsAdmin = false;
                return false;
            }

            apiClient.SetAccessToken(data!.AccessToken);
            return true;
        }

        public async Task LogoutAsync()
        {
            await apiClient.SendAsync(HttpMethod.Post, "auth/logout", null, allowRefresh: false);
            apiClient.SetAccessToken(null);
            IsAdmin = false;
        }
    }
}
