using System.Net;
using Task.Application.Common.CustomExceptions.Constants;

namespace Task.Application.Common.CustomExceptions.Common;

public class ResourceDosNotExist : Exception, ITaskException
{
    public HttpStatusCode Code => HttpStatusCode.NotFound;
    
    public override string Message => ExceptionMessage.ResourceDoesNotExist;
}