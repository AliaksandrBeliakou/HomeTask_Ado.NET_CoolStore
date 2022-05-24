using CoolStore.Library.Models;

namespace CoolStore.Library.Interfaces
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
