using Microsoft.EntityFrameworkCore;
using Todo.Models;

namespace Todo.Repository
{
    public class TodoTaskRepository : ITodoTaskRepository
    {
        private readonly TodoDbContext _todoDbContext;

        public TodoTaskRepository(TodoDbContext todoDbContext)
        {
            _todoDbContext = todoDbContext;
        }

        public async Task<IEnumerable<TodoTask>> GetAllAsync()
        {
            return await _todoDbContext.TodoTasks.ToListAsync();
        }

        public async Task<TodoTask> GetByIdAsync(int id)
        {
            return await _todoDbContext.TodoTasks.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddAsync(TodoTask todoTask)
        {
            var now = DateTime.Now;

            todoTask.CreateDateTime     = now;
            todoTask.LastUpdateDateTime = now;

            await _todoDbContext.TodoTasks.AddAsync(todoTask);
            await _todoDbContext.SaveChangesAsync();
        }
    }
}
