using System.Net;

namespace Todo.Api.Exceptions
{
    public class TodoValidationException : ExceptionBase
    {
        public IDictionary<string, string[]> Errors { get; }

        public TodoValidationException(IDictionary<string, string[]> errors)
            : base("One or more validation failures have occurred.", HttpStatusCode.BadRequest)
        {
            Errors = errors;
        }
    }
}