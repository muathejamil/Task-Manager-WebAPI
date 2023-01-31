namespace Task.Application.Common.Mappers.Dtos.Task;

public record TaskDto(
    Guid Id, string Title, DateTime StartDate, DateTime EndDate);
    