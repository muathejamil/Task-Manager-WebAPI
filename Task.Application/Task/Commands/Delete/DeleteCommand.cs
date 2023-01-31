using MediatR;
using Task.Application.Task.Common;

namespace Task.Application.Task.Commands.Delete;

public record DeleteCommand(
    Guid Id) : IRequest<Unit>;