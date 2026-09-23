using System.Text.RegularExpressions;
using Mixxfit.Admin.Common;
using Mixxfit.Admin.Common.Enums;
using Mixxfit.Admin.Common.Errors;

namespace Mixxfit.Admin.Features.Dashboard
{
    public partial class DashboardView : UserControl
    {
        private const string None = "—";

        private static readonly string[] SortKeys = ["newest", "oldest", "name", "email"];

        private static readonly Color Emerald600 = Color.FromArgb(22, 163, 74);
        private static readonly Color Amber600 = Color.FromArgb(217, 119, 6);

        private DashboardService _dashboard = null!;
        private int _loadTicket;
        private bool _loadedDeleted;

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
            var ticket = ++_loadTicket;
            var search = tbSearch.Text.Trim();
            var sort = SortKeys[Math.Max(cbSort.SelectedIndex, 0)];
            var isDeleted = chkDeleted.Checked;

            UseWaitCursor = true;

            try
            {
                var (data, problem) = await _dashboard.GetAdminDashboardUsersAsync(
                    search: search, sort: sort, isDeleted: isDeleted);

                if (ticket != _loadTicket) return;

                if (problem is not null)
                {
                    MessageBox.Show(ErrorCatalog.Describe(problem), "Error");
                    return;
                }

                _loadedDeleted = isDeleted;
                RefillDataGrid(data!.Items);
            }
            finally
            {
                UseWaitCursor = false;
            }
        }

        private void ShowStats(AdminDashboardResponse stats)
        {
            cardUsers.Value = stats.TotalUsers.ToString("N0");
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
            dgUsers.DataSource = users;

            if (users.Count == 0)
                HideUserOptions();
        }

        private void tbSearch_TextChanged(object? sender, EventArgs e)
        {
            tmrSearch.Stop();
            tmrSearch.Start();
        }

        private async void tmrSearch_Tick(object? sender, EventArgs e)
        {
            tmrSearch.Stop();
            await LoadPagedUsers();
        }

        private async void cbSort_SelectedIndexChanged(object? sender, EventArgs e)
        {
            await LoadPagedUsers();
        }

        private async void chkDeleted_CheckedChanged(object? sender, EventArgs e)
        {
            await LoadPagedUsers();
        }

        private void dgUsers_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != colStatus.Index || e.Value is not AccountStatus status)
                return;

            e.CellStyle!.ForeColor = status == AccountStatus.Active ? Emerald600 : Amber600;
        }

        private void dgUsers_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgUsers.SelectedRows.Count == 0)
            {
                HideUserOptions();
                return;
            }

            if (dgUsers.SelectedRows[0].DataBoundItem is not AdminDashboardUserDto selectedUser)
            {
                MessageBox.Show("Select a user first.");
                return;
            }

            ShowUserOptions(selectedUser);
        }

        private void ShowUserOptions(AdminDashboardUserDto user)
        {
            var isDeleted = user.DeletedAt is not null || _loadedDeleted;

            lblName.Text = user.FullName;
            lblEmail.Text = user.Email;
            lblCreated.Text = user.CreatedAt.ToLocalTime().ToString("dd MMM yyyy, HH:mm");

            lblDeletedCaption.Visible = isDeleted;
            lblDeleted.Visible = isDeleted;
            lblDeleted.Text = user.DeletedAt?.ToLocalTime().ToString("dd MMM yyyy, HH:mm") ?? None;

            btnActivate.Enabled = !isDeleted && user.AccountStatus == AccountStatus.Suspended;
            btnDeactivate.Enabled = !isDeleted && user.AccountStatus == AccountStatus.Active;
            btnDelete.Enabled = !isDeleted;

            lblSelectUser.Visible = false;
            gbUserOptions.Visible = true;
        }

        private void HideUserOptions()
        {
            gbUserOptions.Visible = false;
            lblSelectUser.Visible = true;
        }
    }
}
