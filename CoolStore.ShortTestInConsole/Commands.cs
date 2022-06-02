namespace ADO.NET.Fundamentals.Store.Console
{
    using static System.Console;
    internal static class Commands
    {
        public static void PrintList<T>(IEnumerable<T> list, string message)
        {
            WriteLine(message);
            foreach (var item in list)
            {
                PrintItem(item);
            }
            WriteLine();
        }


        public static void PrintItem<T>(T item, string message)
        {
            WriteLine(message);
            PrintItem(item);
            WriteLine();
        }

        private static void PrintItem<T>(T item) => WriteLine(item?.ToString() ?? "Null value");
    }
}
