using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;

namespace ADO.NET.Fundamentals.Store.Library.Domain.Interfaces
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        void Create(Product product);
        Product GetById(int id);
        void Update(Product product);
        void Delete(Product product);
    }
}
