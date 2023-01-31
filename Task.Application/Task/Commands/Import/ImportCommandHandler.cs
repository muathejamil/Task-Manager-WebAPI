using AutoMapper;
using MediatR;
using Task.Application.Persistence.Task;
using Task.Application.Task.Common.Services;

namespace Task.Application.Task.Commands.Import;

public class ImportCommandHandler : IRequestHandler<ImportCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskValidatorService _taskValidatorService;
    private readonly IMapper _mapper;
    
    public ImportCommandHandler(
        ITaskRepository taskRepository, IMapper mapper, ITaskValidatorService taskValidatorService)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _taskValidatorService = taskValidatorService;
    }
    
    public async Task<Unit> Handle(ImportCommand request, CancellationToken cancellationToken)
    {
        var requestTasks = request.Tasks;
        var existingTasks = _mapper.Map<IEnumerable<Domain.Task.Task>>(requestTasks);
        var tasks = existingTasks.ToList();
        foreach (var task in tasks)
        {
            await _taskValidatorService.ValidateTaskAsync(task);
        }
        await _taskRepository.CreateTasksAsync(tasks);
        return Unit.Value;
    }
}