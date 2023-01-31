using AutoMapper;
using MediatR;
using Task.Application.Common.CustomExceptions.Task;
using Task.Application.Persistence.Task;
using Task.Application.Task.Common.Services;

namespace Task.Application.Task.Commands.Update;

public class UpdateCommandHandler : IRequestHandler<UpdateCommand, Unit>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ITaskValidatorService _taskValidatorService;
    private readonly IMapper _mapper;

    public UpdateCommandHandler(
        ITaskRepository taskRepository, IMapper mapper, ITaskValidatorService taskValidatorService)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
        _taskValidatorService = taskValidatorService;
    }

    public async Task<Unit> Handle(UpdateCommand request, CancellationToken cancellationToken)
    {
        var existingTask = _mapper.Map<Domain.Task.Task>(request.Task);
        await _taskValidatorService.ValidateTaskAsync(existingTask);
        await _taskRepository.UpsertTaskAsync(existingTask);
        return Unit.Value;

    }
}