using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;

namespace ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library
{
    public class OrderEFRepository : IOrderRepository
    {
        private readonly CoolStoreContext context;

        public OrderEFRepository(CoolStoreContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Create(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            context.Orders.Add(order.ToEntity());
            context.SaveChanges();
        }

        public void Delete(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var itemForDeletion = context.Orders.Single(x => x.Id == order.Id);
            context.Orders.Remove(itemForDeletion);
            context.SaveChanges();
        }

        public void Delete(OrderFilterModel filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            foreach (var order in FindEntities(filter))
            {
                context.Orders.Remove(order);
            }
            context.SaveChanges();
        }

        public IEnumerable<Order> Find(OrderFilterModel filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return FindEntities(filter)
                .Select(x => x.ToModel());
        }

        public Order GetById(int id)
        {
            return context.Orders.Single(prop => prop.Id == id).ToModel();
        }

        public void Update(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var oldOrder = context.Orders.Single(prop => prop.Id == order.Id);
            oldOrder.Status = order.Status.ToString();
            oldOrder.CreateDate = order.CreateDate;
            oldOrder.UpdateDate = order.UpdateDate;
            oldOrder.ProductId = order.ProductId;
            context.SaveChanges();
        }

        private IEnumerable<ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library.Entities.Order> FindEntities(OrderFilterModel filter)
        {
            return context.Orders.Where(r =>
                (filter.Year == null || r.CreateDate.Year == filter.Year || r.UpdateDate.Year == filter.Year)
                && (filter.Month == null || r.CreateDate.Month == filter.Month || r.UpdateDate.Month == filter.Month)
                && (filter.Status == null || r.Status == filter.Status.ToString())
                && (filter.ProductId == null || r.ProductId == filter.ProductId));
        }
    }
}
