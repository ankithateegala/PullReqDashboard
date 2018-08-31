using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PullReqDashboard.API.Models.VSTSServiceHooksEvent;
using PullReqDashboard.API.Interfaces;
using PullReqDashboard.API.Models.DTO;
using PullReqDashboard.API.Models.Response;
using PullReqDashboard.API.Utilities;
using Microsoft.AspNetCore.SignalR;

namespace PullReqDashboard.API.Controllers
{

    [Route("api/[controller]")]
    public class PullRequestController: Controller
    {
        private readonly IDBHelper _DBHelper;
        private IHubContext<SignalRHub> _hub;

        public PullRequestController(IDBHelper DBHelper, IHubContext<SignalRHub> hub)
        {
            _DBHelper = DBHelper;
            _hub = hub;
        }

        // GET api/PullRequest
        public async Task<IEnumerable<GetPullRequest>> Get()
        {
            var pullRequests = await _DBHelper.GetPullRequests();
            return pullRequests;
        }

        // POST api/PullRequest
        [HttpPost]
        public async Task Post([FromBody]PullRequestCreated pullRequestCreated)
        {
            var pullRequest = new PullRequest
            {
                id = pullRequestCreated.resource.pullRequestId,
                createdAt = DateTime.UtcNow,
                title = pullRequestCreated.resource.title,
                url = pullRequestCreated.resource.url,
                createdBy = pullRequestCreated.resource.createdBy.displayName
            };
            await _DBHelper.InsertPullRequest(pullRequest);

            var pullRequests = await _DBHelper.GetPullRequests();
            await _hub.Clients.All.SendAsync("updatePullRequests", pullRequests);
        }
        

        // POST api/PullRequest/update
        [HttpPost]
        [Route("update")]
        public async Task Update([FromBody]PullRequestUpdated pullRequestUpdated)
        {
            //TODO: throw/skip if id not exists
            //if there are reviewers with vote == 10(approved)
            if (pullRequestUpdated.resource.reviewers.Any(y => y.vote == 10))
            {
                //get the existing list of approvers and compare
                var approvers = _DBHelper.GetApprovers(pullRequestUpdated).Result;
                var newApprover =  pullRequestUpdated.resource.reviewers.Where(x => !approvers.Contains(x.displayName));
                if (newApprover.Count() != 1) { throw new ArgumentException(); }//why would this happen??

                var approvedBy = newApprover.First().displayName;
                var approved = new Approved
                {
                    pullRequestId = pullRequestUpdated.resource.pullRequestId,
                    approvedBy = approvedBy,
                    approvedAt = DateTime.UtcNow
                };
                await _DBHelper.InsertApproved(approved);

                var pullRequests = await _DBHelper.GetPullRequests();
                await _hub.Clients.All.SendAsync("" +
                                                 "update" +
                                                 "PullRequests", pullRequests);

            }
        }

        // POST api/PullRequest/merge
        [HttpPost]
        [Route("merge")]
        public async Task Merge([FromBody]PullRequestMerged pullRequestMerged)
        {
            await _DBHelper.ClosePullRequest(pullRequestMerged);

            var pullRequests = await _DBHelper.GetPullRequests();
            await _hub.Clients.All.SendAsync("updatePullRequests", pullRequests);
        }
    }
}