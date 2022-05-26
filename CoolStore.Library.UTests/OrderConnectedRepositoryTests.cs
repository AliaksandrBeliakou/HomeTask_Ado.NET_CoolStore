using CoolStore.Library.Repositotories;
using System.Data;

namespace CoolStore.Library.UTests
{
    [TestFixture]
    internal class OrderConnectedRepositoryTests
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
            var mockReader = MockBuilder.ReaderOfSingleProduct;
            var mockBuilder = MockBuilder.GetConnectedDbActorsFactory(mockReader.Object);
            var repo = new OrderConnectedRepository("connection string", mockBuilder.Object);
            // Act
            var product = repo.GetById(1);
            // Assert
            product.Should().BeEquivalentTo(StabBuilder.Order1);
        }
    }
}
