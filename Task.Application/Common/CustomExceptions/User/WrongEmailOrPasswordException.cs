using System.Net;
using Task.Application.Common.CustomExceptions.Constants;

namespace Task.Application.Common.CustomExceptions.User;

public class WrongEmailOrPasswordException: Exception, ITaskException
{
    public HttpStatusCode Code => HttpStatusCode.BadRequest;
    public override string Message  => ExceptionMessage.WrongEmailOrPassword;
}
