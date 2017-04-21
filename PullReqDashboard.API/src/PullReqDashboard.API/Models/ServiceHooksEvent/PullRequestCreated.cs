using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullReqDashboard.API.Models.ServiceHooksEvent
{
    public class PullRequestCreated
    {
        Guid id { get; set; }
        string eventType { get; set; }
        string creationDate { get; set; }
        string title { get; set; }
        string url { get; set; }

        CreatedBy createdBy { get; set; }
    }

    public class CreatedBy
    {
        string displayName { get; set; }
        
    }
}
