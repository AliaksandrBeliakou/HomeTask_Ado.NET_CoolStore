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

        public TEntity GetSingleRow<TEntity>(string sqlCommand, IEnumerable<SqlParameter>? sqlParametersparams, Func<IDataReader, TEntity> map)
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

                            var errorTextBuilder = new StringBuilder("Data in '")
                                .Append(sqlCommand).Append("' request ");
                            if (sqlParametersparams is null)
                            {
                                errorTextBuilder.Append("without ");
                            }
                            else
                            {
                                errorTextBuilder.Append("with '")
                                    .Append(string.Join(", ", sqlParametersparams.Select(x => $"{x.ParameterName} = {x.Value}")))
                                    .Append("' ");
                            }
                            
                            errorTextBuilder.Append("parameters is not found");
                            throw new DataException(errorTextBuilder.ToString());
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

        public IEnumerable<TEntity> Get<TEntity>(string sqlCommand, IEnumerable<SqlParameter>? sqlParametersparams, Func<IDataReader, TEntity> map)
        {
            var result = new List<TEntity>();
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
                            while (reader.Read())
                            {
                                result.Add(map(reader));
                            }
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

            return result;
        }
    }
}
