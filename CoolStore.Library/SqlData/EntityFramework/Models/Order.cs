namespace CoolStore.Library.SqlData.EntityFramework.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int ProductId { get; set; }
    }
}
