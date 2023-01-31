using MediatR;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Contracts.Common;

namespace Task.Application.Task.Queries.GetAll;

public record GetAllQuery(
    int PageNumber, int pageSize) : IRequest<Page<TaskDto>>;