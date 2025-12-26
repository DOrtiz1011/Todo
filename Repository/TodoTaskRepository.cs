using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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

        public async Task<bool> DeleteAsync(int id)
        {
            var todoTask = await GetByIdAsync(id);

            if (todoTask != null)
            {
                _todoDbContext.TodoTasks.Remove(todoTask);
                await _todoDbContext.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> UpdateAsync(TodoTask todoTask)
        {
            var exists = await _todoDbContext.TodoTasks.AsNoTracking().AnyAsync(t => t.Id == todoTask.Id);

            if (!exists)
            {
                return false;
            }

            todoTask.LastUpdateDateTime = DateTime.Now;

            _todoDbContext.TodoTasks.Update(todoTask);
            await _todoDbContext.SaveChangesAsync();
            
            return true;
        }
    }
}
