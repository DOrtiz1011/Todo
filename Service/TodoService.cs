using AutoMapper;
using Todo.APi.DTO;
using Todo.APi.Exceptions;
using Todo.APi.Models;
using Todo.APi.Repository;

namespace Todo.APi.Service
{
    public class TodoService : ITodoService
    {
        private readonly ITodoTaskRepository _todoTaskRepository;
        private readonly IMapper             _mapper;

        public TodoService(ITodoTaskRepository todoTaskRepository, IMapper mapper)
        {
            _todoTaskRepository = todoTaskRepository;
            _mapper             = mapper;
        }

        public async Task<IEnumerable<TodoTaskResponseDTO>> GetAllTasks()
        {
            var todoTasks           = await _todoTaskRepository.GetAllAsync();
            var todoTaskResponseDTO = _mapper.Map<IEnumerable<TodoTaskResponseDTO>>(todoTasks);

            return todoTaskResponseDTO;
        }

        public async Task<TodoTaskResponseDTO> GetTaskById(int id)
        {
            var todoTask = await _todoTaskRepository.GetByIdAsync(id);

            if (todoTask == null)
            {
                throw new NotFoundException("TodoTask", id);
            }

            var todoTaskResponseDTO = _mapper.Map<TodoTaskResponseDTO>(todoTask);

            return todoTaskResponseDTO;
        }

        public async Task<TodoTaskResponseDTO> CreateTask(TodoTaskRequestDTO todoTaskRequestDTO)
        {
            if (string.IsNullOrWhiteSpace(todoTaskRequestDTO.title))
            {
                throw new ValidationException("Task title cannot be empty.");
            }

            var todoTask = _mapper.Map<TodoTask>(todoTaskRequestDTO);

            await _todoTaskRepository.AddAsync(todoTask);

            var todoTaskResponseDTO = _mapper.Map<TodoTaskResponseDTO>(todoTask);

            return todoTaskResponseDTO;
        }

        public async Task<bool> DeleteTask(int id)
        {
            return await _todoTaskRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateTask(TodoTaskRequestDTO todoTaskRequestDTO)
        {
            var todoTask = _mapper.Map<TodoTask>(todoTaskRequestDTO);

            return await _todoTaskRepository.UpdateAsync(todoTask);
        }
    }
}
