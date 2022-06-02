using System.Data;

namespace ADO.NET.Fundamentals.Store.DapperReposies.Library.Interfaces
{
    public interface IConnectionBuilder
    {
        IDbConnection Build();
    }
}