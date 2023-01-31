using MediatR;
using Task.Application.Task.Common;

namespace Task.Application.Task.Queries.GetOne;

public record GetOneQuery(
    Guid Id) : IRequest<TaskResponse>;