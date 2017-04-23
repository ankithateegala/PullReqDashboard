using PullReqDashboard.API.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullReqDashboard.API.Models.Response
{
    public class GetPullRequest
    {
        public Guid id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string createdBy { get; set; }
        public IEnumerable<Approved> approver { get; set; }
    }
}
