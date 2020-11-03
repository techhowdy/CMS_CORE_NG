using System;
using System.Linq;
using System.Threading.Tasks;
using DataService;
using Microsoft.EntityFrameworkCore;
using ModelService;
using Serilog;

namespace DashboardService
{
    public class DashboardSvc : IDashboardSvc
    {
        private readonly ApplicationDbContext _db;

        public DashboardSvc(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<DashboardModel> GetDashboardData()
        {
            DashboardModel dashboardModel = new DashboardModel();

            try
            {
                await using var dbContextTransaction = _db.Database.BeginTransaction();

                try
                {
                    dashboardModel.TotalUsers = await _db.ApplicationUsers.CountAsync();
                    dashboardModel.TotalPosts = 3516;
                    dashboardModel.PendingRequests = 50;
                    dashboardModel.NewUsers = await _db.ApplicationUsers
                        .Where(x => x.AccountCreatedOn == DateTime.Today).CountAsync();
                }
                catch (Exception ex)
                {
                    Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                       ex.Message, ex.StackTrace, ex.InnerException, ex.Source);

                    dbContextTransaction.Rollback();
                }             

            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                       ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }

            return dashboardModel;
        }
    }
}
