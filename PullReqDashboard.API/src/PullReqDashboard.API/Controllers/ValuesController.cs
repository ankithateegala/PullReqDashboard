using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PullReqDashboard.API.Models.ServiceHooksEvent;

namespace PullReqDashboard.API.Controllers
{
    [EnableCors("MyPolicy")]

    [Route("api/[controller]")]
    public class PullRequestController : Controller
    {
        // GET api/PullRequest
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/PullRequest/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/PullRequest
        [HttpPost]
        public void Post([FromBody]PullRequestCreated value)
        {

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
