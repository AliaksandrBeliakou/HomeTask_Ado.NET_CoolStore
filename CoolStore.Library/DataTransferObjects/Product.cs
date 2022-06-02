namespace ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects
{
    public record Product(int Id, string Name, string? Description, int Weight, int Height, int Width, int Length);
}
