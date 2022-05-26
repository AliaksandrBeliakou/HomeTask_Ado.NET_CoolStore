﻿using CoolStore.Library.Helpers;
using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.SqlData;
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
            var sqlParameters = GetSqlParametersFromOrder(order, false);
            this.executer.GetNothing("INSERT INTO [dbo].[Orders] VALUES(@Status, @CreateDate, @UpdateDate, @ProductId)", 
                CommandType.Text, sqlParameters);
        }

        public void Delete(Order order)
        {
            var parameters = new List<SqlParameter> { new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = order.Id } };
            this.executer.GetNothing("DELETE FROM [dbo].[Orders] WHERE Id = @Id", CommandType.Text, parameters);
        }

        public void Delete(OrderFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Order> Find(OrderFilterModel filter)
        {
            throw new NotImplementedException();
        }

        public Order GetById(int id)
        {
            var sqlParameters = new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id }.ToEnumerable();
            return this.executer.GetSingle("SELECT * FROM [dbo].[Orders] WHERE Id = @Id", CommandType.Text, sqlParameters, Mapper.GetOrder);
        }

        public void Update(Order order)
        {
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
    }
}
