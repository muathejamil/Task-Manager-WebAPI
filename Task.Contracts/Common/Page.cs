namespace Task.Contracts.Common;

public class Page<T>
{
    public List<T> Contents { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageNumber { get; set; }
}