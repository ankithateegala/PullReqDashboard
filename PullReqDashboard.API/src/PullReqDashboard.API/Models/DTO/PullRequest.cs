using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullReqDashboard.API.Models.DTO
{
    public class PullRequest
    {
        Guid id { get; set; }
        string eventType { get; set; }
        string creationDate { get; set; }
        string title { get; set; }
        string url { get; set; }
        string createdBy { get; set; }
    }
}