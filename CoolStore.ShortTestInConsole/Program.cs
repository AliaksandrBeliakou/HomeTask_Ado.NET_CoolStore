﻿using CoolStore.Library.Interfaces;
using CoolStore.Library.Models;
using CoolStore.Library.Repositotories;
using CoolStore.Library.SqlData.AdoNetDisconectedModel;

Console.WriteLine("Hello, World!");
var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Database=CoolStore;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Connect Timeout=60;Encrypt=False;TrustServerCertificate=False";
var dataset = new CoolStoreDatasetBuilder().Build(connectionString);
IProductRepository productRepo = new ProductDisconnectedRepository(dataset, connectionString);

Console.WriteLine("All products");
var productList = productRepo.GetAll();
foreach (var product in productList)
    Console.WriteLine(product.ToString());

Console.WriteLine("\nOne product");
Console.WriteLine(productRepo.GetById(1).ToString());

//productRepo.Create(new Product(0, "2", null, 12, 12, 12, 12));
//productRepo.Update(new Product(1002, "3", "34", 22, 12, 12, 12));
//productRepo.Delete(new Product(1002, "3", "34", 22, 12, 12, 12));

IOrderRepository orderRepo = new OrderDisconnectedRepository(dataset, connectionString);
var filter = new OrderFilterModel(OrderStatus.Loading, null, null, 3);
Console.WriteLine("All products");
var orderList = orderRepo.Find(filter);
foreach (var order in orderList)
    Console.WriteLine(order.ToString());

//Console.WriteLine("\nOne product");
//Console.WriteLine(orderRepo.GetById(1).ToString());

//orderRepo.Create(new Order(0, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
//orderRepo.Update(new Order(1003, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
//orderRepo.Delete(new Order(1003, OrderStatus.Loading, new DateTime(1999, 1, 1), new DateTime(2021, 1, 1), 3));
//orderRepo.Delete(filter);

