using CoolStore.Library.Models;

namespace CoolStore.Library.UTests.Helpers
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
    }
}
