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
            string insertQuery = "INSERT INTO PULLREQUEST ([DAY], Eat1, Eat2, Eat3, Eat4, Eat5, Eat6, userid)"
                                + " VALUES(@Day, @Eat1, @Eat2, @Eat3, @Eat4, @Eat5, @Eat6, 1)";

            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Execute(insertQuery, pullRequest);
            }
        }
    }
}
