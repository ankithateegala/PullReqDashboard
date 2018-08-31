using System.Collections.Generic;

namespace PullReqDashboard.API.Models.VSTSServiceHooksEvent
{
    public class PullRequestUpdated
    {
        public resource resource { get; set; }
        public message message { get; set; }
    }
}
