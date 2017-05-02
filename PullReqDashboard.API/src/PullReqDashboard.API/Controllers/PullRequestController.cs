using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PullReqDashboard.API.Models.ServiceHooksEvent;
using PullReqDashboard.API.Interfaces;
using PullReqDashboard.API.Models.DTO;
using PullReqDashboard.API.Models.Response;
using PullReqDashboard.API.Utilities;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace PullReqDashboard.API.Controllers
{

    [Route("api/[controller]")]
    public class PullRequestController: Controller
    {
        private readonly IDBHelper _DBHelper;
        private IConnectionManager _connectionManager;

        public PullRequestController(IDBHelper DBHelper, IConnectionManager connectionManager)
        {
            _DBHelper = DBHelper;
            _connectionManager = connectionManager;
        }
        // GET api/PullRequest
        public async Task<IEnumerable<GetPullRequest>> Get()
        {
            var pullRequests = await _DBHelper.GetPullRequests();
            _connectionManager.GetHubContext<SignalRHub>().Clients.All.test("SignalR connection establised");
            return pullRequests;
        }

        // POST api/PullRequest
        [HttpPost]
        public async Task Post([FromBody]PullRequestCreated pullRequestCreated)
        {
            var pullRequest = new PullRequest
            {
                id = pullRequestCreated.id,
                eventType = pullRequestCreated.eventType,
                createdAt = DateTime.UtcNow,
                title = pullRequestCreated.title,
                url = pullRequestCreated.url,
                createdBy = pullRequestCreated.createdBy.displayName
            };
            await _DBHelper.InsertPullRequest(pullRequest);

            var pullRequests = await _DBHelper.GetPullRequests();
            var h = _connectionManager.GetHubContext<SignalRHub>();
            h.Clients.All.updatePullRequests(pullRequests);
        }

        // PUT api/PullRequest
        [HttpPut]
        public async Task Put([FromBody]PullRequestUpdated pullRequestUpdated)
        {
            //throw if id not exists
            //if there are reviewers with vote == 10(approved)
            if (pullRequestUpdated.reviewers.Where(y => (y.vote == 10)).Count() > 0)
            {
                //get the existing list of approvers and compare
                var approvers = _DBHelper.GetApprovers(pullRequestUpdated).Result;
                var newApprover =  pullRequestUpdated.reviewers.Where(x => !approvers.Contains(x.displayName));
                if (newApprover.Count() != 1) { throw new ArgumentException(); }//why would this happen??

                var approvedBy = newApprover.First().displayName;
                var approved = new Approved
                {
                    pullRequestId = pullRequestUpdated.id,
                    approvedBy = approvedBy,
                    approvedAt = DateTime.UtcNow
                };
                await _DBHelper.InsertApproved(approved);

                var pullRequests = await _DBHelper.GetPullRequests();
                _connectionManager.GetHubContext<SignalRHub>().Clients.All.updatePullRequests(pullRequests);

            }
        }

        // DELETE api/PullRequest
        [HttpDelete]
        public async Task Delete([FromBody]PullRequestMerged pullRequestMerged)
        {
            await _DBHelper.DeletePullRequest(pullRequestMerged);

            var pullRequests = await _DBHelper.GetPullRequests();
            _connectionManager.GetHubContext<SignalRHub>().Clients.All.updatePullRequests(pullRequests);
        }
    }
}