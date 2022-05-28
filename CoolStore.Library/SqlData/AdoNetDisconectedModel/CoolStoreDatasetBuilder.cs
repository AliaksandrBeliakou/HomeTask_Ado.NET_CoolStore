using CoolStore.Library.SqlData.AdoNetDisconectedModel.CoolStoreDataSetTableAdapters;
using System.Data.SqlClient;

namespace CoolStore.Library.SqlData.AdoNetDisconectedModel
{
    public class CoolStoreDatasetBuilder
    {
        private CoolStoreDataSet dataset;

        public CoolStoreDataSet Build(string connectionString)
        {
            if (dataset is null)
            {
                dataset = new CoolStoreDataSet();
                using (var connection = new SqlConnection(connectionString))
                {
                    var adapterManager = new TableAdapterManager();
                    adapterManager.ProductsTableAdapter = new ProductsTableAdapter { Connection = connection };
                    adapterManager.ProductsTableAdapter.Fill(this.dataset.Products);
                    adapterManager.OrdersTableAdapter = new OrdersTableAdapter { Connection = connection };
                    adapterManager.OrdersTableAdapter.Fill(this.dataset.Orders);
                }
            }

            return dataset;
        }
    }
}
