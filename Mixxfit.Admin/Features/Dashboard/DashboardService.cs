using Mixxfit.Admin.Api;
using Mixxfit.Admin.Common;

namespace Mixxfit.Admin.Features.Dashboard
{
    public class DashboardService(MixxFitApiClient apiClient)
    {
        private const int MaxPageSize = 100;

        public async Task<(AdminDashboardResponse? Data, ProblemDetails? Problem)> GetAdminDashboardAsync()
        {
            return await apiClient.SendAsync<AdminDashboardResponse>(
                HttpMethod.Get, "admin/dashboard");
        }

        public async Task<(PagedResult<AdminDashboardUserDto>? Data, ProblemDetails? Problem)>
            GetAdminDashboardUsersAsync(
                int page = 1, int pageSize = 30, string search = "", string sort = "", bool isDeleted = false)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, MaxPageSize);

            return await apiClient.SendAsync<PagedResult<AdminDashboardUserDto>>(
                HttpMethod.Get,
                $"admin/users?page={page}&pageSize={pageSize}&search={Uri.EscapeDataString(search)}&sort={sort}&isDeleted={isDeleted}");
        }

        public async Task<ProblemDetails?> DeleteUserAsync(string userId)
        {
            return await apiClient.SendAsync(HttpMethod.Delete, $"admin/users/{userId}");
        }

        public async Task<ProblemDetails?> SuspendUser(string userId)
        {
            return await apiClient.SendAsync(HttpMethod.Post, $"admin/users/{userId}/suspend");
        }

        public async Task<ProblemDetails?> UnsuspendUser(string userId)
        {
            return await apiClient.SendAsync(HttpMethod.Post, $"admin/users/{userId}/unsuspend");
        }
    }
}
