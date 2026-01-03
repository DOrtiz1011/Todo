using Todo.Api.DTO;

namespace Todo.Api.Service
{
    public interface ITodoService
    {
        Task<IEnumerable<TodoTaskResponseDTO>> GetAllTasks();
        
        Task<TodoTaskResponseDTO> GetTaskById(int id);
            
        Task<TodoTaskResponseDTO> CreateTask(TodoTaskRequestDTO dto);
            
        Task<bool> DeleteTask(int id);
            
        Task<bool> UpdateTask(TodoTaskRequestDTO todoTaskRequestDTO);
    }
}
