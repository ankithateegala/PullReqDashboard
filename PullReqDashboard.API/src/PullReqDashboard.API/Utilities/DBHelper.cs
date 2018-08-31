using System;
using PullReqDashboard.API.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PullReqDashboard.API.Models.DTO;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using PullReqDashboard.API.Models.VSTSServiceHooksEvent;
using PullReqDashboard.API.Models.Response;

namespace PullReqDashboard.API.Utilities
{
    public class DBHelper : IDBHelper
    {
        private readonly string m_connectionString;
        public DBHelper()
        {
            //connectionString = @"Server=witomyf-ohio.cdqgdsizulw9.us-east-2.rds.amazonaws.com,1433;Database=PullRequestDashboard-Dev;User Id =admin;Password=adminwitomyfohio;";

        }
        private IDbConnection Connection => new SqlConnection(m_connectionString);

        public async Task InsertPullRequest(PullRequest pullRequest)
        {
            //TODO: Add message
            var teamNames = new List<string>{ "Benjamin Luo", "Ben Simpson", "Michael Wilkinson", "Steven Wu", "Ankitha Teegala" };
            teamNames.Remove(pullRequest.createdBy);
            // Fun way of selecting random string from list. IS NOT EFFICIENT
            string randomReviewer1 = teamNames.OrderBy(s => Guid.NewGuid()).First();
            teamNames.Remove(randomReviewer1);
            string randomReviewer2 = teamNames.OrderBy(s => Guid.NewGuid()).First();
            string randomReviewers = randomReviewer1 + "," + randomReviewer2;
            pullRequest.randomReviewers = randomReviewers;

            string insertQuery = "INSERT INTO PullRequest (Id, CreatedAt, Title, Url, CreatedBy, RandomReviewers)"
                                + " VALUES(@id, @createdAt, @title, @url, @createdBy, @randomReviewers)";

            using (IDbConnection dbConnection = Connection)
            {
                await dbConnection.ExecuteAsync(insertQuery, pullRequest);
            }
        }

        public async Task InsertApproved(Approved approved)
        {
            string insertQuery = "INSERT INTO Approved (PullRequestid, ApprovedBy, ApprovedAt)"
                                + " VALUES(@pullRequestId, @approvedBy, @approvedAt)";

            using (IDbConnection dbConnection = Connection)
            {
                await dbConnection.ExecuteAsync(insertQuery, approved);
            }
        }
        public async Task<IEnumerable<string>> GetApprovers(PullRequestUpdated pullRequest)
        {
            string selectQuery = "SELECT ApprovedBy FROM Approved WHERE PullRequestid = @pullRequestId";

            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.QueryAsync<string>(selectQuery, pullRequest.resource);
            }
        }

        public async Task ClosePullRequest(PullRequestMerged pullRequestMerged)
        {
            //TODO: Add closedAt time
            string insertQuery = "UPDATE PullRequest SET Closed = 1, Message = @text WHERE Id = @pullRequestId";

            using (IDbConnection dbConnection = Connection)
            {
                await dbConnection.ExecuteAsync(insertQuery, new {pullRequestMerged.resource.pullRequestId, pullRequestMerged.message.text});
            }
        }

        public async Task<IEnumerable<GetPullRequest>> GetPullRequests()
        {
            string selectPullrequestQuery = "SELECT Id, Title, Url, CreatedBy, RandomReviewers FROM PullRequest where Closed = 0 order by CreatedAt";
            string selectApprovedQuery = "SELECT PullRequestId, ApprovedBy, ApprovedAt FROM Approved";
            IEnumerable<GetPullRequest> pullRequestList;
            IEnumerable<Approved> approvedList;
            IEnumerable<Approved> temp;
            using (IDbConnection dbConnection = Connection)
            {
                pullRequestList = await dbConnection.QueryAsync<GetPullRequest>(selectPullrequestQuery);
                approvedList = await dbConnection.QueryAsync<Approved>(selectApprovedQuery);
                foreach (GetPullRequest pullRequest in pullRequestList)
                {
                    temp = approvedList.Where(x => x.pullRequestId == pullRequest.id);
                    pullRequest.approver = new List<Approved>(temp);
                }
                return pullRequestList;
            }
        }
    }
}
