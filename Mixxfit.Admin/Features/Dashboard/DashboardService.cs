using Mixxfit.Admin.Api;
using Mixxfit.Admin.Common;

namespace Mixxfit.Admin.Features.Dashboard
{
    public class DashboardService(MixxFitApiClient apiClient)
    {
        private const int MaxPageSize = 100;

        public Task<(AdminDashboardResponse? Data, ProblemDetails? Problem)> GetAdminDashboardAsync()
        {
            return apiClient.SendAsync<AdminDashboardResponse>(
                HttpMethod.Get, "admin/dashboard");
        }

        public Task<(PagedResult<AdminDashboardUserDto>? Data, ProblemDetails? Problem)> GetAdminDashboardUsersAsync(
            int page = 1, int pageSize = 30, string search = "", string sort = "", bool isDeleted = false)
        {
            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, MaxPageSize);

            return apiClient.SendAsync<PagedResult<AdminDashboardUserDto>>(
                HttpMethod.Get, $"admin/users?page={page}&pageSize={pageSize}&search={search}&sort={sort}&isDeleted={isDeleted}");
        }
    }
}
