using ADO.NET.Fundamentals.Store.DapperReposies.Library.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADO.NET.Fundamentals.Store.DapperReposies.Library
{
    public class ConnectionBuilder : IConnectionBuilder
    {
        private readonly string connectionString;

        public ConnectionBuilder(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection Build()
        {
            return new SqlConnection(connectionString);
        }
    }
}
