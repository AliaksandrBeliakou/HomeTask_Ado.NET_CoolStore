using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.AdoNetDisconectedModel;
using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.Interfaces;

namespace ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library
{
    public class CoolStoreDatasetBuilder : ICoolStoreDatasetBuilder
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
