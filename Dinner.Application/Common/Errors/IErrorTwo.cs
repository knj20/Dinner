using System.Net;

namespace Dinner.Application.Common.Errors
{
    public interface IErrorTwo
    {
        public HttpStatusCode StatusCode { get; }
        public string ErrorMessage { get; }
    }
}
