using CoolStore.Library.Models;
using System.Data;

namespace CoolStore.Library.Helpers
{
    public static class Mapper
    {
        public static IEnumerable<T> ToEnumerable<T>(this T item)
        {
            yield return item;
        }

        public static Product GetProduct(this IDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            
            return new Product(
                reader.GetInt32(0),
                reader.GetString(1),
                reader.GetString(2),
                reader.GetInt32(3),
                reader.GetInt32(4),
                reader.GetInt32(5),
                reader.GetInt32(6));
        }
    }
}
