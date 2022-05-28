using System.Data;

namespace CoolStore.Library.SqlData.Interfaces
{
    public interface IConnectedDbActorsFactory
    {
        IDbConnection GetConnection(string connectionString);
        IDbCommand GetCommand(IDbConnection connection, string command, CommandType commandType, IEnumerable<IDbDataParameter>? dataParametrs);
        IDataReader GetDataReader(IDbCommand command);
    }
}
