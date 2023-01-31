using System.ComponentModel.DataAnnotations;

namespace Task.Domain.Task;

public class Task
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } = DateTime.Now;
    
    public bool IsOverlapping(Task task)
    {
        return StartDate <= task.EndDate && EndDate >= task.StartDate;
    }
    
    public bool IsTitleTaken(string title)
    {
        return Title.Equals(title);
    }
}