using System.Net;
using Task.Application.Common.CustomExceptions.Constants;

namespace Task.Application.Common.CustomExceptions.User;

public class DuplicateEmailException : Exception, ITaskException
{
    public HttpStatusCode Code => HttpStatusCode.Conflict;
    public override string Message  => ExceptionMessage.DuplicateEmail;
}
