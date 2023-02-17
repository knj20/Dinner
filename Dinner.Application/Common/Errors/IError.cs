
using System.Net;

namespace Dinner.Application.Common.Errors
{
    public interface IError
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
