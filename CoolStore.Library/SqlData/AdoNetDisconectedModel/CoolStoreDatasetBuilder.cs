using CoolStore.Library.SqlData.AdoNetDisconectedModel.CoolStoreDataSetTableAdapters;
using CoolStore.Library.SqlData.AdoNetDisconectedModel.Interfaces;
using System.Data.SqlClient;

namespace CoolStore.Library.SqlData.AdoNetDisconectedModel
{
    public class CoolStoreDatasetBuilder: ICoolStoreDatasetBuilder
    {
        private CoolStoreDataSet? dataset;

        public CoolStoreDataSet Build(ICoolStoreDbProvider provider)
        {
            if (dataset is null)
            {
                dataset = new CoolStoreDataSet();
                provider.Fill(dataset);
            }

            return dataset;
        }
    }
}
