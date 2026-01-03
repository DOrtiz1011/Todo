using Todo.Api.Models;

namespace Todo.Api.Repository
{
    /// <summary>
    /// Interface for the task repository
    /// </summary>
    public interface ITodoTaskRepository : IRepository<TodoTask>
    {
    }
}
