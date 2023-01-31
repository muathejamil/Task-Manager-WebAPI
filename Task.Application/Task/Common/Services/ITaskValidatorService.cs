namespace Task.Application.Task.Common.Services;

public interface ITaskValidatorService
{
    System.Threading.Tasks.Task ValidateTaskAsync(Domain.Task.Task task);
}