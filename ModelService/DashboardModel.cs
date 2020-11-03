using System;
namespace ModelService
{
    public class DashboardModel
    {
        public int TotalUsers { get; set; }
        public int NewUsers { get; set; }
        public int PendingRequests { get; set; }
        public int TotalPosts { get; set; }
    }
}
