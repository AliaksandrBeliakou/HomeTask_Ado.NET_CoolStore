using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;

namespace ADO.NET.Fundamentals.Store.Library.Domain.Interfaces
{
    public interface IOrderRepository
    {
        void Add(Order order);
        Order GetById(int id);
        void Update(Order order);
        IEnumerable<Order> Find(OrderFilterModel filter);
        void Delete(Order order);
        void Delete(OrderFilterModel filter);
    }
}
