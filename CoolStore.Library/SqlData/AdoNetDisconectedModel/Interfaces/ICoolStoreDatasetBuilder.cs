namespace CoolStore.Library.SqlData.AdoNetDisconectedModel.Interfaces
{
    public interface ICoolStoreDatasetBuilder
    {
        CoolStoreDataSet Build(ICoolStoreDbProvider provider);
    }
}
