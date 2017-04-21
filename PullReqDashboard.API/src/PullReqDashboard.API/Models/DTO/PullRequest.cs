using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullReqDashboard.API.Models.DTO
{
    public class PullRequest
    {
        public Guid id { get; set; }
        public string eventType { get; set; }
        public DateTime createdAt { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string createdBy { get; set; }
    }
}