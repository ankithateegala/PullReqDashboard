using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PullReqDashboard.API.Models.ServiceHooksEvent;
using PullReqDashboard.API.Interfaces;
using PullReqDashboard.API.Models.DTO;
using PullReqDashboard.API.Models.Response;
using PullReqDashboard.API.Utilities;
using Microsoft.AspNetCore.SignalR.Infrastructure;

namespace PullReqDashboard.API.Controllers
{
    [EnableCors("MyPolicy")]

    [Route("api/[controller]")]
    public class PullRequestController : ApiHubController<Broadcaster>
    {
        public readonly IDBHelper _DBHelper;

        public PullRequestController(IConnectionManager signalRConnectionManager, IDBHelper DBHelper)
            :base(signalRConnectionManager)
        {
            _DBHelper = DBHelper;
        }
        // GET api/PullRequest
        [HttpGet]
        public async Task<IEnumerable<GetPullRequest>> Get()
        {
            return await _DBHelper.GetPullRequests();
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
            await Clients.All.UpdatePullRequests(pullRequests);
        }

        // PUT api/PullRequest
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
                var approved = new Models.DTO.Approved
                {
                    pullRequestId = pullRequestUpdated.id,
                    approvedBy = approvedBy,
                    approvedAt = DateTime.UtcNow
                };
                await _DBHelper.InsertApproved(approved);

                var pullRequests = await _DBHelper.GetPullRequests();
                await Clients.All.UpdatePullRequests(pullRequests);
            }
        }

        //// DELETE api/PullRequest/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
