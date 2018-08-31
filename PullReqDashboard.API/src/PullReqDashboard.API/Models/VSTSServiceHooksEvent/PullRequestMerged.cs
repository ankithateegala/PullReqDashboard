using System;

namespace PullReqDashboard.API.Models.VSTSServiceHooksEvent
{
    public class PullRequestMerged
    {
        public resource resource { get; set; }
        public message message { get; set; }
    }
}
