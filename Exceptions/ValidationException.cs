using System.Net;

namespace Todo.APi.Exceptions
{
    public class ValidationException : ExceptionBase
    {
        public ValidationException(string message)
            : base(message, HttpStatusCode.BadRequest)
        {
        }
    }
}
