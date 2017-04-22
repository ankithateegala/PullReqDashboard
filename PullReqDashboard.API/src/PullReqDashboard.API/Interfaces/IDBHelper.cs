using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PullReqDashboard.API.Models.DTO;

namespace PullReqDashboard.API.Interfaces
{
    public interface IDBHelper
    {
        Task InsertPullRequest(PullRequest pullRequest);
        Task<IEnumerable<PullRequest>> GetPullRequests();
        Task InsertApproved(Approved approved);
        Task<IEnumerable<string>> GetApprovers(Guid id);
    }
}