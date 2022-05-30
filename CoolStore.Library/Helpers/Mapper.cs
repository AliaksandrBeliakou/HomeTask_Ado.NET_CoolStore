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

        public static Order GetOrder(this IDataReader reader)
        {
            if (reader is null)
            {
                throw new ArgumentNullException(nameof(reader));
            }
            var statusString = reader.GetString(1);
            var status = Enum.Parse<OrderStatus>(statusString, true);
            return new Order(
                reader.GetInt32(0),
                status,
                reader.GetDateTime(2),
                reader.GetDateTime(3),
                reader.GetInt32(4));
        }

        public static Product ToModel(this CoolStore.Library.SqlData.EntityFramework.Models.Product product)
        {
            return new Product(product.Id, product.Name, product.Description, product.Weight, product.Height, product.Width, product.Length);
        }

        public static CoolStore.Library.SqlData.EntityFramework.Models.Product ToEntity(this Product product)
        {
            return new CoolStore.Library.SqlData.EntityFramework.Models.Product
            {
                Id = product.Id, 
                Name = product.Name, 
                Description = product.Description, 
                Weight = product.Weight, 
                Height = product.Height, 
                Width = product.Width, 
                Length = product.Length
            };
        }

        public static Order ToModel(this CoolStore.Library.SqlData.EntityFramework.Models.Order order)
        {
            return new Order(order.Id, Enum.Parse<OrderStatus>(order.Status), order.CreateDate, order.UpdateDate, order.ProductId);
        }

        public static CoolStore.Library.SqlData.EntityFramework.Models.Order ToEntity(this Order order)
        {
            return new CoolStore.Library.SqlData.EntityFramework.Models.Order
            {
                Id = order.Id,
                Status = order.Status.ToString(),
                CreateDate = order.CreateDate,
                UpdateDate = order.UpdateDate,
                ProductId = order.ProductId
            };
        }
    }
}
