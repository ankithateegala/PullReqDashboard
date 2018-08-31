using PullReqDashboard.API.Models.DTO;
using System.Collections.Generic;

namespace PullReqDashboard.API.Models.Response
{
    public class GetPullRequest
    {
        public string id { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string createdBy { get; set; }
        public IEnumerable<Approved> approver { get; set; }
        public string randomReviewers { get; set; }
    }
}
