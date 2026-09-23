using Mixxfit.Admin.Api;
using Mixxfit.Admin.Common;
using Mixxfit.Admin.Features.Auth;
using Mixxfit.Admin.Features.Dashboard;

namespace Mixxfit.Admin
{
    public partial class Main : Form
    {
        private readonly AuthService _authService;
        private readonly DashboardService _dashboard;

        public bool SwitchedEnvironment { get; private set; }

        public Main(AuthService authService)
        {
            _authService = authService;
            _dashboard = new DashboardService(MixxFitApiClient.Instance);
            InitializeComponent();
            UpdateEnvironmentButton();
        }

        private void UpdateEnvironmentButton() => btnEnvironment.Text = ApiEnvironment.CurrentName;

        private async void btnEnvironment_Click(object? sender, EventArgs e)
        {
            var answer = MessageBox.Show(
                $"Do you want to switch to {ApiEnvironment.OtherName} ({(ApiEnvironment.IsProduction ? "local" : "production")} API)?\n\nYou will be logged out.",
                "Switch environment", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer != DialogResult.Yes) return;

            UseWaitCursor = true;
            btnEnvironment.Enabled = false;
            btnLogout.Enabled = false;
            try
            {
                await _authService.LogoutAsync();
                MixxFitApiClient.Instance.SwitchBaseUrl(ApiEnvironment.Toggle());
            }
            finally
            {
                UseWaitCursor = false;
            }

            SwitchedEnvironment = true;
            Close();
        }

        private async void Main_Shown(object? sender, EventArgs e)
        {
            var view = new DashboardView();
            view.Init(_dashboard);

            ShowView(view);
            await view.LoadDataAsync();
            await view.LoadPagedUsers();
        }

        private async void btnLogout_Click(object? sender, EventArgs e)
        {
            UseWaitCursor = true;
            btnLogout.Enabled = false;
            try
            {
                await _authService.LogoutAsync();
            }
            finally
            {
                UseWaitCursor = false;
            }

            Close();
        }

        private void ShowView(UserControl view)
        {
            pnlContent.Controls.Clear();
            view.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(view);
        }
    }
}
