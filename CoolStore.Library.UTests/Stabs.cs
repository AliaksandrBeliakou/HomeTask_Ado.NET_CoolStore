using CoolStore.Library.Models;

namespace CoolStore.Library.UTests
{
    public static class Stabs
    {
        public static Product Product1 { get; } = new Product(1, "Super computer", "Calculate really fast", 100000, 2000, 400, 700);
        public static Product Product2 { get; } = new Product(2, "Developer station", "For apps creation", 10000, 700, 20, 500);
        public static Product Product3 { get; } = new Product(3, "Play station 5", "Gate to entertainment", 5000, 400, 20, 30);
        public static IEnumerable<Product> ProductList { get; } = new Product[] { Product1, Product2, Product3 };    

    }
}
