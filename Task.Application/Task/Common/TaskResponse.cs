using Task.Application.Common.Mappers.Dtos.Task;

namespace Task.Application.Task.Common;

public record TaskResponse(
    TaskDto Task);