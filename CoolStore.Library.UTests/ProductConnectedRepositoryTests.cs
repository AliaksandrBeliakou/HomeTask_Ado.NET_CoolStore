using CoolStore.Library.Models;
using CoolStore.Library.Repositotories;
using CoolStore.Library.SqlData;
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
            var repo = new ProductConnectedRepository(Env.ConnectionString, new SqlConnectedModelActorsFactory());
            // Act, Assert
            Assert.Throws<DataException>(() => _ = repo.GetById(int.MaxValue));
        }

        [Test]
        public void GetById_One_CorrectData()
        {
            // Asset
            var repo = new ProductConnectedRepository(Env.ConnectionString, new SqlConnectedModelActorsFactory());
            // Act
            var product = repo.GetById(1);
            // Assert
            product.Should().BeEquivalentTo(new Product(1, "Super computer", "Calculate really fast", 100000, 2000, 400, 700));
        }
    }
}
