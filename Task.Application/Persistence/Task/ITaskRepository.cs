using Task.Contracts.Common;

namespace Task.Application.Persistence.Task;


public interface ITaskRepository
{
    System.Threading.Tasks.Task CreateTaskAsync(Domain.Task.Task task);
    
    System.Threading.Tasks.Task CreateTasksAsync(IEnumerable<Domain.Task.Task> tasks);
    Task<Page<Domain.Task.Task>> GetTasksAsync(int pageNumber, int pageSize);
    Task<Domain.Task.Task?> GetTaskByIdAsync(Guid? id);
    System.Threading.Tasks.Task UpsertTaskAsync(Domain.Task.Task task);
    System.Threading.Tasks.Task DeleteTaskAsync(Domain.Task.Task task);
    
    Task<bool> IsOverlappingAsync(Domain.Task.Task task);
    Task<bool> IsTitleTakenAsync(string title);

}