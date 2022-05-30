using CoolStore.Library.SqlData.AdoNetDisconectedModel.CoolStoreDataSetTableAdapters;
using CoolStore.Library.SqlData.AdoNetDisconectedModel.Interfaces;
using System.Data.SqlClient;

namespace CoolStore.Library.SqlData.AdoNetDisconectedModel
{
    public class CoolStoreDbProvider : ICoolStoreDbProvider
    {
        private readonly string connectionString;

        public CoolStoreDbProvider(string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public void Fill(CoolStoreDataSet dataset)
        {
            if (dataset is null)
            {
                throw new ArgumentNullException(nameof(dataset));
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var adapterManager = new TableAdapterManager();
                adapterManager.ProductsTableAdapter = new ProductsTableAdapter { Connection = connection };
                adapterManager.ProductsTableAdapter.Fill(dataset.Products);
                adapterManager.OrdersTableAdapter = new OrdersTableAdapter { Connection = connection };
                adapterManager.OrdersTableAdapter.Fill(dataset.Orders);
            }
        }

        public void Update(CoolStoreDataSet.OrdersDataTable table)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var adapter = new OrdersTableAdapter { Connection = connection };
                adapter.Update(table);
            }
        }

        public void Update(CoolStoreDataSet.ProductsDataTable table)
        {
            if (table is null)
            {
                throw new ArgumentNullException(nameof(table));
            }

            using (var connection = new SqlConnection(connectionString))
            {
                var adapter = new ProductsTableAdapter { Connection = connection };
                adapter.Update(table);
            }
        }
    }
}
