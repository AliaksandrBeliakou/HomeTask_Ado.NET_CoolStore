using CoolStore.Library.Helpers;
using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData.AdoNetConnectedModel;
using CoolStore.Library.SqlData.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace CoolStore.Library.Repositotories
{
    public class OrderConnectedRepository : IOrderRepository
    {
        private readonly SqlExecuter executer;

        public OrderConnectedRepository(string connectionString, IConnectedDbActorsFactory factory)
        {
            this.executer = new SqlExecuter(
                connectionString ?? throw new ArgumentNullException(nameof(connectionString)),
                factory ?? throw new ArgumentNullException(nameof(factory)));
        }

        public void Create(Order order)
        {
            if(order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var sqlParameters = GetSqlParametersFromOrder(order, false);
            this.executer.GetNothing("INSERT INTO [dbo].[Orders] VALUES(@Status, @CreateDate, @UpdateDate, @ProductId)", 
                CommandType.Text, sqlParameters);
        }

        public void Delete(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = order.Id } };
            this.executer.GetNothing("DELETE FROM [dbo].[Orders] WHERE Id = @Id", CommandType.Text, parameters);
        }

        public void Delete(OrderFilterModel filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var sqlParameters = GetSqlParametersFromOrderFilter(filter);
            this.executer.GetNothing("DeleteOrdersByFilter", CommandType.StoredProcedure, sqlParameters);
        }

        public IEnumerable<Order> Find(OrderFilterModel filter)
        {
            if (filter is null)
            {
                throw new ArgumentNullException(nameof(filter));
            }

            var sqlParameters = GetSqlParametersFromOrderFilter(filter);
            return this.executer.GetList("GetOrdersByFilter", CommandType.StoredProcedure, sqlParameters, Mapper.GetOrder);
        }

        public Order GetById(int id)
        {
            var sqlParameters = new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id }.ToEnumerable();
            return this.executer.GetSingle("SELECT * FROM [dbo].[Orders] WHERE Id = @Id", CommandType.Text, sqlParameters, Mapper.GetOrder);
        }

        public void Update(Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            var sqlParameters = GetSqlParametersFromOrder(order, true);
            this.executer.GetNothing(
                "UPDATE [dbo].[Orders] SET Status = @Status, CreadteDate = @CreadteDate, UpdateDate = @UpdateDate, ProductId = @ProductId WHERE Id = @Id",
                CommandType.Text, sqlParameters);
        }

        private IEnumerable<SqlParameter> GetSqlParametersFromOrder(Order order, bool withIdFlag)
        {
            var result = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@Status", SqlDbType = SqlDbType.NVarChar, Size = 50, Value = order.Status.ToString() },
                new SqlParameter { ParameterName = "@CreateDate", SqlDbType = SqlDbType.Date, Value = order.CreateDate },
                new SqlParameter { ParameterName = "@UpdateDate", SqlDbType = SqlDbType.Date, Value = order.UpdateDate },
                new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = order.ProductId },
            };
            if (withIdFlag)
            {
                result.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = order.Id });
            }

            return result;
        }

        private IEnumerable<SqlParameter> GetSqlParametersFromOrderFilter(OrderFilterModel filter)
        {
            var result = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "@Status", SqlDbType = SqlDbType.NVarChar, Size = 50, IsNullable = true, Value = filter.Status.ToString() },
                new SqlParameter { ParameterName = "@Year", SqlDbType = SqlDbType.Int, IsNullable = true, Value = filter.Year },
                new SqlParameter { ParameterName = "@Month", SqlDbType = SqlDbType.Int, IsNullable = true, Value = filter.Month },
                new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, IsNullable = true, Value = filter.ProductId },
            };

            return result;
        }
    }
}
