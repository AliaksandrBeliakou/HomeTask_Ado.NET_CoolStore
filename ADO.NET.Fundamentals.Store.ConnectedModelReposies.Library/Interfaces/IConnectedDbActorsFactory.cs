using System.Data;

namespace ADO.NET.Fundamentals.Store.ConnectedModelReposies.Library.Interfaces
{
    public interface IConnectedDbActorsFactory
    {
        IDbConnection GetConnection(string connectionString);
        IDbCommand GetCommand(IDbConnection connection, string command, CommandType commandType, IEnumerable<IDbDataParameter>? dataParametrs);
        IDataReader GetDataReader(IDbCommand command);
    }
}
