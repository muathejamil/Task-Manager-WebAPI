using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Authentication.Commands.Register;
using Task.Application.Authentication.Queries;
using Task.Contracts.Authentication;

namespace Task.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("auth/v1/[controller]")]
public class AuthenticationsController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    
    public AuthenticationsController(
        ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }
    

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var response = await _mediator.Send(command);
        return Ok(response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);
        var response = await _mediator.Send(query);
        return Ok(response);
    }
}