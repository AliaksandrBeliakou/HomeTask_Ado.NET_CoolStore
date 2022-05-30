using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.Repositotories;
using CoolStore.Library.SqlData.EntityFramework;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");
var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Database=CoolStore;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
var optionsBuilder = new DbContextOptionsBuilder<CoolStoreContext>();

var options = optionsBuilder.UseSqlServer(connectionString).Options;
var dbContext = new CoolStoreContext();
//var connectionBuilder = new ConnectionBuilder(connectionString);
//var provider = new CoolStoreDbProvider(connectionString);
//var dataset = new CoolStoreDatasetBuilder().Build(provider);
//IProductRepository productRepo = new ProductDisconnectedRepository(dataset, provider);
IProductRepository productRepo = new ProductEFRepository(dbContext);

//productRepo.Delete(new Product(2002, "3", "34", 22, 12, 12, 12));
Console.WriteLine("All products");
var productList = productRepo.GetAll().ToList();
foreach (var product in productList)
    Console.WriteLine(product.ToString());

Console.WriteLine("\nOne product");
var pr = productRepo.GetById(1);
Console.WriteLine(pr.ToString());

var newP = new Product(pr.Id, "ksjhdflksajdfh", "akldjhflkawdjf", 1000, 1000, 1000, 1000);
productRepo.Update(newP);
Console.WriteLine(productRepo.GetById(1).ToString());



//productRepo.Create(new Product(0, "2", null, 12, 12, 12, 12));
//productRepo.Update(new Product(1002, "3", "34", 22, 12, 12, 12));

IOrderRepository orderRepo = new OrderEFRepository(dbContext);
//var filter = new OrderFilterModel(OrderStatus.Loading, null, null, 3);
var filter = new OrderFilterModel(null, null, null, 3);
orderRepo.Delete(new OrderFilterModel(null, null, null, 3));

Console.WriteLine("All products");
var orderList = orderRepo.Find(new OrderFilterModel(null, null, null, null));
foreach (var order in orderList)
    Console.WriteLine(order.ToString());

Console.WriteLine("\nOne product");
Console.WriteLine(orderRepo.GetById(10).ToString());

//orderRepo.Create(new Order(0, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
//orderRepo.Update(new Order(1003, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
//orderRepo.Delete(new Order(1003, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));

