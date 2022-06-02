using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;
using ADO.NET.Fundamentals.Store.Library.Domain.Interfaces;

namespace ADO.NET.Fundamentals.Store.Console
{
    using ADO.NET.Fundamentals.Store.Console.RepositoryBuilders;

    public static class Programm
    {
        private static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Database=CoolStore;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";

        public static void Main()
        {
            //var builder = new EFRepositoryBuilder(connectionString);
            //var builder = new DapperRepositoryBuilder(connectionString);
            var builder = new DisconnectedAdoNetModelRepositoryBuilder(connectionString);

            IProductRepository productRepo = builder.BuiltProductRepository();
            //productRepo.Delete(new Product(2002, "3", "34", 22, 12, 12, 12));

            var productList = productRepo.GetAll();
            Commands.PrintList(productList, "All products");
            var product = productRepo.GetById(1);
            Commands.PrintItem(product, "One product");
            var newP = new Product(product.Id, "ksjhdflksajdfh", "akldjhflkawdjf", 1000, 1000, 1000, 1000);
            productRepo.Update(newP);
            Commands.PrintItem(productRepo.GetById(1).ToString(), "Updated product");

            //productRepo.Create(new Product(0, "2", null, 12, 12, 12, 12));
            //productRepo.Update(new Product(1002, "3", "34", 22, 12, 12, 12));

            IOrderRepository orderRepo = builder.BuildOrderRepository();
            orderRepo.Delete(new OrderFilterModel(null, null, null, 3));

            var orderList = orderRepo.Find(new OrderFilterModel(null, null, null, null));
            Commands.PrintList(orderList, "All orders");
            Commands.PrintItem(orderRepo.GetById(5), "One order");

            //orderRepo.Create(new Order(0, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
            //orderRepo.Update(new Order(1003, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
            //orderRepo.Delete(new Order(1003, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));


        }
    }
}
