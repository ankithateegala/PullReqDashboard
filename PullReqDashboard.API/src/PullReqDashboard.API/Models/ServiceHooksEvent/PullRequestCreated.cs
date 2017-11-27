﻿using System;

namespace PullReqDashboard.API.Models.ServiceHooksEvent
{
    public class PullRequestCreated
    {
        public Guid id { get; set; }
        public string eventType { get; set; }
        public string createdAt { get; set; }
        public string title { get; set; }
        public string url { get; set; }

        public CreatedBy createdBy { get; set; }
    }

    public class CreatedBy
    {
        public string displayName { get; set; }
        
    }
}
