namespace ADO.NET.Fundamentals.Store.Library.Domain.DataTransferObjects
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