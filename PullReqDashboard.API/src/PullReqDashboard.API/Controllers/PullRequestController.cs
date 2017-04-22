using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PullReqDashboard.API.Models.ServiceHooksEvent;
using PullReqDashboard.API.Interfaces;
using PullReqDashboard.API.Models.DTO;

namespace PullReqDashboard.API.Controllers
{
    [EnableCors("MyPolicy")]

    [Route("api/[controller]")]
    public class PullRequestController : Controller
    {
        public readonly IDBHelper _DBHelper;

        public PullRequestController(IDBHelper DBHelper)
        {
            _DBHelper = DBHelper;
        }
        // GET api/PullRequest
        [HttpGet]
        public IEnumerable<PullRequest> Get()
        {
            return _DBHelper.GetPullRequests().Result;
            //return new PullRequest[0];
        }

        // GET api/PullRequest/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/PullRequest
        [HttpPost]
        public async Task Post([FromBody]PullRequestCreated pullRequestCreated)
        {
            var pullRequest = new PullRequest
            {
                id = pullRequestCreated.id,
                eventType = pullRequestCreated.eventType,
                createdAt = DateTime.Parse(pullRequestCreated.createdAt),
                title = pullRequestCreated.title,
                url = pullRequestCreated.url,
                createdBy = pullRequestCreated.createdBy.displayName
            };
            await _DBHelper.InsertPullRequest(pullRequest);
        }

        // POST api/PullRequest
        [HttpPost]
        public async Task Post([FromBody]PullRequestUpdated pullRequestUpdated)
        {
            //if there are reviewers with vote == 10(approved)
            if (pullRequestUpdated.reviewers.Where(y => (y.vote == 10)).Count() > 0)
            {
                //get the existing list of approvers and compare
                var approvers = _DBHelper.GetApprovers(pullRequestUpdated.id).Result;
                var newApprover =  pullRequestUpdated.reviewers.Where(x => !approvers.Contains(x.displayName));
                if (newApprover.Count() != 1) { throw new ArgumentException(); }//why would this happen??

                var approvedBy = newApprover.First().displayName;
                var approved = new Approved
                {
                    pullRequestId = pullRequestUpdated.id,
                    approvedBy = approvedBy,
                    approvedAt = DateTime.Now
                };
                await _DBHelper.InsertApproved(approved);
            }
        }

        //// PUT api/PullRequest/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/PullRequest/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
