﻿using CoolStore.Library.SqlData.Interfaces;
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

        public TEntity GetSingle<TEntity>(string sqlCommand, CommandType commandType, IEnumerable<SqlParameter>? sqlParametersparams, Func<IDataReader, TEntity> map)
        {
            TEntity? result = default;
            this.ConnectCreateReaderAndAct(sqlCommand, commandType, sqlParametersparams, reader =>
            {
                if (reader.Read())
                {
                    result = map(reader);
                }
            });
            if (result is null)
            {
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
            return result;
        }

        public IEnumerable<TEntity> GetList<TEntity>(string sqlCommand, CommandType commandType, IEnumerable<SqlParameter>? sqlParametersparams, Func<IDataReader, TEntity> map)
        {
            var result = new List<TEntity>();
            this.ConnectCreateReaderAndAct(sqlCommand, commandType, sqlParametersparams, reader =>
            {
                while (reader.Read())
                {
                    result.Add(map(reader));
                }
            });

            return result;
        }

        public TResult? GetScalar<TEntity, TResult>(string sqlCommand, CommandType commandType, IEnumerable<SqlParameter>? sqlParametersparams, Func<IDataReader, TEntity> map)
        {
            object? result = default;
            ConnectAndAct(sqlCommand, commandType, sqlParametersparams, command =>
            {
                result = command.ExecuteScalar();
            });

            return (TResult?)result;
        }
        public void GetNothing(string sqlCommand, CommandType commandType, IEnumerable<SqlParameter>? sqlParametersparams)
        {
            ConnectAndAct(sqlCommand, commandType, sqlParametersparams, command =>
            {
                command.ExecuteNonQuery();
            });
        }

        private void ConnectAndAct(string sqlCommand, CommandType commandType, IEnumerable<SqlParameter>? sqlParametersparams, Action<IDbCommand> action)
        {
            using (var connection = this.actorBuilder.GetConnection(connectionString))
            {
                var command = this.actorBuilder.GetCommand(connection, sqlCommand, commandType, sqlParametersparams);
                try
                {
                    connection.Open();
                    action(command);
                }
                finally
                {
                    connection.Close();
                }
            }
        }

        private void ConnectCreateReaderAndAct(string sqlCommand, CommandType commandType, IEnumerable<SqlParameter>? sqlParametersparams, Action<IDataReader> action)
        {
            ConnectAndAct(sqlCommand, commandType, sqlParametersparams, command =>
            {
                using (var reader = this.actorBuilder.GetDataReader(command))
                {
                    try
                    {
                        action(reader);
                    }
                    finally
                    {
                        reader.Close();
                    }
                }
            });
        }
    }
}
