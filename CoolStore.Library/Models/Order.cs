namespace CoolStore.Library.Models
{
    public record Order (int Id, OrderStatus Status, DateOnly CreatedDate, DateOnly UpdatedDate, int ProductId);
}
