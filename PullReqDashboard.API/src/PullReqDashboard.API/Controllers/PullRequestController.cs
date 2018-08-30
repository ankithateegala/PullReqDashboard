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
                id = pullRequestCreated.id,
                createdAt = DateTime.UtcNow,
                title = pullRequestCreated.resource.title,
                url = pullRequestCreated.resource.url,
                createdBy = pullRequestCreated.resource.createdBy.displayName
            };
            await _DBHelper.InsertPullRequest(pullRequest);

            var pullRequests = await _DBHelper.GetPullRequests();
            await _hub.Clients.All.SendAsync("updatePullRequests", pullRequests);
        }


        // POST api/PullRequest/FromSlack
        //[HttpPost("FromSlack")]
        //public async Task PostFromSlack([FromBody]PullRequestFromSlack PullRequestFromSlack)
        //{
        //    PullRequest pullRequest = null;
        //    var userName = "noName";
        //    var prTitle = "Title";
        //    var prUrl = "noUrl";
        //    if (PullRequestFromSlack.Text.Contains("created"))
        //    {
        //        var splitText = PullRequestFromSlack.Text.Split(" created ");
        //        userName = splitText[0];
        //        splitText = splitText[1].Split(" in ");
        //        var repo = splitText[1];
        //        splitText = splitText[0].Split(" (");
        //        prTitle = splitText[1].TrimEnd(')');

        //        pullRequest = new PullRequest
        //        {
        //            id = splitText[0],
        //            eventType = "slack.created",
        //            createdAt = DateTime.UtcNow,
        //            title = prTitle,
        //            url = prUrl,
        //            createdBy = userName,
        //            from = "slack"
        //        };
        //    }

        //    if (pullRequest != null)
        //    {
        //        await _DBHelper.InsertPullRequest(pullRequest);
        //        var pullRequests = await _DBHelper.GetPullRequests();
        //        await _hub.Clients.All.InvokeAsync("updatePullRequests", pullRequests);
        //    }
        //}

        // PUT api/PullRequest
        [HttpPut]
        public async Task Put([FromBody]PullRequestUpdated pullRequestUpdated)
        {
            //throw if id not exists
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
                    pullRequestId = pullRequestUpdated.id,
                    approvedBy = approvedBy,
                    approvedAt = DateTime.UtcNow
                };
                await _DBHelper.InsertApproved(approved);

                var pullRequests = await _DBHelper.GetPullRequests();
                //await _hub.Clients.All.InvokeAsync("updatePullRequests", pullRequests);
            }
        }

        // DELETE api/PullRequest
        [HttpDelete]
        public async Task Delete([FromBody]PullRequestMerged pullRequestMerged)
        {
            await _DBHelper.DeletePullRequest(pullRequestMerged);

            var pullRequests = await _DBHelper.GetPullRequests();
            //await _hub.Clients.All.InvokeAsync("updatePullRequests", pullRequests);
        }
    }
}