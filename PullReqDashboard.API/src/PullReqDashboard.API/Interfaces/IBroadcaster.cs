using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PullReqDashboard.API.Models.Response;

namespace PullReqDashboard.API.Interfaces
{
    public interface IBroadcaster
    {
        Task SetConnectionId(string connectionId);
        Task UpdatePullRequests(GetPullRequest pullRequest);
    }
}
