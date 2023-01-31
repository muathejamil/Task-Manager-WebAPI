namespace Task.Contracts.Task;

public record CreateTaskRequest(
    string Title, 
    DateTime StartDate, 
    DateTime EndDate);