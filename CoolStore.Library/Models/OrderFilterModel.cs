namespace CoolStore.Library.Models
{
    public record OrderFilterModel
    (
        OrderStatus? Status,
        int? Year,
        int? Month,
        int? ProductId
    );
}
