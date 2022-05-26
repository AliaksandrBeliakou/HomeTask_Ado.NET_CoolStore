using CoolStore.Library.Repositotories;
using System.Data;
using System.Data.SqlClient;

namespace CoolStore.Library.UTests
{
    [TestFixture]
    public class ProductConnectedRepositoryTests
    {
        [Test]
        public void GetById_VeryBigId_DataException()
        {
            // Asset
            var mockReader = Mocks.ReaderWhitoutProduct;
            var mockBuilder = Mocks.GetConnectedDbActorsFactory(mockReader.Object);
            var repo = new ProductConnectedRepository("connection string", mockBuilder.Object);
            // Act, Assert
            Assert.Throws<DataException>(() => _ = repo.GetById(int.MaxValue));
        }

        [Test]
        public void GetById_One_CorrectData()
        {
            // Asset
            var mockReader = Mocks.ReaderOfSingleProduct;
            var mockBuilder = Mocks.GetConnectedDbActorsFactory(mockReader.Object);
            var repo = new ProductConnectedRepository("connection string", mockBuilder.Object);
            // Act
            var product = repo.GetById(1);
            // Assert
            product.Should().BeEquivalentTo(Stabs.Product1);
        }

        [Test]
        public void GetAll_VeryBigId_DataException()
        {
            // Asset
            var mockReader = Mocks.ReaderOfProductList;
            var mockBuilder = Mocks.GetConnectedDbActorsFactory(mockReader.Object);
            var repo = new ProductConnectedRepository("connection string", mockBuilder.Object);
            // Act
            var actualList = repo.GetAll();
            // Assert
            actualList.Should().BeEquivalentTo(Stabs.ProductList);
        }

        [Test]
        public void Create_CreatedProduct_CommandColl()
        {
            // Asset
            var mockCommand = new Mock<IDbCommand>();
            var mockBuilder = Mocks.GetConnectedDbActorsFactory(mockCommand.Object);
            var repo = new ProductConnectedRepository("connection string", mockBuilder.Object);
            var expectedParams = new List<IDbDataParameter>
            {
                new SqlParameter { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Size = 128, Value = Stabs.Product1.Name },
                new SqlParameter { ParameterName = "@Description", SqlDbType = SqlDbType.Text, Value = Stabs.Product1.Description },
                new SqlParameter { ParameterName = "@Weight", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Weight },
                new SqlParameter { ParameterName = "@Height", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Height },
                new SqlParameter { ParameterName = "@Width", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Width },
                new SqlParameter { ParameterName = "@Length", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Length },
            };
            // Act
            repo.Create(Stabs.Product1);
            // Assert
            mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
            mockBuilder.Verify(
                m => m.GetCommand(
                    It.IsAny<IDbConnection>(),
                    "INSERT INTO [dbo].[Products] VALUES(@Name, @Description, @Weight, @Height, @Width, @Length)",
                    CommandType.Text,
                    It.Is<IEnumerable<IDbDataParameter>>(x => x.All(p => expectedParams.Any(inp => inp.Value!.Equals(p.Value) && inp.ParameterName == p.ParameterName))))
                , Times.Once);
        }

        [Test]
        public void Update_ChangedProduct_CommandColl()
        {
            // Asset
            var mockCommand = new Mock<IDbCommand>();
            var mockBuilder = Mocks.GetConnectedDbActorsFactory(mockCommand.Object);
            var repo = new ProductConnectedRepository("connection string", mockBuilder.Object);
            var expectedParams = new List<IDbDataParameter>
            {
                new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Id },
                new SqlParameter { ParameterName = "@Name", SqlDbType = SqlDbType.NVarChar, Size = 128, Value = Stabs.Product1.Name },
                new SqlParameter { ParameterName = "@Description", SqlDbType = SqlDbType.Text, Value = Stabs.Product1.Description },
                new SqlParameter { ParameterName = "@Weight", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Weight },
                new SqlParameter { ParameterName = "@Height", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Height },
                new SqlParameter { ParameterName = "@Width", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Width },
                new SqlParameter { ParameterName = "@Length", SqlDbType = SqlDbType.Int, Value = Stabs.Product1.Length },
            };
            // Act
            repo.Update(Stabs.Product1);
            // Assert
            mockCommand.Verify(m => m.ExecuteNonQuery(), Times.Once);
            mockBuilder.Verify(
                m => m.GetCommand(
                    It.IsAny<IDbConnection>(),
                    "UPDATE [dbo].[Products] SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length WHERE Id = @Id",
                    CommandType.Text,
                    It.Is<IEnumerable<IDbDataParameter>>(x => x.All(p => expectedParams.Any(inp => inp.Value!.Equals(p.Value) && inp.ParameterName == p.ParameterName))))
                , Times.Once);
        }
    }
}
