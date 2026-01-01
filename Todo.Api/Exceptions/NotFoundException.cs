using System.Net;

namespace Todo.APi.Exceptions
{
    public class NotFoundException : ExceptionBase
    {
        public NotFoundException(string entityName, object key)
            : base($"{entityName} with id ({key}) was not found.", HttpStatusCode.NotFound)
        {
        }
    }
}
