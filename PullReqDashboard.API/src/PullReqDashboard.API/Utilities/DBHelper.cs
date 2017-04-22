using PullReqDashboard.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PullReqDashboard.API.Models.DTO;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace PullReqDashboard.API.Utilities
{
    public class DBHelper : IDBHelper
    {
        private string connectionString;
        public DBHelper()
        {
            connectionString = @"Server=witomyf-ohio.cdqgdsizulw9.us-east-2.rds.amazonaws.com,1433;Database=PullRequestDashboard-Dev;User Id =admin;Password=adminwitomyfohio;";
        }
        private IDbConnection Connection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
        public PullRequest GetPullRequest()
        {
            throw new NotImplementedException();
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

        public async Task InsertApproved(Approved approved)
        {
            string insertQuery = "INSERT INTO APPROVED (pullRequestid, approvedBy, approvedAt)"
                                + " VALUES(@pullRequestId, @approvedBy, @approvedAt)";

            using (IDbConnection dbConnection = Connection)
            {
                await dbConnection.ExecuteAsync(insertQuery, approved);
            }
        }
        public async Task<IEnumerable<string>> GetApprovers(Guid id)
        {
            string selectQuery = "SELECT * FROM APPROVED WHERE pullRequestid=@id)";

            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.QueryAsync<string>(selectQuery, id);
            }
        }

        public async Task<IEnumerable<PullRequest>> GetPullRequests()
        {
            string selectQuery = "SELECT * FROM PULLREQUEST";

            using (IDbConnection dbConnection = Connection)
            {
                return await dbConnection.QueryAsync<PullRequest>(selectQuery);
            }
        }
    }
}
