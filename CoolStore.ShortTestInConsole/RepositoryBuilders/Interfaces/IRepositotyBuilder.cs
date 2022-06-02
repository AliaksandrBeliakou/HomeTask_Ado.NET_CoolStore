using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;

namespace ADO.NET.Fundamentals.Store.Console.RepositoryBuilders.Interfaces
{
    internal interface IRepositotyBuilder
    {
        IProductRepository BuiltProductRepository();
        IOrderRepository BuildOrderRepository();
    }
}
