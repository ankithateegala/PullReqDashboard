using PullReqDashboard.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PullReqDashboard.API.Models.DTO;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using PullReqDashboard.API.Models.ServiceHooksEvent;
using PullReqDashboard.API.Models.Response;

namespace PullReqDashboard.API.Utilities
{
    public class DBHelper : IDBHelper
    {
        private string connectionString;
        public DBHelper()
        {
            connectionString = @"Server=witomyf-ohio.cdqgdsizulw9.us-east-2.rds.amazonaws.com,1433;Database=PullRequestDashboard-Dev;";
        }
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }

        public async Task InsertPullRequest(PullRequest pullRequest)
        {
            string insertQuery = "INSERT INTO PULLREQUEST (id, eventType, createdAt, title, url, createdBy)"
                                + " VALUES(@id, @eventType, @createdAt, @title, @url, @createdBy)";

            using (IDbConnection dbConnection = Connection)
            {
                await dbConnection.ExecuteAsync(insertQuery, pullRequest);
            }
        }

        public async Task InsertApproved(Models.DTO.Approved approved)
        {
            string insertQuery = "INSERT INTO APPROVED (pullRequestid, approvedBy, approvedAt)"
                                + " VALUES(@pullRequestId, @approvedBy, @approvedAt)";

            using (IDbConnection dbConnection = Connection)
            {
                await dbConnection.ExecuteAsync(insertQuery, approved);
            }
        }
        public async Task<IEnumerable<string>> GetApprovers(PullRequestUpdated pullRequest)
        {
            string selectQuery = "SELECT approvedBy FROM APPROVED WHERE pullRequestid = @id";

            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.QueryAsync<string>(selectQuery, pullRequest);
            }
        }

        public async Task<IEnumerable<GetPullRequest>> GetPullRequests()
        {
            string selectPullrequestQuery = "SELECT id, title, url, createdBy FROM PULLREQUEST";
            string selectApprovedQuery = "SELECT pullRequestId, approvedBy, approvedAt FROM APPROVED";
            IEnumerable<GetPullRequest> pullRequestList;
            IEnumerable<Approved> approvedList;
            IEnumerable<Approved> temp;
            using (IDbConnection dbConnection = Connection)
            {
                pullRequestList = await dbConnection.QueryAsync<GetPullRequest>(selectPullrequestQuery);
                approvedList = await dbConnection.QueryAsync<Approved>(selectApprovedQuery);
                foreach (GetPullRequest pullRequest in pullRequestList)
                {
                    temp = new List<Approved>();
                    temp = approvedList.Where(x => (x.pullRequestId == pullRequest.id));
                    pullRequest.approver = new List<Approved>(temp);
                }
                return pullRequestList;
            }
        }
    }
}
