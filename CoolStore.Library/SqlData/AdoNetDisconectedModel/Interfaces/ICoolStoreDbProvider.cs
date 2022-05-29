using static CoolStore.Library.SqlData.AdoNetDisconectedModel.CoolStoreDataSet;

namespace CoolStore.Library.SqlData.AdoNetDisconectedModel.Interfaces
{
    public interface ICoolStoreDbProvider
    {
        void Fill(CoolStoreDataSet dataset);
        void Update(OrdersDataTable table);
        void Update(ProductsDataTable table);
    }
}
