using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData.AdoNetDisconectedModel;
using CoolStore.Library.SqlData.AdoNetDisconectedModel.CoolStoreDataSetTableAdapters;
using System.Data.SqlClient;
using static CoolStore.Library.SqlData.AdoNetDisconectedModel.CoolStoreDataSet;

namespace CoolStore.Library.Repositotories
{
    public class OrderDisconnectedRepository : IOrderRepository
    {
        private readonly string connectionString;
        private readonly CoolStoreDataSet dataset;


        public OrderDisconnectedRepository(CoolStoreDataSet dataset, string connectionString)
        {
            this.connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
            this.dataset = dataset ?? throw new ArgumentNullException(nameof(dataset));           
        }

        public void Create(Order order)
        {
            this.dataset.Orders.AddOrdersRow(order.Status.ToString(), order.CreateDate, order.UpdateDate, this.dataset.Products.FindById(order.ProductId));
            SaveChangesToDatabase();
        }

        public void Delete(Order order)
        {
            this.dataset.Orders.FindById(order.Id)?.Delete();
            SaveChangesToDatabase();
        }

        public void Delete(OrderFilterModel filter)
        {
            foreach(var row in GetFilteredRows(filter))
            {
                row.Delete();
            }
            SaveChangesToDatabase();
        }

        public IEnumerable<Order> Find(OrderFilterModel filter)
        {
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
            using (var connection = new SqlConnection(connectionString))
            {
                var adapter = new OrdersTableAdapter { Connection = connection };
                adapter.Update(this.dataset.Orders);
            }
        }
    }
}
