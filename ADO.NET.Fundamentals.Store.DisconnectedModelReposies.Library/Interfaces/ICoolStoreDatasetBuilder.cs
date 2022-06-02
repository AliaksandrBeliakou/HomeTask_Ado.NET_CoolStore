using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.AdoNetDisconectedModel;

namespace ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.Interfaces
{
    public interface ICoolStoreDatasetBuilder
    {
        CoolStoreDataSet Build(ICoolStoreDbProvider provider);
    }
}
