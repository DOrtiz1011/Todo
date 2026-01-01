using AutoMapper;
using FluentValidation;
using Todo.APi.DTO;
using Todo.APi.Exceptions;
using Todo.APi.Models;
using Todo.APi.Repository;

namespace Todo.APi.Service
{
    /// <summary>
    /// Provides operations for managing to-do tasks, including creating, retrieving, updating, and deleting tasks.
    /// </summary>
    /// <remarks>The TodoService implements the ITodoService interface and acts as the main entry point for
    /// business logic related to to-do tasks. It coordinates data access and mapping between domain entities and data
    /// transfer objects (DTOs). All methods are asynchronous and intended for use in applications that require task
    /// management functionality.</remarks>
    public class TodoService : ITodoService
    {
        private readonly ITodoTaskRepository            _todoTaskRepository;
        private readonly IMapper                        _mapper;
        private readonly IValidator<TodoTaskRequestDTO> _validator;

        public TodoService(ITodoTaskRepository todoTaskRepository, IMapper mapper, IValidator<TodoTaskRequestDTO> validator)
        {
            _todoTaskRepository = todoTaskRepository;
            _mapper             = mapper;
            _validator          = validator;
        }

        /// <summary>
        /// Asynchronously retrieves all to-do tasks.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of <see
        /// cref="TodoTaskResponseDTO"/> objects representing all to-do tasks. The collection is empty if no tasks are
        /// found.</returns>
        public async Task<IEnumerable<TodoTaskResponseDTO>> GetAllTasks()
        {
            var todoTasks           = await _todoTaskRepository.GetAllAsync();
            var todoTaskResponseDTO = _mapper.Map<IEnumerable<TodoTaskResponseDTO>>(todoTasks);

            return todoTaskResponseDTO;
        }

        /// <summary>
        /// Retrieves a to-do task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the to-do task to retrieve. Must be a valid, existing task ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a <see
        /// cref="TodoTaskResponseDTO"/> representing the requested to-do task.</returns>
        /// <exception cref="NotFoundException">Thrown if a to-do task with the specified <paramref name="id"/> does not exist.</exception>
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

        /// <summary>
        /// Creates a new to-do task based on the specified request data.
        /// </summary>
        /// <param name="todoTaskRequestDTO">The data used to create the new to-do task. Must contain a non-empty title.</param>
        /// <returns>A <see cref="TodoTaskResponseDTO"/> representing the newly created to-do task.</returns>
        /// <exception cref="ValidationException">Thrown if the <paramref name="todoTaskRequestDTO"/> does not contain a valid, non-empty title.</exception>
        public async Task<TodoTaskResponseDTO> CreateTask(TodoTaskRequestDTO todoTaskRequestDTO)
        {
            var validationResult = await _validator.ValidateAsync(todoTaskRequestDTO);
            var todoTask         = _mapper.Map<TodoTask>(todoTaskRequestDTO);

            await _todoTaskRepository.CreateAsync(todoTask);

            var todoTaskResponseDTO = _mapper.Map<TodoTaskResponseDTO>(todoTask);

            return todoTaskResponseDTO;
        }

        /// <summary>
        /// Deletes the task with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the task was
        /// successfully deleted; otherwise, <see langword="false"/>.</returns>
        public async Task<bool> DeleteTask(int id)
        {
            return await _todoTaskRepository.DeleteAsync(id);
        }

        /// <summary>
        /// Updates an existing to-do task with the values provided in the specified request data transfer object.
        /// </summary>
        /// <param name="todoTaskRequestDTO">An object containing the updated values for the to-do task. All required fields must be populated as
        /// expected by the update operation.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the update
        /// was successful; otherwise, <see langword="false"/>.</returns>
        public async Task<bool> UpdateTask(TodoTaskRequestDTO todoTaskRequestDTO)
        {
            var validationResult = await _validator.ValidateAsync(todoTaskRequestDTO);
            var todoTask         = _mapper.Map<TodoTask>(todoTaskRequestDTO);

            return await _todoTaskRepository.UpdateAsync(todoTask);
        }
    }
}
