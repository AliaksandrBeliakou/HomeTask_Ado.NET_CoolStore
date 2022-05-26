using CoolStore.Library.Models;

namespace CoolStore.Library.Interfaces
{
    public interface IOrderRepository
    {
        void Create(Order order);
        Product GetById(int id);
        void Update(Order order);
        IEnumerable<Order> Find(OrderFilterModel filter);
        void Delete(Order order);
        void Delete(OrderFilterModel filter);
    }
}
