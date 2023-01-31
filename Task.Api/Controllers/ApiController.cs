using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Task.Api.Controllers;

[Authorize]
public class ApiController : ControllerBase
{
    
}