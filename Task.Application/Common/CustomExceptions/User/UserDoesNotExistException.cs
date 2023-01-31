using System.Net;
using Task.Application.Common.CustomExceptions.Constants;

namespace Task.Application.Common.CustomExceptions.User;

public class UserDoesNotExistException : Exception, ITaskException
{
    public HttpStatusCode Code => HttpStatusCode.NotFound;
    public override string Message  => ExceptionMessage.UserDoesNotExist;
}