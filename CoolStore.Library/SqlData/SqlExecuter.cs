using CoolStore.Library.SqlData.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace CoolStore.Library.SqlData
{
    internal class SqlExecuter
    {
        private readonly string connectionString;
        private readonly IConnectedDbActorsFactory actorBuilder;
        public SqlExecuter(string connectionString, IConnectedDbActorsFactory actorBuilder)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            this.actorBuilder = actorBuilder ?? throw new ArgumentNullException(nameof(actorBuilder));
        }

        public TEntity GetSingleRow<TEntity>(string sqlCommand, IEnumerable<SqlParameter> sqlParametersparams, Func<IDataReader, TEntity> map)
        {
            using (var connection = this.actorBuilder.GetConnection(connectionString))
            {
                var command = this.actorBuilder.GetCommand(connection, sqlCommand, CommandType.Text, sqlParametersparams);
                try
                {
                    connection.Open();
                    using (var reader = this.actorBuilder.GetDataReader(command))
                    {
                        try
                        {
                            if (reader.Read())
                            {
                                return map(reader);
                            }

                            var errorText = new StringBuilder("Data in '")
                                .Append(sqlCommand).Append("' request with '")
                                .Append(string.Join(", ", sqlParametersparams.Select(x => $"{x.ParameterName} = {x.Value}")))
                                .Append("' parameters is not found")
                                .ToString();
                            throw new DataException(errorText);
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
