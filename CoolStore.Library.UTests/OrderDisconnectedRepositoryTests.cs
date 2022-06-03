using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library;
using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using System.Data;

namespace ADO.NET.Fundamentals.Store.Library.UTests
{
    [TestFixture]
    public class OrderDisconnectedRepositoryTests
    {
        [Test]
        public void GetById_VeryBigId_DataException()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var repo = new OrderDisconnectedRepository(StabBuilder.CoolStoreDataSet, mockProvide.Object);
            // Act, Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _ = repo.GetById(int.MaxValue));
        }

        [Test]
        public void GetById_First_CorrectData()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var repo = new OrderDisconnectedRepository(StabBuilder.CoolStoreDataSet, mockProvide.Object);
            int id = StabBuilder.CoolStoreDataSet.Products.First().Id;
            // Act
            var product = repo.GetById(id);
            // Assert
            product.Should().BeEquivalentTo(StabBuilder.Order1);
        }

        [Test]
        public void Create_CreatedProduct_CommandColl()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var count = dataset.Orders.Count;
            var repo = new OrderDisconnectedRepository(dataset, mockProvide.Object);

            // Act
            repo.Add(StabBuilder.Order1);
            // Assert
            mockProvide.Verify(m => m.Update(dataset.Orders), Times.Once);
            dataset.Orders.Count.Should().Be(count + 1);
        }

        [Test]
        public void Update_ChangedProduct_CommandColl()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var repo = new OrderDisconnectedRepository(dataset, mockProvide.Object);
            var itemForChange = dataset.Orders.Select(x => new Order(x.Id, OrderStatus.NotStarted, x.CreateDate, x.UpdateDate, x.ProductId)).Last();
            // Act
            repo.Update(itemForChange);
            // Assert
            mockProvide.Verify(m => m.Update(dataset.Orders), Times.Once);
            dataset.Orders[1].RowState.Should().Be(DataRowState.Modified);
        }

        [Test]
        public void Delete_ChangedProduct_CommandColl()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var repo = new OrderDisconnectedRepository(dataset, mockProvide.Object);
            var itemForDeletion = dataset.Orders.Select(x => new Order(x.Id, Enum.Parse<OrderStatus>(x.Status, true), x.CreateDate, x.UpdateDate, x.ProductId)).Last();
            // Act
            repo.Delete(itemForDeletion);
            // Assert
            mockProvide.Verify(m => m.Update(dataset.Orders), Times.Once);
            dataset.Orders[1].RowState.Should().Be(DataRowState.Deleted);
        }

        [Test]
        public void Find_StrongOrderFilter_EmptyList()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var repo = new OrderDisconnectedRepository(dataset, mockProvide.Object);
            // Act
            var list = repo.Find(new OrderFilterModel(OrderStatus.NotStarted, 1000, 1, 1000));
            // Assert
            list.Should().BeEquivalentTo(Array.Empty<Order>());
        }

        [Test]
        public void Find_SWeakOrderFilter_EmptyList()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var repo = new OrderDisconnectedRepository(dataset, mockProvide.Object);
            // Act
            var list = repo.Find(new OrderFilterModel(null, 2021, null, null));
            // Assert
            list.Should().BeEquivalentTo(StabBuilder.OrderList);
        }

        [Test]
        public void Delete_OrderFilter_CommandColl()
        {
            // Asset
            var mockProvide = MockBuilder.CoolStoreDbProvider;
            var dataset = StabBuilder.CoolStoreDataSet;
            var repo = new OrderDisconnectedRepository(dataset, mockProvide.Object);
            // Act
            repo.Delete(new OrderFilterModel(null, 2021, null, null));
            // Assert
            foreach (var order in dataset.Orders)
            {
                order.RowState.Should().Be(DataRowState.Deleted);
            }
        }
    }
}
