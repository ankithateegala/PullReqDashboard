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
using PullReqDashboard.API.Models.SlackOutgoingWebhook;

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
            await _hub.Clients.All.InvokeAsync("updatePullRequests", pullRequests);
        }

        // POST api/PullRequest
        public async Task PostFromSlack([FromBody]Data data)
        {
            PullRequest pullRequest = null;
            var userName = "noName";
            var prTitle = "Title";
            var prUrl = "noUrl";
            if (data.Text.Contains("created"))
            {
                var splitText = data.Text.Split(" created ");
                userName = splitText[0];
                prTitle = splitText[1].Split(" in ")[1];
                pullRequest = new PullRequest
                {
                    eventType = "slack.created",
                    createdAt = DateTime.UtcNow,
                    title = prTitle,
                    url = prUrl,
                    createdBy = userName
                };
            }

            if (pullRequest != null)
            {
                await _DBHelper.InsertPullRequest(pullRequest);
                var pullRequests = await _DBHelper.GetPullRequests();
                await _hub.Clients.All.InvokeAsync("updatePullRequests", pullRequests);
            }
        }

        // PUT api/PullRequest
        [HttpPut]
        public async Task Put([FromBody]PullRequestUpdated pullRequestUpdated)
        {
            //throw if id not exists
            //if there are reviewers with vote == 10(approved)
            if (pullRequestUpdated.reviewers.Any(y => y.vote == 10))
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
                await _hub.Clients.All.InvokeAsync("updatePullRequests", pullRequests);
            }
        }

        // DELETE api/PullRequest
        [HttpDelete]
        public async Task Delete([FromBody]PullRequestMerged pullRequestMerged)
        {
            await _DBHelper.DeletePullRequest(pullRequestMerged);

            var pullRequests = await _DBHelper.GetPullRequests();
            await _hub.Clients.All.InvokeAsync("updatePullRequests", pullRequests);
        }
    }
}