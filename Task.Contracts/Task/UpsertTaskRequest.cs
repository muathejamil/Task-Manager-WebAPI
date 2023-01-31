namespace Task.Contracts.Task;

public record UpsertTaskRequest(
        Guid? Id,
        string Title,
        DateTime StartDate,
        DateTime EndDate
    );