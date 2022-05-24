using CoolStore.Library.Models;
using CoolStore.Library.Repositotories;
using System.Data;

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
            product.Should().BeEquivalentTo(new Product(1, "Super computer", "Calculate really fast", 100000, 2000, 400, 700));
        }
    }
}
