using ADO.NET.Fundamentals.Store.Console.RepositoryBuilders.Interfaces;
using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library;
using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.AdoNetDisconectedModel;
using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.Interfaces;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;

namespace ADO.NET.Fundamentals.Store.Console.RepositoryBuilders
{
    internal class DisconnectedAdoNetModelRepositoryBuilder : IRepositotyBuilder
    {
        private readonly CoolStoreDataSet dataset;
        private readonly ICoolStoreDbProvider provider;

        public DisconnectedAdoNetModelRepositoryBuilder(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            this.provider = new CoolStoreDbProvider(connectionString);
            this.dataset = new CoolStoreDatasetBuilder().Build(this.provider);
        }
        public IOrderRepository BuildOrderRepository()
        {
            return new OrderDisconnectedRepository(this.dataset, this.provider);
        }

        public IProductRepository BuiltProductRepository()
        {
            return new ProductDisconnectedRepository(this.dataset, this.provider);
        }
    }
}
