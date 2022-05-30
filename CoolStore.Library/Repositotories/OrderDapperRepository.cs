using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData.Dapper;
using Dapper;
using System.Data;

namespace CoolStore.Library.Repositotories
{
    public class OrderDapperRepository: IOrderRepository
    {
        private readonly IConnectionBuilder connectionBuilder;

        public OrderDapperRepository(IConnectionBuilder connectionBuilder)
        {
            Dapper.SqlMapper.AddTypeMap(typeof(OrderStatus), DbType.String);
            this.connectionBuilder = connectionBuilder ?? throw new ArgumentNullException(nameof(connectionBuilder));
        }

        public void Create(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            using var connection = connectionBuilder.Build();
            connection.Open();
            connection.Execute("INSERT INTO [dbo].[Orders] VALUES(@Status, @CreateDate, @UpdateDate, @ProductId)",
                new { Status = order.Status, order.CreateDate, order.UpdateDate, order.ProductId });
        }

        public void Delete(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            using var connection = connectionBuilder.Build();
            connection.Open();
            connection.Execute("DELETE FROM [dbo].[Orders] WHERE Id = @Id", new { order.Id });
        }

        public void Delete(OrderFilterModel filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            using var connection = connectionBuilder.Build();
            connection.Open();
            connection.Execute(
                "DeleteOrdersByFilter",
                new { filter.Year, filter.Month, filter.Status, filter.ProductId },
                commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<Order> Find(OrderFilterModel filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            using var connection = connectionBuilder.Build();
            connection.Open();
            return connection.Query<Order>(
                "GetOrdersByFilter", 
                new { filter.Year, filter.Month, filter.Status, filter.ProductId },
                commandType: CommandType.StoredProcedure);
        }

        public Order GetById(int id)
        {
            using var connection = connectionBuilder.Build();
            connection.Open();
            return connection.QuerySingle<Order>("SELECT * FROM [dbo].[Orders] WHERE Id = @Id", new { Id = id});
        }

        public void Update(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            using var connection = connectionBuilder.Build();
            connection.Open();
            connection.Execute("UPDATE [dbo].[Orders] SET Status = @Status, CreadteDate = @CreadteDate, UpdateDate = @UpdateDate, ProductId = @ProductId WHERE Id = @Id",
                new { order.Id, order.Status, order.CreateDate, order.UpdateDate, order.ProductId });
        }
    }
}
