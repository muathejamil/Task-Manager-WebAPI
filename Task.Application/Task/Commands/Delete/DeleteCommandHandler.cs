using MediatR;
using Task.Application.Common.CustomExceptions.Common;
using Task.Application.Persistence.Task;
using Task.Application.Task.Common;

namespace Task.Application.Task.Commands.Delete;

public class DeleteCommandHandler : IRequestHandler<DeleteCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;
    
    public DeleteCommandHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetTaskByIdAsync(request.Id);
        if (task == null)
        {
            throw new ResourceDosNotExist();
        }
        
        await _taskRepository.DeleteTaskAsync(task);
        
        return Unit.Value;
    }
    
}