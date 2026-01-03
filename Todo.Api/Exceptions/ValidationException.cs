using System.Net;

namespace Todo.Api.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public ValidationException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
