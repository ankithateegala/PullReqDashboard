using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PullReqDashboard.API.Models.ServiceHooksEvent
{
    public class PullRequestUpdated
    {
        public Guid id { get; set; }
        public string eventType { get; set; }
        public IEnumerable<Reviewer> reviewers { get; set; }

    }

    public class Reviewer
    {
        public string displayName { get; set; }
        public int vote { get; set; }
    }
}
