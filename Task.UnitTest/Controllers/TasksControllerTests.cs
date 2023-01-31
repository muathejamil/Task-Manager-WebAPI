using Task.Api.Controllers;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Task.Application.Common.Mappers.Dtos.Task;
using Task.Application.Common.Services.CsvReader;
using Task.Application.Task.Commands.Create;
using Task.Application.Task.Commands.Delete;
using Task.Application.Task.Commands.Import;
using Task.Application.Task.Common;
using Task.Application.Task.Queries.GetAll;
using Task.Application.Task.Queries.GetOne;
using Task.Contracts.Common;
using Task.Contracts.Task;
using Xunit;
using Assert = Xunit.Assert;

namespace Task.Api.UnitTest.Controllers;

public class TasksControllerTests
{
    private readonly Mock<ISender> _mediator;
    private readonly Mock<ICsvReader> _csvReader;
    private readonly TasksController _controller;
    private readonly Mock<ILogger<TasksController>> _logger;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IFormFile> _formFile;

    public TasksControllerTests()
    {
        _mediator = new Mock<ISender>();
        _csvReader = new Mock<ICsvReader>();
        _logger = new Mock<ILogger<TasksController>>();
        _mapper = new Mock<IMapper>();
        _controller = new TasksController(_logger.Object, _mediator.Object, _mapper.Object, _csvReader.Object);
        _formFile = new Mock<IFormFile>();
    }

    [Fact]
    public async System.Threading.Tasks.Task Create_ReturnsCreatedResult()
    {
        // Arrange
        var createTaskRequest = new CreateTaskRequest(
            "Test Task", DateTime.Now, DateTime.Now.AddHours(1));

        var taskDto = new TaskDto(Guid.NewGuid(),
            createTaskRequest.Title, createTaskRequest.StartDate,
            createTaskRequest.EndDate
        );
        var id = taskDto.Id;
        var command = new CreateCommand(taskDto);
        _mediator.Setup(x => x.Send(command, CancellationToken.None))
            .ReturnsAsync(Unit.Value);

        _mapper.Setup(x => x.Map<TaskDto>(createTaskRequest))
            .Returns(taskDto);

        // Act
        var result = await _controller.Create(createTaskRequest);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
        Assert.Equal(id, createdAtActionResult.RouteValues?["id"]);
    }

    [Fact]
    public async System.Threading.Tasks.Task GetAll_ReturnsOkResult()
    {
        // Arrange
        var tasks = new List<TaskDto>
        {
            new TaskDto(Guid.NewGuid(), "Task 1", DateTime.Now, DateTime.Now.AddHours(1)),
            new TaskDto(Guid.NewGuid(), "Task 2", DateTime.Now, DateTime.Now.AddHours(2)),
            new TaskDto(Guid.NewGuid(), "Task 3", DateTime.Now, DateTime.Now.AddHours(3)),
        };
        var query = new GetAllQuery(1, 10);
        Page<TaskDto> page = new Page<TaskDto>();
        page.PageNumber = 1;
        page.TotalCount = tasks.Count;
        page.Contents = tasks;
        _mediator.Setup(x => x.Send(query, CancellationToken.None))
            .ReturnsAsync(page);

        // Act
        var result = await _controller.GetAll(1, 10);

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result);
        var response = Assert.IsType<Page<TaskDto>>(okObjectResult.Value);
        Assert.Equal(tasks.Count, response.TotalCount);
    }

    [Fact]
    public async System.Threading.Tasks.Task Get_ReturnsOkResult_WithTaskResponse()
    {
        var taskDto = new TaskDto(Guid.NewGuid(), "Task 1", DateTime.Now, DateTime.Now.AddHours(1));
        var taskResponse1 = new TaskResponse(taskDto);

        // Arrange
        var expectedTaskResponse = new Func<TaskResponse>(() => taskResponse1);
        var id = taskDto.Id;
        _mediator.Setup(x => x.Send(It.Is<GetOneQuery>(y => y.Id == id), CancellationToken.None))
            .ReturnsAsync(taskResponse1);



        // Act
        var result = await _controller.Get(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var taskResponse = Assert.IsAssignableFrom<TaskResponse>(okResult.Value);
        Assert.Equal(taskResponse1, taskResponse);
        _mediator.Verify(x => x.Send(It.Is<GetOneQuery>(y => y.Id == id), CancellationToken.None), Times.Once());
    }

    [Fact]
    public async System.Threading.Tasks.Task Delete_ReturnsOkResult()
    {
        // Arrange
        var id = Guid.NewGuid();
        var command = new DeleteCommand(id);

        // Act
        var result = await _controller.Delete(id);

        // Assert
        Assert.IsType<OkResult>(result);
        _mediator.Verify(x => x.Send(command, CancellationToken.None), Times.Once());
    }

    [Fact]
    public async System.Threading.Tasks.Task ImportTasks_ReturnsCreatedResult_WithTaskResponse()
    {
        // Arrange
        var taskDtos = new List<TaskDto>
        {
            new TaskDto(Guid.NewGuid(), "Task 1", DateTime.Now, DateTime.Now.AddHours(1)),
            new TaskDto(Guid.NewGuid(), "Task 2", DateTime.Now, DateTime.Now.AddHours(2))
        };
        var command = new ImportCommand(taskDtos);
        _csvReader.Setup(x => x.ReadAsync<TaskDto>(_formFile.Object))
            .ReturnsAsync(taskDtos);
        _mediator.Setup(x => x.Send(command, CancellationToken.None))
            .ReturnsAsync(Unit.Value);

        // Act
        var result = await _controller.ImportTasks(_formFile.Object);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result);
        var resultTaskResponse = Assert.IsAssignableFrom<Unit>(createdResult.Value);
        Assert.Equal(Unit.Value, resultTaskResponse);
        _csvReader.Verify(x => x.ReadAsync<TaskDto>(_formFile.Object), Times.Once());
        _mediator.Verify(x => x.Send(command, CancellationToken.None), Times.Once());
    }

}


