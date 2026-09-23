using Mixxfit.Admin.Api;
using Mixxfit.Admin.Features.Auth;
using Mixxfit.Admin.Features.Dashboard;

namespace Mixxfit.Admin
{
    public partial class Main : Form
    {
        private readonly AuthService _authService;
        private readonly DashboardService _dashboard;

        public Main(AuthService authService)
        {
            _authService = authService;
            _dashboard = new DashboardService(MixxFitApiClient.Instance);
            InitializeComponent();
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
