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

        //// GET api/PullRequest
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/PullRequest/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/PullRequest
        [HttpPost]
        public void Post([FromBody]PullRequestCreated pullRequestCreated)
        {
            var pullRequest = new PullRequest
            {
                id = pullRequestCreated.id,
                eventType = pullRequestCreated.eventType,
                creationDate = pullRequestCreated.creationDate,
                title = pullRequestCreated.title,
                url = pullRequestCreated.url,
                createdBy = pullRequestCreated.createdBy.displayName
            };
            _DBHelper.InsertPullRequest(pullRequest);
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
