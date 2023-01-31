using AutoMapper;
using MediatR;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Application.Persistence.Task;
using Task.Application.Task.Common;

namespace Task.Application.Task.Queries.GetOne;

public class GetOneQueryHandler : IRequestHandler<GetOneQuery, TaskResponse>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    public GetOneQueryHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    
    public async Task<TaskResponse> Handle(GetOneQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskByIdAsync(request.Id);
        var taskDto = _mapper.Map<TaskDto>(task);
        return new TaskResponse(taskDto);
    }
}