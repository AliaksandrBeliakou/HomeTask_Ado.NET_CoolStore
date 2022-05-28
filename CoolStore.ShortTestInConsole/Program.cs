using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.Repositotories;
using CoolStore.Library.SqlData.AdoNetDisconectedModel;

Console.WriteLine("Hello, World!");
var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Database=CoolStore;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
var dataset = new CoolStoreDataSet();
//IProductRepository repo = new ProductDisconnectedRepository(dataset, connectionString);

//Console.WriteLine("All products");
//var productList = repo.GetAll();
//foreach (var product in productList)
//    Console.WriteLine(product.ToString());

//Console.WriteLine("\nOne product");
//Console.WriteLine(repo.GetById(1).ToString());

//repo.Create(new CoolStore.Library.Models.Product(0, "2", null, 12, 12, 12, 12));
//repo.Update(new CoolStore.Library.Models.Product(1002, "3", "34", 22, 12, 12, 12));
//repo.Delete(new CoolStore.Library.Models.Product(1002, "3", "34", 22, 12, 12, 12));

IOrderRepository repo = new OrderDisconnectedRepository(dataset, connectionString);
var filter = new OrderFilterModel(null, null, null, 1);
Console.WriteLine("All products");
var orderList = repo.Find(filter);
foreach (var order in orderList)
    Console.WriteLine(order.ToString());

//Console.WriteLine("\nOne product");
//Console.WriteLine(repo.GetById(1).ToString());

//repo.Create(new Order(0, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
//repo.Update(new Order(1003, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
//repo.Delete(new Order(1003, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
repo.Delete(filter);

