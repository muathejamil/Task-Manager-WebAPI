

using Microsoft.EntityFrameworkCore;
using Task.Application.Persistence.Task;
using Task.Contracts.Common;

namespace Task.Infrastructure.Persistence.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly TaskDbContext _dbContext;
    
    public TaskRepository(TaskDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async System.Threading.Tasks.Task CreateTaskAsync(
        Domain.Task.Task task)
    {
        _dbContext.Tasks.Add(task);
        await _dbContext.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task CreateTasksAsync(
        IEnumerable<Domain.Task.Task> tasks)
    {
        _dbContext.Tasks.AddRange(tasks);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Page<Domain.Task.Task>> GetTasksAsync(int pageNumber, int pageSize)
    {
        var totalCount = await _dbContext.Tasks.CountAsync();
        var tasks = await _dbContext.Tasks
            .OrderBy(t => t.EndDate)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new Page<Domain.Task.Task>
        {
            Contents = tasks,
            TotalCount = totalCount,
            PageNumber = pageNumber
        };
    }

    public async Task<Domain.Task.Task?> GetTaskByIdAsync(Guid? id)
    {
        return await _dbContext.Tasks.SingleOrDefaultAsync(t => t.Id == id);
    }
    
    public async System.Threading.Tasks.Task UpsertTaskAsync(Domain.Task.Task task)
    {
        var existingTask = await _dbContext.Tasks.FindAsync(task.Id);
        if (existingTask == null)
        {
            _dbContext.Tasks.Add(task);
        }
        else
        {
            existingTask.Title = task.Title;
            existingTask.StartDate = task.StartDate;
            existingTask.EndDate = task.EndDate;
            _dbContext.Tasks.Update(existingTask);
        }
        await _dbContext.SaveChangesAsync();
    }

    public async System.Threading.Tasks.Task DeleteTaskAsync(Domain.Task.Task task)
    {
        _dbContext.Tasks.Remove(task);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> IsOverlappingAsync(Domain.Task.Task task)
    {
        //TODO: This is not the best way to do this. when we have a lot of tasks, this will be very slow.
        var tasks = await _dbContext.Tasks
            .AsNoTracking()
            .ToListAsync();
        return tasks
            .Any(t => t.Id != task.Id && t.IsOverlapping(task));
    }
    
    
    public async Task<bool> IsTitleTakenAsync(string title)
    {
        var tasks = await _dbContext.Tasks
            .AsNoTracking()
            .ToListAsync();
        return tasks
            .Any(t => t.IsTitleTaken(title));
    }
}