using ADO.NET.Fundamentals.Store.Console.RepositoryBuilders.Interfaces;
using ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;

namespace ADO.NET.Fundamentals.Store.Console.RepositoryBuilders
{
    internal class EFRepositoryBuilder : IRepositotyBuilder
    {
        private readonly CoolStoreContext context;

        public EFRepositoryBuilder(string connectionString)
        {
            context = new CoolStoreContext(connectionString);
        }
        public IOrderRepository BuildOrderRepository()
        {
            return new OrderEFRepository(context);
        }

        public IProductRepository BuiltProductRepository()
        {
            return new ProductEFRepository(context);
        }
    }
}
