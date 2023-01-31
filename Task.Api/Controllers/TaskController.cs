using System.Net.Mime;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Application.Common.Services.CsvReader;
using Task.Application.Task.Commands.Create;
using Task.Application.Task.Commands.Delete;
using Task.Application.Task.Commands.Import;
using Task.Application.Task.Commands.Update;
using Task.Application.Task.Queries.GetAll;
using Task.Application.Task.Queries.GetOne;
using Task.Contracts.Task;

namespace Task.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TasksController : ApiController
{
    private readonly ILogger<TasksController> _logger;
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly ICsvReader _csvReader;

    public TasksController(
        ILogger<TasksController> logger, 
        ISender mediator, IMapper mapper, ICsvReader csvReader)
    {
        _logger = logger;
        _mediator = mediator;
        _mapper = mapper;
        _csvReader = csvReader;
    }
    
    
    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json)] 
    [ProducesResponseType(StatusCodes.Status201Created)] 
    public async Task<IActionResult> Create(CreateTaskRequest createTaskRequest)
    {
        _logger.LogInformation("Creating a new task");
        var taskDto = _mapper.Map<TaskDto>(createTaskRequest);
        var command = new CreateCommand(taskDto);
        await _mediator.Send(command);
        return CreatedAtAction(
            nameof(Get), new {id = command.Task.Id}, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        _logger.LogInformation("Getting all tasks");
        var query = new GetAllQuery(page, pageSize);
        var response = await _mediator.Send(query);
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    { 
        _logger.LogInformation("Getting task by id");
        var query = new GetOneQuery(id);
        return Ok(await _mediator.Send(query));
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Upsert(
        Guid id, [FromBody] UpsertTaskRequest upsertTaskRequest)
    {
        _logger.LogInformation("Updating task");
        var taskDto = _mapper.Map<TaskDto>(upsertTaskRequest);
        var command = new UpdateCommand(id, taskDto);
        await _mediator.Send(command);
        return NoContent();
    }
        
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        _logger.LogInformation("Deleting task");
        var command = new DeleteCommand(id);
        await _mediator.Send(command);
        return Ok();
    }
    
    
    [HttpPost("import")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> ImportTasks(IFormFile file)
    {
        var taskDtos = _csvReader.ReadAsync<TaskDto>(file);
        var command = new ImportCommand(taskDtos.Result);
        var taskResponse = await _mediator.Send(command);
        return CreatedAtAction(nameof(ImportTasks), taskResponse);
    }
}