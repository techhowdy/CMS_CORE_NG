using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModelService;

namespace ActivityService
{
    public interface IActivitySvc
    {
        Task AddUserActivity(ActivityModel model);

        Task<List<ActivityModel>> GetUserActivity(string userId);
    }
}
