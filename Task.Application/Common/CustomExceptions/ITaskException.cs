using System.Net;

namespace Task.Application.Common.CustomExceptions;

public interface ITaskException
{
    public HttpStatusCode Code { get; }
    public string Message { get; }
}