using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Task.Api.Controllers;
using Task.Application.Authentication.Commands.Register;
using Task.Application.Authentication.Queries;
using Task.Application.Common.Mappers.Dtos.User;
using Task.Contracts.Authentication;
using Xunit;
using AuthenticationResponse = Task.Application.Authentication.Common.AuthenticationResponse;

namespace Task.UnitTests
{
    public class AuthenticationsControllerTests
    {
        private readonly Mock<ISender> _mediator;
        private readonly AuthenticationsController _controller;
        private readonly Mock<ILogger<TasksController>> _logger;
        private readonly Mock<IMapper> _mapper;
        
        public AuthenticationsControllerTests()
        {
            _mediator = new Mock<ISender>();
            _logger = new Mock<ILogger<TasksController>>();
            _mapper = new Mock<IMapper>();
            _controller = new AuthenticationsController(_mediator.Object, _mapper.Object);
        }
        
        [Fact]
        public async System.Threading.Tasks.Task Register_ReturnsOkResult()
        {
            // Arrange
            var request = new RegisterRequest(
                    "Muathe", 
                    "Jamil",
                    "muathejamil@gmail.com", 
                    "1234123");
                
            var command = new RegisterCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password
                );
            var user = new UserDto(Guid.NewGuid(), request.FirstName, request.LastName, request.Email);
            var response = new AuthenticationResponse(
                user, "token");

            _mapper.Setup(x => x.Map<RegisterCommand>(request)).Returns(command);
            _mediator.Setup(x => x.Send(command, CancellationToken.None))
                .ReturnsAsync(response);
            // Act
            var result = await _controller.Register(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(response, okResult.Value);
        }

        [Fact]
        public async System.Threading.Tasks.Task Login_ReturnsOkResult()
        {
            // Arrange
            var request = new LoginRequest(
                "muathejamil@gmail.com",
                "1234123");
            var query = new LoginQuery(
                request.Email, 
                request.Password);
            var user = new UserDto(Guid.NewGuid(), 
                "Muathe", 
                "Jamil",
                "muathejamil@gmail.com");
            var response = new AuthenticationResponse(user, "token");

            _mapper.Setup(x => x.Map<LoginQuery>(request)).Returns(query);
            _mediator.Setup(x => x.Send(query, CancellationToken.None))
                .Returns(System.Threading.Tasks.Task.FromResult(response));
            
            // Act
            var result = await _controller.Login(request);

            // Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.Equal(response, okResult.Value);
        }
    }
}