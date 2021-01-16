using System;
using System.Data;
using StudentManagement.Common;
using MySql.Data.MySqlClient;

namespace StudentManagement.Query
{
    public class QueryBase : IDisposable
    {
        protected IDbConnection Connection { get; }
        
        protected QueryBase(IBConfiguration configuration)
        {
            Connection = new MySqlConnection(configuration.ConnectionString);
            Connection.Open();
        }

        public void Dispose()
        {
            Connection?.Dispose();
        }
    }
}