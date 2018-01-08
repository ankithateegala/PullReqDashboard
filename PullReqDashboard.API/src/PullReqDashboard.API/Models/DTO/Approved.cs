using System;

namespace PullReqDashboard.API.Models.DTO
{
    public class Approved
    {
        public string pullRequestId { get; set; }
        public string approvedBy { get; set; }
        public DateTime approvedAt { get; set; }
    }
}
