using CoolStore.Library.Models;

namespace CoolStore.Library.Interfaces
{
    public interface IOrderRepository
    {
        void Create(Order product);
        Product GetById(int id);
        void Update(Order product);
        IEnumerable<Order> Find(OrderFilterModel filter);
        void Delete(Order product);
        void Delete(OrderFilterModel filter);
    }
}
