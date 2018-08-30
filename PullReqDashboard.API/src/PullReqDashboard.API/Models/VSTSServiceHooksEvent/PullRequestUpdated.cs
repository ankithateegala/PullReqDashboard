using System.Collections.Generic;

namespace PullReqDashboard.API.Models.VSTSServiceHooksEvent
{
    public class PullRequestUpdated
    {
        public string id { get; set; }
        public resource resource { get; set; }
    }
}
