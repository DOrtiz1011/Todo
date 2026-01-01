using Todo.APi.Models;

namespace Todo.APi.Repository
{
    /// <summary>
    /// Defines the basic CRUD operations for all repos. This uses a generic to enforce that all models inherit from TableBase.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : TableBase
    {
        Task<T> GetByIdAsync(int id);

        Task<IEnumerable<T>> GetAllAsync();
        
        Task AddAsync(T t);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(T t);
    }
}
