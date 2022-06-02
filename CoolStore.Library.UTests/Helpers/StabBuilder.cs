using ADO.NET.Fundamentals.Store.DisconnectedModelReposies.Library.AdoNetDisconectedModel;
using ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects;

namespace ADO.NET.Fundamentals.Store.Library.UTests.Helpers
{
    public static class StabBuilder
    {
        // Products
        public static Product Product1 { get; } = new Product(1, "Super computer", "Calculate really fast", 100000, 2000, 400, 700);
        public static Product Product2 { get; } = new Product(2, "Developer station", "For apps creation", 10000, 700, 20, 500);
        public static Product Product3 { get; } = new Product(3, "Play station 5", "Gate to entertainment", 5000, 400, 20, 30);
        public static IEnumerable<Product> ProductList { get; } = new Product[] { Product1, Product2, Product3 };
        //Orders
        public static Order Order1 { get; } = new Order(1, OrderStatus.Done, new DateTime(2020, 8, 5), new DateTime(2021, 5, 26), 1);
        public static Order Order2 { get; } = new Order(2, OrderStatus.InProgress, new DateTime(2021, 8, 5), new DateTime(2021, 5, 26), 1);
        public static IEnumerable<Order> OrderList { get; } = new Order[] { Order1, Order2 };

        //OrderFilters
        public static OrderFilterModel OrderFilter1 { get; } = new OrderFilterModel(OrderStatus.Done, 2021, null, null);
        public static OrderFilterModel OrderFilter2 { get; } = new OrderFilterModel(OrderStatus.InProgress, null, null, 1);

        public static CoolStoreDataSet CoolStoreDataSet
        {
            get
            {
                var dataset = new CoolStoreDataSet();
                dataset.Products.AddProductsRow(Product1.Name, Product1.Description, Product1.Weight, Product1.Height, Product1.Width, Product1.Length);
                dataset.Products.AddProductsRow(Product2.Name, Product2.Description, Product2.Weight, Product2.Height, Product2.Width, Product2.Length);
                dataset.Products.AddProductsRow(Product3.Name, Product3.Description, Product3.Weight, Product3.Height, Product3.Width, Product3.Length);
                dataset.Orders.AddOrdersRow(Order1.Status.ToString(), Order1.CreateDate, Order1.UpdateDate, dataset.Products[1]);
                dataset.AcceptChanges();
                return dataset;
            }
        }
    }
}
