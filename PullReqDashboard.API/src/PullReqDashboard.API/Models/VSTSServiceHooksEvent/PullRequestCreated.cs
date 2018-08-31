using System;
using System.Collections.Generic;

namespace PullReqDashboard.API.Models.VSTSServiceHooksEvent
{
    public class PullRequestCreated
    {
        public resource resource { get; set; }
        public message message { get; set; }

    }

    public class createdBy
    {
        public string displayName { get; set; }
    }

    public class resource
    {
        public string pullRequestId { get; set; }
        public createdBy createdBy { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public IEnumerable<Reviewer> reviewers { get; set; }
    }

    public class Reviewer
    {
        public string displayName { get; set; }
        public int vote { get; set; }
    }

    public class message
    {
        public string text { get; set; }
    }
}
