using Microsoft.AspNetCore.Mvc;
using Todo.Api.DTO;
using Todo.Api.Service;

namespace Todo.Api.Controllers
{
    /// <summary>
    /// Represents an API controller that manages to-do tasks, providing endpoints to create, retrieve, update, and
    /// delete tasks.
    /// </summary>
    /// <remarks>This controller exposes RESTful endpoints for interacting with to-do tasks. All actions
    /// require valid request data and return appropriate HTTP status codes based on the outcome of the operation. The
    /// controller depends on an implementation of <see cref="ITodoService"/> to perform business logic and data access.
    /// Endpoints follow standard conventions for resource management in web APIs.</remarks>
    [ApiController]
    [Route("api/[controller]")]
    public class TodoTaskController : Controller
    {
        private readonly ITodoService _todoService;

        /// <summary>
        /// Initializes a new instance of the TodoTaskController class using the specified todo service.
        /// </summary>
        /// <param name="todoService">The service used to manage and perform operations on to-do tasks. Cannot be null.</param>
        public TodoTaskController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        /// <summary>
        /// Retrieves all to-do tasks.
        /// </summary>
        /// <returns>An <see cref="ActionResult{T}">ActionResult</see> containing a collection of <see
        /// cref="TodoTaskResponseDTO"/> objects representing all to-do tasks. Returns an empty collection if no tasks
        /// are found.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTaskResponseDTO>>> GetAllTasks()
        {
            var todoTaskResponseDTOs = await _todoService.GetAllTasks();

            return Ok(todoTaskResponseDTOs);
        }
        
        /// <summary>
        /// Retrieves a to-do task by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the to-do task to retrieve.</param>
        /// <returns>An <see cref="ActionResult{TodoTaskResponseDTO}"/> containing the to-do task if found; otherwise, a 404 Not
        /// Found response.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTaskResponseDTO>> GetTaskById(int id)
        {
            var todoTaskResponseDTO = await _todoService.GetTaskById(id);

            if (todoTaskResponseDTO == null)
            {
                return NotFound(new { Message = $"Task with ID {id} not found." });
            }

            return Ok(todoTaskResponseDTO);
        }

        /// <summary>
        /// Creates a new to-do task based on the specified request data.
        /// </summary>
        /// <param name="todoTaskRequestDTO">The data for the to-do task to create. Must not be null.</param>
        /// <returns>A 201 Created response containing the details of the newly created to-do task.</returns>
        [HttpPost]
        public async Task<ActionResult<TodoTaskResponseDTO>> CreateTask([FromBody] TodoTaskRequestDTO todoTaskRequestDTO)
        {
            var todoTaskResponseDTO = await _todoService.CreateTask(todoTaskRequestDTO);

            // Returns 201 Created with the location of the new resource
            return CreatedAtAction(nameof(CreateTask), new { todoTaskResponseDTO.id }, todoTaskResponseDTO);
        }

        /// <summary>
        /// Deletes the task with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the task to delete.</param>
        /// <returns>A 204 No Content response if the task was successfully deleted; otherwise, a 404 Not Found response if no
        /// task with the specified identifier exists.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _todoService.DeleteTask(id);

            if (deleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound(new { Message = $"Task with ID {id} not found." });
            }
        }

        /// <summary>
        /// Updates an existing to-do task with the specified details.
        /// </summary>
        /// <param name="todoTaskRequestDTO">The updated details of the to-do task. Must include the task identifier and the new values to apply.</param>
        /// <returns>An HTTP 204 No Content response if the update is successful; otherwise, an HTTP 404 Not Found response if
        /// the task does not exist.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateTask([FromBody] TodoTaskRequestDTO todoTaskRequestDTO)
        {
            var updated = await _todoService.UpdateTask(todoTaskRequestDTO);

            if (updated)
            {
                return NoContent();
            }
            else
            {
                return NotFound(new { Message = $"Task with ID {todoTaskRequestDTO.id} not found." });
            }
        }
    }
}
