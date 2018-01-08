using System.Collections.Generic;

namespace PullReqDashboard.API.Models.VSTSServiceHooksEvent
{
    public class PullRequestUpdated
    {
        public string id { get; set; }
        public string eventType { get; set; }
        public IEnumerable<Reviewer> reviewers { get; set; }

    }

    public class Reviewer
    {
        public string displayName { get; set; }
        public int vote { get; set; }
    }
}
