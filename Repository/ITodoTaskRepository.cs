using Todo.Models;

namespace Todo.Repository
{
    /// <summary>
    /// Interface for the task repository
    /// </summary>
    public interface ITodoTaskRepository : IRepository<TodoTask>
    {
    }
}
