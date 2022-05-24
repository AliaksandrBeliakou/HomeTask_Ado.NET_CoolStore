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
            product.Should().BeEquivalentTo(Stabs.Product1);
        }

        [Test]
        public void GetAll_VeryBigId_DataException()
        {
            // Asset
            var mockReader = Mocks.ReaderWhitoutProduct;
            var mockBuilder = Mocks.GetConnectedDbActorsFactory(mockReader.Object);
            var repo = new ProductConnectedRepository("connection string", mockBuilder.Object);
            // Act
            var actualList = repo.GetAll();
            // Assert
            actualList.Should().BeEquivalentTo(Stabs.ProductList);
        }
    }
}
