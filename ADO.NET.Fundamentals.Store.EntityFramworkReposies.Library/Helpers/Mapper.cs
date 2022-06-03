using ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library.Entities;
using OrderDTO = ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects.Order;
using OrderStatusDTO = ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects.OrderStatus;
using ProductDTO = ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects.Product;

namespace ADO.NET.Fundamentals.Store.EntityFramworkReposies.Library
{
    public static class Mapper
    {
        public static ProductDTO ToModel(this Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new ProductDTO(product.Id, product.Name, product.Description, product.Weight, product.Height, product.Width, product.Length);
        }

        public static Product ToEntity(this ProductDTO product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new Product
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

        public static OrderDTO ToModel(this Order order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return new OrderDTO(order.Id, Enum.Parse<OrderStatusDTO>(order.Status), order.CreateDate, order.UpdateDate, order.ProductId);
        }

        public static Order ToEntity(this OrderDTO order)
        {
            if (order is null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            return new Order
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
