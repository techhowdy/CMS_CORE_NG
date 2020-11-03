using System;
using System.Threading.Tasks;
using ModelService;

namespace DashboardService
{
    public interface IDashboardSvc
    {
        Task<DashboardModel> GetDashboardData();
    }
}
