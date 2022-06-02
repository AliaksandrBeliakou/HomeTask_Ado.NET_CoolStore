using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.AdoNetDisconectedModel;
using static ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.AdoNetDisconectedModel.CoolStoreDataSet;

namespace ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.Interfaces
{
    public interface ICoolStoreDbProvider
    {
        void Fill(CoolStoreDataSet dataset);
        void Update(OrdersDataTable table);
        void Update(ProductsDataTable table);
    }
}
