using ADO.NET.Fundamentals.Store.Console.RepositoryBuilders.Interfaces;
using ADO.NET.Fundamentals.Store.DapperReposies.Library;
using ADO.NET.Fundamentals.Store.DapperReposies.Library.Interfaces;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;

namespace ADO.NET.Fundamentals.Store.Console.RepositoryBuilders
{
    internal class DapperRepositoryBuilder : IRepositotyBuilder
    {
        private readonly IConnectionBuilder connectionBuilder;

        public DapperRepositoryBuilder(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            connectionBuilder = new ConnectionBuilder(connectionString);
        }
        public IOrderRepository BuildOrderRepository()
        {
            return new OrderDapperRepository(connectionBuilder);
        }

        public IProductRepository BuiltProductRepository()
        {
            return new ProductDapperRepository(connectionBuilder);
        }
    }
}
