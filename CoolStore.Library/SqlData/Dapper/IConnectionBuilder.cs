using System.Data;

namespace CoolStore.Library.SqlData.Dapper
{
    public interface IConnectionBuilder
    {
        IDbConnection Build(); 
    }
}