using System.Net;
using Task.Application.Common.CustomExceptions.Constants;

namespace Task.Application.Common.CustomExceptions.Task;

public class DuplicateTaskTitleException : Exception, ITaskException
{
    public HttpStatusCode Code => HttpStatusCode.BadRequest;
    public override string Message => ExceptionMessage.DuplicateTaskTitle;
}