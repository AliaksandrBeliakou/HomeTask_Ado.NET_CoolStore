namespace ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects
{
    public record Order(int Id, OrderStatus Status, DateTime CreateDate, DateTime UpdateDate, int ProductId);
}
