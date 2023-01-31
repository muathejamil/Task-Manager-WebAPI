using System.Net;

namespace Task.Application.Common.CustomExceptions.User;

public class BadRequestException : Exception, ITaskException
{
    private string LocalMessage { get; }
    public BadRequestException(string message)
    {
        LocalMessage = message;
    }
    public HttpStatusCode Code => HttpStatusCode.BadRequest;
    public override string Message  => LocalMessage;
}