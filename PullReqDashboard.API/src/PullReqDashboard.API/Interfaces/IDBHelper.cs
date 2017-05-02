using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PullReqDashboard.API.Models.DTO;
using PullReqDashboard.API.Models.ServiceHooksEvent;
using PullReqDashboard.API.Models.Response;

namespace PullReqDashboard.API.Interfaces
{
    public interface IDBHelper
    {
        Task InsertPullRequest(PullRequest pullRequest);
        Task<IEnumerable<GetPullRequest>> GetPullRequests();
        Task InsertApproved(Models.DTO.Approved approved);
        Task<IEnumerable<string>> GetApprovers(PullRequestUpdated pullRequest);
        Task DeletePullRequest(PullRequestMerged pullRequestMerged);
    }
}