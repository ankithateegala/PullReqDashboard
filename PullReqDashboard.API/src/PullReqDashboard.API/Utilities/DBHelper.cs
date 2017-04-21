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
            connectionString = @"Server=witomyf-ohio.cdqgdsizulw9.us-east-2.rds.amazonaws.com,1433;Database=witomyf-dev;User Id =admin;Password=adminwitomyfohio;";
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

        public void InsertPullRequest(PullRequest pullRequest)
        {
            string insertQuery = "INSERT INTO PULLREQUEST (id, eventType, createdAt, title, url, createdBy)"
                                + " VALUES(@id, @eventType, @createdAt, @title, @url, @createdBy)";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Execute(insertQuery, pullRequest);
            }
        }

        public void InsertApproved(Approved approved)
        {
            string insertQuery = "INSERT INTO APPROVED (pullRequestid, approvedBy, approvedAt)"
                                + " VALUES(@pullRequestId, @approvedBy, @approvedAt)";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Execute(insertQuery, approved);
            }
        }
    }
}
