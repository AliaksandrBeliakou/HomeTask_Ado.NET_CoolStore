using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;
using System.Linq.Expressions;
using OrderEntity = ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library.Entities.Order;

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

        private IEnumerable<OrderEntity> FindEntities(OrderFilterModel filter)
        {
            var filterExprettion = buildFilterTree(filter);
            if(filterExprettion == null)
            {
                return context.Orders;
            }

            return context.Orders.Where(filterExprettion);
            //return context.Orders.Where(r =>
            //    (filter.Year == null || r.CreateDate.Year == filter.Year || r.UpdateDate.Year == filter.Year)
            //    && (filter.Month == null || r.CreateDate.Month == filter.Month || r.UpdateDate.Month == filter.Month)
            //    && (filter.Status == null || r.Status == filter.Status.ToString())
            //    && (filter.ProductId == null || r.ProductId == filter.ProductId));
        }

        private Expression<Func<OrderEntity, bool>>? buildFilterTree(OrderFilterModel filter)
        {
            Expression? result = null;
            var innerParameter = Expression.Parameter(typeof(OrderEntity), "y");

            var expressions = new List<Expression<Func<OrderEntity, bool>>>();
            if (filter.Year.HasValue)
            {
                var yearConstant = Expression.Constant(filter.Year, typeof(int));
                result = Expression.Or(
                    Expression.Equal(Expression.Property(Expression.Property(innerParameter, "CreateDate"), "Year"), yearConstant),
                    Expression.Equal(Expression.Property(Expression.Property(innerParameter, "UpdateDate"), "Year"), yearConstant));
            }
            if (filter.Month.HasValue)
            {
                var monthConstant = Expression.Constant(filter.Month, typeof(int));
                var monthResult = Expression.Or(
                    Expression.Equal(Expression.Property(Expression.Property(innerParameter, "CreateDate"), "Year"), monthConstant),
                    Expression.Equal(Expression.Property(Expression.Property(innerParameter, "UpdateDate"), "Year"), monthConstant));
                result = result == null? monthResult :
                    Expression.And(result, monthResult);
            }
            if (filter.Status.HasValue)
            {
                var statusConstant = Expression.Call(Expression.Constant(filter.Status, typeof(OrderStatus)), "ToString", Type.EmptyTypes);
                var statusResult = Expression.Equal(Expression.Property(innerParameter, "Status"), statusConstant);
                result = result == null ? statusResult :
                    Expression.And(result, statusResult);
            }
            if (filter.ProductId.HasValue)
            {
                var productIdConstant = Expression.Constant(filter.ProductId, typeof(int));
                var productIdResult = Expression.Equal(Expression.Property(innerParameter, "ProductId"), productIdConstant);
                result = result == null ? productIdResult :
                    Expression.And(result, productIdResult);
            }

            if(result == null)
                return null;
            return Expression.Lambda<Func<OrderEntity, bool>>(result, innerParameter);
        }
    }
}
