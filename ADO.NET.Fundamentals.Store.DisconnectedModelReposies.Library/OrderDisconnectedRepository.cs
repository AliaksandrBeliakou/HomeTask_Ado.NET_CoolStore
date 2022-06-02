using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.AdoNetDisconectedModel;
using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.Interfaces;
using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;
using static ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.AdoNetDisconectedModel.CoolStoreDataSet;

namespace ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library
{
    public class OrderDisconnectedRepository : IOrderRepository
    {
        private readonly CoolStoreDataSet dataset;
        private readonly ICoolStoreDbProvider provider;


        public OrderDisconnectedRepository(CoolStoreDataSet dataset, ICoolStoreDbProvider provider)
        {
            this.dataset = dataset ?? throw new ArgumentNullException(nameof(dataset));           
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));           
        }

        public void Add(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            this.dataset.Orders.AddOrdersRow(order.Status.ToString(), order.CreateDate, order.UpdateDate, this.dataset.Products.FindById(order.ProductId));
            SaveChangesToDatabase();
        }

        public void Delete(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            this.dataset.Orders.FindById(order.Id)?.Delete();
            SaveChangesToDatabase();
        }

        public void Delete(OrderFilterModel filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            foreach (var row in GetFilteredRows(filter))
            {
                row.Delete();
            }
            SaveChangesToDatabase();
        }

        public IEnumerable<Order> Find(OrderFilterModel filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            return GetFilteredRows(filter)
                .Select(r => new Order(r.Id, Enum.Parse<OrderStatus>(r.Status, true), r.CreateDate, r.UpdateDate, r.ProductId));
        }

        public Order GetById(int id)
        {
            var row = dataset.Orders.FindById(id) ?? throw new ArgumentOutOfRangeException($"Order with id equal to {id} is not exist");
            return new Order(row.Id, Enum.Parse<OrderStatus>(row.Status, true), row.CreateDate, row.UpdateDate, row.ProductId);
        }

        public void Update(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var orderDataRow = dataset.Orders.FindById(order.Id) ?? throw new ArgumentOutOfRangeException($"Order with id equal to {order.Id} is not exist");
            orderDataRow.Status = order.Status.ToString();
            orderDataRow.CreateDate = order.CreateDate;
            orderDataRow.UpdateDate = order.UpdateDate;
            orderDataRow.ProductId = order.ProductId;
            SaveChangesToDatabase();
        }

        private IEnumerable<OrdersRow> GetFilteredRows(OrderFilterModel filter)
        {
            return this.dataset.Orders.Where(r =>
                (filter.Year is null || r.CreateDate.Year == filter.Year || r.UpdateDate.Year == filter.Year)
                && (filter.Month is null || r.CreateDate.Month == filter.Month || r.UpdateDate.Month == filter.Month)
                && (filter.Status is null || r.Status == filter.Status.ToString())
                && (filter.ProductId is null || r.ProductId == filter.ProductId));
        }

        private void SaveChangesToDatabase()
        {
            this.provider.Update(this.dataset.Orders);
        }
    }
}
