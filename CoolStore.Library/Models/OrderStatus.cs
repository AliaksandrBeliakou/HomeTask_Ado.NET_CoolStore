namespace CoolStore.Library.Models
{
    public enum OrderStatus
    {
        NotStarted = 0,
        Loading,
        InProgress,
        Arrived,
        Unloading,
        Cancelled,
        Done
    }
}