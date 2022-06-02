namespace ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects
{
    public record OrderFilterModel
    (
        OrderStatus? Status,
        int? Year,
        int? Month,
        int? ProductId
    );
}
