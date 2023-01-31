using MediatR;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Application.Task.Common;

namespace Task.Application.Task.Commands.Update;

public record UpdateCommand(
    Guid? Id, TaskDto Task) : IRequest<Unit>;