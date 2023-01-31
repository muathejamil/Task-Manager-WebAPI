using Task.Application.Common.CustomExceptions.Task;
using Task.Application.Persistence.Task;

namespace Task.Application.Task.Common.Services;

public class TaskValidatorService : ITaskValidatorService
{
    private readonly ITaskRepository _taskRepository;
    
    public TaskValidatorService(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }
    
    
    public async System.Threading.Tasks.Task ValidateTaskAsync(Domain.Task.Task task)
    {
        await ValidateOverlapping(task);
        await ValidateTakenTitle(task);
    }
    
    private async System.Threading.Tasks.Task ValidateTakenTitle(Domain.Task.Task existingTask)
    {
        if (await _taskRepository.IsTitleTakenAsync(existingTask.Title))
        {
            throw new DuplicateTaskTitleException();
        }
    }

    private async System.Threading.Tasks.Task ValidateOverlapping(Domain.Task.Task existingTask)
    {
        if (await _taskRepository.IsOverlappingAsync(existingTask))
        {
            throw new TaskOverlappingException();
        }
    }
}