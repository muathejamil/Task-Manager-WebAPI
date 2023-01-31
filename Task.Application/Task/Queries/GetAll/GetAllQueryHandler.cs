using AutoMapper;
using MediatR;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Application.Persistence.Task;
using Task.Contracts.Common;

namespace Task.Application.Task.Queries.GetAll;

public class GetAllQueryHandler : IRequestHandler<GetAllQuery, Page<TaskDto>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    public GetAllQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<Page<TaskDto>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        var tasksAsync = await _taskRepository.GetTasksAsync(request.PageNumber, request.pageSize);
        return _mapper.Map<Page<TaskDto>>(tasksAsync);;
    }
}