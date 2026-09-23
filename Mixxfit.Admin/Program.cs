using Microsoft.Extensions.Configuration;
using Mixxfit.Admin.Api;
using Mixxfit.Admin.Features.Auth;

namespace Mixxfit.Admin
{
    internal static class Program
    {

        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            MixxFitApiClient.Initialize(config["Api:BaseUrl"]!);

            var auth = new AuthService(MixxFitApiClient.Instance);
            MixxFitApiClient.Instance.RefreshCallback = auth.TryRefreshAsync; 

            using (var login = new LoginForm(auth))
            {
                if (login.ShowDialog() != DialogResult.OK) return;
            }

            Application.Run(new Main(auth));
        }
    }
}