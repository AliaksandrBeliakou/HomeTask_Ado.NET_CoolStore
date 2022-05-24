using CoolStore.Library.SqlData.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CoolStore.Library.SqlData
{
    public class SqlConnectedModelActorsFactory : IConnectedDbActorsFactory
    {
        public IDbCommand GetCommand(IDbConnection connection, string command, CommandType commandType, IEnumerable<IDbDataParameter> dataParametrs)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = command;
            sqlCommand.CommandType = commandType;

            if(dataParametrs is not null)
            {
                foreach(var param in dataParametrs)
                {
                    if(param is null)
                    {
                        throw new ArgumentNullException(nameof(dataParametrs));
                    }

                    sqlCommand.Parameters.Add(param);
                }
            }

            return sqlCommand;        
        }

        public IDbConnection GetConnection(string connectionString)
        {
            if (connectionString is null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            return new SqlConnection(connectionString);
        }

        public IDataReader GetDataReader(IDbCommand command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            return command.ExecuteReader();
        }
    }
}
