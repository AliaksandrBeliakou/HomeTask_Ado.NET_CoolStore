using ADO.NET.Fundamentals.Store.ConnectedModelReposies.Library;
using ADO.NET.Fundamentals.Store.ConnectedModelReposies.Library.Interfaces;
using ADO.NET.Fundamentals.Store.Console.RepositoryBuilders.Interfaces;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;

namespace ADO.NET.Fundamentals.Store.Console.RepositoryBuilders
{
    internal class ConnectedAdoNetModelRepositoryBuilder : IRepositotyBuilder
    {
        private readonly string connectionString;
        private readonly IConnectedDbActorsFactory factory;

        public ConnectedAdoNetModelRepositoryBuilder(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            this.factory = new SqlConnectedModelActorsFactory();
        }
        public IOrderRepository BuildOrderRepository()
        {
            return new OrderConnectedRepository(this.connectionString, this.factory);
        }

        public IProductRepository BuiltProductRepository()
        {
            return new ProductConnectedRepository(this.connectionString, this.factory);
        }
    }
}
