using MediatR;
using Task.Application.Common.Mappers.Dtos.Task;

namespace Task.Application.Task.Commands.Import;

public record ImportCommand(
    IEnumerable<TaskDto> Tasks) : IRequest<Unit>;