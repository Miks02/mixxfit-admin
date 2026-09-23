using Microsoft.Extensions.Configuration;
using Mixxfit.Admin.Api;
using Mixxfit.Admin.Common;
using Mixxfit.Admin.Features.Auth;

namespace Mixxfit.Admin
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var config = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.Development.json", optional: true)
                .Build();

            ApiEnvironment.Initialize(config);
            MixxFitApiClient.Initialize(ApiEnvironment.CurrentUrl);

            var auth = new AuthService(MixxFitApiClient.Instance);
            MixxFitApiClient.Instance.RefreshCallback = auth.TryRefreshAsync;

            bool switchedEnvironment;
            do
            {
                using (var login = new LoginForm(auth))
                {
                    if (login.ShowDialog() != DialogResult.OK) return;
                }

                var main = new Main(auth);
                Application.Run(main);
                switchedEnvironment = main.SwitchedEnvironment;
            } while (switchedEnvironment);
        }
    }
}