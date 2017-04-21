using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PullReqDashboard.API.Models.DTO;

namespace PullReqDashboard.API.Interfaces
{
    public interface IDBHelper
    {
        void InsertPullRequest(PullRequest pullRequest);
        PullRequest GetPullRequest();
        void InsertApproved(Approved approved);
    }
}