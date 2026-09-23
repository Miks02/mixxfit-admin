using System.Text.RegularExpressions;
using Mixxfit.Admin.Common;
using Mixxfit.Admin.Common.Errors;

namespace Mixxfit.Admin.Features.Dashboard
{
    public partial class DashboardView : UserControl
    {
        private const string None = "—";

        private DashboardService _dashboard = null!;

        public DashboardView()
        {
            InitializeComponent();
        }

        public void Init(DashboardService dashboard)
        {
            _dashboard = dashboard;
        }

        public async Task LoadDataAsync()
        {
            UseWaitCursor = true;
            try
            {
                var (data, problem) = await _dashboard.GetAdminDashboardAsync();

                if (problem is not null)
                {
                    MessageBox.Show(ErrorCatalog.Describe(problem), "Error");
                    return;
                }

                ShowStats(data!);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        public async Task LoadPagedUsers()
        {
            UseWaitCursor = true;

            try
            {
                var (data, problem) = await _dashboard.GetAdminDashboardUsersAsync();

                if (problem is not null)
                {
                    MessageBox.Show(ErrorCatalog.Describe(problem), "Error");
                    return;
                }

                RefillDataGrid(data!.Items);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ShowStats(AdminDashboardResponse stats)
        {
            //cardUsers.Value = stats.Users.TotalCount.ToString("N0");
            cardWorkouts.Value = stats.TotalWorkouts.ToString("N0");
            cardExercises.Value = stats.TotalExercises.ToString("N0");
            cardWeightEntries.Value = stats.TotalWeightEntries.ToString("N0");
            cardMostCommonType.Value = stats.MostCommonExerciseType is { } type
                ? Regex.Replace(type.ToString(), "(?<=[a-z])(?=[A-Z])", " ")
                : None;
            cardAverageAge.Value = stats.AverageUserAge?.ToString("F1") ?? None;
        }

        private void RefillDataGrid(List<AdminDashboardUserDto> users)
        {
            dgUsers.Rows.Clear();
            dgUsers.DataSource = users;
        }
    }
}
