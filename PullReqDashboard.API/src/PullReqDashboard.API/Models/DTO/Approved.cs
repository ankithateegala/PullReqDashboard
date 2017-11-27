using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullReqDashboard.API.Models.DTO
{
    public class Approved
    {
        public Guid pullRequestId { get; set; }
        public string approvedBy { get; set; }
        public DateTime approvedAt { get; set; }
    }
}
