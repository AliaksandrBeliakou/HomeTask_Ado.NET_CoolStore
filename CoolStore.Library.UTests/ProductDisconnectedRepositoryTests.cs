using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library;
using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using System.Data;

namespace ADO.NET.Fundamentals.Store.Library.UTests
{
    [TestFixture]
    public class ProductDisconnectedRepositoryTests
    {
        [Test]
        public void GetById_VeryBigId_DataException()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var repo = new ProductDisconnectedRepository(StabBuilder.CoolStoreDataSet, mockProvide.Object);
            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = repo.GetById(int.MaxValue));
        }

        [Test]
        public void GetById_First_CorrectData()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var repo = new ProductDisconnectedRepository(StabBuilder.CoolStoreDataSet, mockProvide.Object);
            int id = StabBuilder.CoolStoreDataSet.Products.First().Id;
            // Act
            var product = repo.GetById(id);
            // Assert
            product.Should().BeEquivalentTo(StabBuilder.Product1);
        }

        [Test]
        public void GetAll_VeryBigId_DataException()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var repo = new ProductDisconnectedRepository(StabBuilder.CoolStoreDataSet, mockProvide.Object);
            // Act
            var actualList = repo.GetAll();
            // Assert
            actualList.Should().BeEquivalentTo(StabBuilder.ProductList);
        }

        [Test]
        public void Create_CreatedProduct_CommandColl()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var count = dataset.Products.Count;
            var repo = new ProductDisconnectedRepository(dataset, mockProvide.Object);

            // Act
            repo.Add(StabBuilder.Product1);
            // Assert
            mockProvide.Verify(m => m.Update(dataset.Products), Times.Once);
            dataset.Products.Count.Should().Be(count + 1);
        }

        [Test]
        public void Update_ChangedProduct_CommandColl()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var repo = new ProductDisconnectedRepository(dataset, mockProvide.Object);
            var itemForChange = dataset.Products.Select(x => new Product(x.Id, "BigComputer", x.Description, x.Weight, x.Height, x.Width, x.Length)).Last();
            // Act
            repo.Update(itemForChange);
            // Assert
            mockProvide.Verify(m => m.Update(dataset.Products), Times.Once);
            dataset.Products[2].RowState.Should().Be(DataRowState.Modified);
        }

        [Test]
        public void Delete_ChangedProduct_CommandColl()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var repo = new ProductDisconnectedRepository(dataset, mockProvide.Object);
            var itemForDeletion = dataset.Products.Select(x => new Product(x.Id, x.Name, x.Description, x.Weight, x.Height, x.Width, x.Length)).Last();
            // Act
            repo.Delete(itemForDeletion);
            // Assert
            mockProvide.Verify(m => m.Update(dataset.Products), Times.Once);
            dataset.Products[2].RowState.Should().Be(DataRowState.Deleted);
        }
    }
}
