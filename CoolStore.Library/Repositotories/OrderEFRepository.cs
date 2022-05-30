using CoolStore.Library.Helpers;
using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData.EntityFramework;

namespace CoolStore.Library.Repositotories
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
            context.Orders.Add(order.ToEntity());
            context.SaveChanges();
        }

        public void Delete(Order order)
        {
            var itemForDeletion = context.Orders.Single(x => x.Id == order.Id);
            context.Orders.Remove(itemForDeletion);
            context.SaveChanges();
        }

        public void Delete(OrderFilterModel filter)
        {
            foreach(var order in FindEntities(filter))
            {
                context.Orders.Remove(order);
            }
            context.SaveChanges();
        }

        public IEnumerable<Order> Find(OrderFilterModel filter)
        {
            return FindEntities(filter)
                .Select(x => x.ToModel());
        }

        public Order GetById(int id)
        {
            return context.Orders.Single(prop => prop.Id == id).ToModel();
        }

        public void Update(Order order)
        {
            var oldOrder = context.Orders.Single(prop => prop.Id == order.Id);
            oldOrder.Status = order.Status.ToString();
            oldOrder.CreateDate = order.CreateDate;
            oldOrder.UpdateDate = order.UpdateDate;
            oldOrder.ProductId = order.ProductId;
            context.SaveChanges();
        }

        private IEnumerable<CoolStore.Library.SqlData.EntityFramework.Models.Order> FindEntities(OrderFilterModel filter)
        {
            return context.Orders.Where(r =>
                (filter.Year == null || r.CreateDate.Year == filter.Year || r.UpdateDate.Year == filter.Year)
                && (filter.Month == null || r.CreateDate.Month == filter.Month || r.UpdateDate.Month == filter.Month)
                && (filter.Status == null || r.Status == filter.Status.ToString())
                && (filter.ProductId == null || r.ProductId == filter.ProductId));
        }
    }
}
