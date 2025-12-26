using Todo.Models;

namespace Todo.Repository
{
    public interface IRepository<T> where T : TableBase
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
        
        Task AddAsync(T t);

        Task<bool> DeleteAsync(int id);
    }
}
