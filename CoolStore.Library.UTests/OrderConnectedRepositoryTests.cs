using CoolStore.Library.Models;
using CoolStore.Library.Repositotories;
using System.Data;
using System.Data.SqlClient;

namespace CoolStore.Library.UTests
{
    [TestFixture]
    public partial class OrderConnectedRepositoryTests
    {
        private readonly static SqlParameterEqualityComparer sqlParameterEqualityComparer = new();
        [Test]
        public void GetById_VeryBigId_DataException()
        {
            // Asset
            var mockReader = MockBuilder.ReaderWhitoutData;
            var mockBuilder = MockBuilder.GetConnectedDbActorsFactory(mockReader.Object);
            var repo = new OrderConnectedRepository("connection string", mockBuilder.Object);
            // Act, Assert
            Assert.Throws<DataException>(() => _ = repo.GetById(int.MaxValue));
        }

        [Test]
        public void GetById_One_CorrectData()
        {
            // Asset
            var mockReader = MockBuilder.ReaderOfSingleOrder;
            var mockBuilder = MockBuilder.GetConnectedDbActorsFactory(mockReader.Object);
            var repo = new OrderConnectedRepository("connection string", mockBuilder.Object);
            // Act
            var order = repo.GetById(1);
            // Assert
            order.Should().BeEquivalentTo(StabBuilder.Order1);
        }

        [Test]
        public void Create_CreatedOrder_CommandColl()
        {
            // Asset
            var mockCommand = new Mock<IDbCommand>();
            var mockBuilder = MockBuilder.GetConnectedDbActorsFactory(mockCommand.Object);
            var repo = new OrderConnectedRepository("connection string", mockBuilder.Object);
            var expectedParams = new List<IDbDataParameter>
            {
                new SqlParameter { ParameterName = "@Status", SqlDbType = SqlDbType.NVarChar, Size = 50, Value = StabBuilder.Order1.Status.ToString() },
                new SqlParameter { ParameterName = "@CreateDate", SqlDbType = SqlDbType.Date, Value = StabBuilder.Order1.CreateDate },
                new SqlParameter { ParameterName = "@UpdateDate", SqlDbType = SqlDbType.Date, Value = StabBuilder.Order1.UpdateDate },
                new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = StabBuilder.Order1.ProductId },
            };
            // Act
            repo.Create(StabBuilder.Order1);
            // Assert
            mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
            mockBuilder.Verify(
                m => m.GetCommand(
                    It.IsAny<IDbConnection>(),
                    "INSERT INTO [dbo].[Orders] VALUES(@Status, @CreateDate, @UpdateDate, @ProductId)",
                    CommandType.Text,
                    It.Is<IEnumerable<IDbDataParameter>>(actualParams => sqlParameterEqualityComparer.Equals(actualParams, expectedParams)))
                , Times.Once);
        }

        [Test]
        public void Update_ChangedOrder_CommandColl()
        {
            // Asset
            var mockCommand = new Mock<IDbCommand>();
            var mockBuilder = MockBuilder.GetConnectedDbActorsFactory(mockCommand.Object);
            var repo = new OrderConnectedRepository("connection string", mockBuilder.Object);
            var expectedParams = new List<IDbDataParameter>
            {
                new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = StabBuilder.Order1.Id },
                new SqlParameter { ParameterName = "@Status", SqlDbType = SqlDbType.NVarChar, Size = 50, Value = StabBuilder.Order1.Status.ToString() },
                new SqlParameter { ParameterName = "@CreateDate", SqlDbType = SqlDbType.Date, Value = StabBuilder.Order1.CreateDate },
                new SqlParameter { ParameterName = "@UpdateDate", SqlDbType = SqlDbType.Date, Value = StabBuilder.Order1.UpdateDate },
                new SqlParameter { ParameterName = "@ProductId", SqlDbType = SqlDbType.Int, Value = StabBuilder.Order1.ProductId },
            };
            // Act
            repo.Update(StabBuilder.Order1);
            // Assert
            mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
            mockBuilder.Verify(
                m => m.GetCommand(
                    It.IsAny<IDbConnection>(),
                    "UPDATE [dbo].[Orders] SET Status = @Status, CreadteDate = @CreadteDate, UpdateDate = @UpdateDate, ProductId = @ProductId WHERE Id = @Id",
                    CommandType.Text,
                    It.Is<IEnumerable<IDbDataParameter>>(x => sqlParameterEqualityComparer.Equals(x, expectedParams)))
                , Times.Once);
        }

        [Test]
        public void Delete_ChangedOrder_CommandColl()
        {
            // Asset
            var mockCommand = new Mock<IDbCommand>();
            var mockBuilder = MockBuilder.GetConnectedDbActorsFactory(mockCommand.Object);
            var repo = new OrderConnectedRepository("connection string", mockBuilder.Object);
            var expectedParams = new List<IDbDataParameter>
            {
                new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = StabBuilder.Order1.Id },
            };
            // Act
            repo.Delete(StabBuilder.Order1);
            // Assert
            mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
            mockBuilder.Verify(
                m => m.GetCommand(
                    It.IsAny<IDbConnection>(),
                    "DELETE FROM [dbo].[Orders] WHERE Id = @Id",
                    CommandType.Text,
                    It.Is<IEnumerable<IDbDataParameter>>(x => sqlParameterEqualityComparer.Equals(x, expectedParams)))
                , Times.Once);
        }



        [Test]
        public void Find_OrderFilter_CommandCollAndOrderList()
        { // Asset


            // Asset
            var mockReader = MockBuilder.ReaderOfOrderList;
            var mockCommand = MockBuilder.GetCommandWithReader(mockReader.Object);
            var mockBuilder = MockBuilder.GetConnectedDbActorsFactory(mockCommand.Object);
            var repo = new OrderConnectedRepository("connection string", mockBuilder.Object);
            var expectedParams = GetSqlParametersFromOrderFilter(StabBuilder.OrderFilter1);
            // Act
            var list = repo.Find(StabBuilder.OrderFilter1);
            // Assert
            list.Should().BeEquivalentTo(StabBuilder.OrderList);
            mockBuilder.Verify(
                m => m.GetCommand(
                    It.IsAny<IDbConnection>(),
                    "GetOrdersByFilter",
                    CommandType.StoredProcedure,
                    It.Is<IEnumerable<IDbDataParameter>>(actualParams => sqlParameterEqualityComparer.Equals(actualParams, expectedParams)))
                , Times.Once);
        }


        [Test]
        public void Delete_OrderFilter_CommandColl()
        { // Asset


            // Asset
            var mockReader = MockBuilder.ReaderOfOrderList;
            var mockCommand = MockBuilder.GetCommandWithReader(mockReader.Object);
            var mockBuilder = MockBuilder.GetConnectedDbActorsFactory(mockCommand.Object);
            var repo = new OrderConnectedRepository("connection string", mockBuilder.Object);
            var expectedParams = GetSqlParametersFromOrderFilter(StabBuilder.OrderFilter1);
            // Act
            repo.Delete(StabBuilder.OrderFilter1);
            // Assert
            mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
            mockBuilder.Verify(
                m => m.GetCommand(
                    It.IsAny<IDbConnection>(),
                    "DeleteOrdersByFilter",
                    CommandType.StoredProcedure,
                    It.Is<IEnumerable<IDbDataParameter>>(actualParams => sqlParameterEqualityComparer.Equals(actualParams, expectedParams)))
                , Times.Once);
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
