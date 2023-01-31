using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task.Application.Common.CustomExceptions;
using Task.Application.Common.CustomExceptions.Constants;


namespace Task.Api.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        var (statusCode, title) = exception switch
        {
            ITaskException taskException => ((int)taskException.Code, taskException.Message),
            _ => (StatusCodes.Status500InternalServerError, ExceptionMessage.InternalServerError)
        };
        return Problem(statusCode: statusCode, title: title);
    }
}