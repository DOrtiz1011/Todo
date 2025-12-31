using Todo.APi.Models;

namespace Todo.APi.Repository
{
    /// <summary>
    /// Interface for the task repository
    /// </summary>
    public interface ITodoTaskRepository : IRepository<TodoTask>
    {
    }
}
