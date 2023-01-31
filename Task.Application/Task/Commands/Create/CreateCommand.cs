using MediatR;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Application.Task.Common;


namespace Task.Application.Task.Commands.Create;

public record CreateCommand(
    TaskDto Task) : IRequest<Unit>;