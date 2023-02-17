using System.Net;

namespace Dinner.Application.Common.Errors
{
    public class DuplicateEmailException : Exception, IServiceException
    {
        HttpStatusCode IServiceException.StatusCode => HttpStatusCode.Conflict;

        string IServiceException.Message => "Email already exists";
    }
}
