using System.Net;

namespace Todo.Api.Exceptions
{
    public abstract class ExceptionBase : Exception
    {
        public HttpStatusCode StatusCode { get; }

        protected ExceptionBase(string message, HttpStatusCode statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
