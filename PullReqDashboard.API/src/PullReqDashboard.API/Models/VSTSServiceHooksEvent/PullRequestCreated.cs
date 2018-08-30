using System;
using System.Collections.Generic;

namespace PullReqDashboard.API.Models.VSTSServiceHooksEvent
{
    public class PullRequestCreated
    {
        public string id { get; set; }

        public resource resource { get; set; }

    }

    public class createdBy
    {
        public string displayName { get; set; }
    }

    public class resource
    {
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
}
