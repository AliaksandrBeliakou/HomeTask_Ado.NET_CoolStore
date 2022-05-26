namespace CoolStore.Library.Models
{
    public record Order(int Id, OrderStatus Status, DateTime CreateDate, DateTime UpdateDate, int ProductId);
}
