namespace CoolStore.Library.Models
{
    public record Product(int Id, string Name, string? Description, int Weight, int Height, int Width, int Length);
}
