using System.Data;
using System.Data.SqlClient;

namespace CoolStore.Library.SqlData.Dapper
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
