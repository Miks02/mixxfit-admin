using System.Text.RegularExpressions;
using Mixxfit.Admin.Common;
using Mixxfit.Admin.Common.Controls;
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
        private int _page = 1;
        private bool _hasPreviousPage;
        private bool _hasNextPage;
        private bool _busy;
        private AdminDashboardUserDto? _selectedUser;

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

        public async Task LoadPagedUsers(int? page = null)
        {
            var ticket = ++_loadTicket;
            var requestedPage = page ?? _page;
            var search = tbSearch.Text.Trim();
            var sort = SortKeys[Math.Max(cbSort.SelectedIndex, 0)];
            var isDeleted = chkDeleted.Checked;

            UseWaitCursor = true;

            try
            {
                var (data, problem) = await _dashboard.GetAdminDashboardUsersAsync(
                    page: requestedPage, search: search, sort: sort, isDeleted: isDeleted);

                if (ticket != _loadTicket) return;

                if (problem is not null)
                {
                    MessageBox.Show(ErrorCatalog.Describe(problem), "Error");
                    return;
                }

                // The last item of the last page was just removed: step back to the new last page.
                if (data!.Items.Count == 0 && data.TotalPages > 0 && requestedPage > data.TotalPages)
                {
                    await LoadPagedUsers(data.TotalPages);
                    return;
                }

                _loadedDeleted = isDeleted;
                _page = data.Page;
                RefillDataGrid(data.Items);
                UpdatePager(data);
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

        private void UpdatePager(PagedResult<AdminDashboardUserDto> result)
        {
            var totalPages = Math.Max(result.TotalPages, 1);

            lblPage.Text = $"Page {result.Page} of {totalPages}";
            lblTotal.Text = $"Showing {result.Items.Count} of {result.TotalCount:N0} users";
            _hasPreviousPage = result.HasPreviousPage;
            _hasNextPage = result.HasNextPage;
            ApplyButtonStates();
        }

        private async void btnPrev_Click(object? sender, EventArgs e)
        {
            await LoadPagedUsers(_page - 1);
        }

        private async void btnNext_Click(object? sender, EventArgs e)
        {
            await LoadPagedUsers(_page + 1);
        }

        private async void btnRefetch_Click(object? sender, EventArgs e)
        {
            SetBusy(btnRefetch);
            try
            {
                await ReloadAsync();
            }
            finally
            {
                SetBusy(null);
            }
        }

        private async void btnActivate_Click(object? sender, EventArgs e)
        {
            if (_selectedUser is not { } user) return;

            await RunUserActionAsync(btnActivate, () => _dashboard.UnsuspendUser(user.Id),
                $"Activate {user.Email}?", $"{user.Email} has been activated.");
        }

        private async void btnDeactivate_Click(object? sender, EventArgs e)
        {
            if (_selectedUser is not { } user) return;

            await RunUserActionAsync(btnDeactivate, () => _dashboard.SuspendUser(user.Id),
                $"Deactivate {user.Email}?", $"{user.Email} has been deactivated.");
        }

        private async void btnDelete_Click(object? sender, EventArgs e)
        {
            if (_selectedUser is not { } user) return;

            await RunUserActionAsync(btnDelete, () => _dashboard.DeleteUserAsync(user.Id),
                $"Delete {user.Email}?", $"{user.Email} has been deleted.");
        }

        /// <summary>
        /// Asks for confirmation, then runs a moderation call with the triggering button spinning
        /// and every other button disabled, refreshes stats and the user list and reports the outcome.
        /// </summary>
        private async Task RunUserActionAsync(
            RoundedButton trigger, Func<Task<ProblemDetails?>> action, string confirmMessage, string successMessage)
        {
            var confirm = MessageBox.Show(confirmMessage, "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (confirm != DialogResult.Yes) return;

            ProblemDetails? problem;

            SetBusy(trigger);
            try
            {
                problem = await action();
                if (problem is null)
                    await ReloadAsync();
            }
            finally
            {
                SetBusy(null);
            }

            if (problem is not null)
                MessageBox.Show(ErrorCatalog.Describe(problem), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                MessageBox.Show(successMessage, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private async Task ReloadAsync()
        {
            await LoadDataAsync();
            await LoadPagedUsers();
        }

        /// <summary>
        /// Marks <paramref name="active"/> as the working button (spinner) and disables all
        /// buttons but the log out one; pass null to finish.
        /// </summary>
        private void SetBusy(RoundedButton? active)
        {
            _busy = active is not null;

            foreach (var button in new[] { btnActivate, btnDeactivate, btnDelete, btnRefetch, btnPrev, btnNext })
                button.Busy = ReferenceEquals(button, active);

            ApplyButtonStates();
        }

        /// <summary>Single place that decides which buttons are clickable.</summary>
        private void ApplyButtonStates()
        {
            var user = _selectedUser;
            var canModerate = !_busy && user is not null && !IsDeleted(user);

            btnActivate.Enabled = canModerate && user!.AccountStatus == AccountStatus.Suspended;
            btnDeactivate.Enabled = canModerate && user!.AccountStatus == AccountStatus.Active;
            btnDelete.Enabled = canModerate;

            btnPrev.Enabled = !_busy && _hasPreviousPage;
            btnNext.Enabled = !_busy && _hasNextPage;
            btnRefetch.Enabled = !_busy;
        }

        private bool IsDeleted(AdminDashboardUserDto user) => user.DeletedAt is not null || _loadedDeleted;

        private void tbSearch_TextChanged(object? sender, EventArgs e)
        {
            tmrSearch.Stop();
            tmrSearch.Start();
        }

        private async void tmrSearch_Tick(object? sender, EventArgs e)
        {
            tmrSearch.Stop();
            await LoadPagedUsers(1);
        }

        private async void cbSort_SelectedIndexChanged(object? sender, EventArgs e)
        {
            await LoadPagedUsers(1);
        }

        private async void chkDeleted_CheckedChanged(object? sender, EventArgs e)
        {
            await LoadPagedUsers(1);
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
            _selectedUser = user;
            var isDeleted = IsDeleted(user);

            lblName.Text = user.FullName;
            lblEmail.Text = user.Email;
            lblCreated.Text = user.CreatedAt.ToLocalTime().ToString("dd MMM yyyy, HH:mm");

            lblDeletedCaption.Visible = isDeleted;
            lblDeleted.Visible = isDeleted;
            lblDeleted.Text = user.DeletedAt?.ToLocalTime().ToString("dd MMM yyyy, HH:mm") ?? None;

            ApplyButtonStates();

            lblSelectUser.Visible = false;
            gbUserOptions.Visible = true;
        }

        private void HideUserOptions()
        {
            _selectedUser = null;
            ApplyButtonStates();

            gbUserOptions.Visible = false;
            lblSelectUser.Visible = true;
        }
    }
}
