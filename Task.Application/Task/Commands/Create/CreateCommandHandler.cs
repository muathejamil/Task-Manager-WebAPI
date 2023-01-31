using AutoMapper;
using MediatR;
using Task.Application.Common.CustomExceptions.Task;
using Task.Application.Persistence.Task;
using Task.Application.Task.Common;

namespace Task.Application.Task.Commands.Create;

public class CreateCommandHandler : IRequestHandler<CreateCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;
    
    public CreateCommandHandler(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(
        CreateCommand request, 
        CancellationToken cancellationToken)
    {
        
        var task = _mapper.Map<Domain.Task.Task>(request.Task);
        var isOverlappingAsync = await _taskRepository.IsOverlappingAsync(task);
        if (isOverlappingAsync)
        {
            throw new TaskOverlappingException();
        }

        var isTitleTakenAsync = await _taskRepository.IsTitleTakenAsync(task.Title);
        if (isTitleTakenAsync)
        {
            throw new DuplicateTaskTitleException();
        }
        
        await _taskRepository.CreateTaskAsync(task);
        return Unit.Value;
    }
}