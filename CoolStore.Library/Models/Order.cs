namespace CoolStore.Library.Models
{
    public record Order(int Id, OrderStatus Status, DateTime CreatedDate, DateTime UpdatedDate, int ProductId);
}
