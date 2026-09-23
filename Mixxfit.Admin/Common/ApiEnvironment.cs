using Microsoft.Extensions.Configuration;

namespace Mixxfit.Admin.Common
{
    public static class ApiEnvironment
    {
        public const string ProductionUrl = "https://api.getmixxfit.com/api/";

        private static string _localUrl = null!;

        public static string CurrentUrl { get; private set; } = null!;

        public static bool IsProduction => CurrentUrl == ProductionUrl;

        public static string CurrentName => IsProduction ? "Production" : "Local";

        public static string OtherName => IsProduction ? "Local" : "Production";

        public static void Initialize(IConfiguration config)
        {
            _localUrl = Normalize(config["Api:BaseUrl"]!);
            CurrentUrl = _localUrl;
        }

        public static string Toggle()
        {
            CurrentUrl = IsProduction ? _localUrl : ProductionUrl;
            return CurrentUrl;
        }

        private static string Normalize(string url) => url.EndsWith('/') ? url : url + '/';
    }
}
