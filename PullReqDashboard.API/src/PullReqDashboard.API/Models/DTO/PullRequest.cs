﻿using System;

namespace PullReqDashboard.API.Models.DTO
{
    public class PullRequest
    {
        public string id { get; set; }
        public DateTime createdAt { get; set; }
        public string title { get; set; }
        public string url { get; set; }
        public string createdBy { get; set; }
        public string randomReviewers { get; set; }
        public bool closed { get; set; }
    }
}