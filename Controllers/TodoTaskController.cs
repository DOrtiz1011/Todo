using Microsoft.AspNetCore.Mvc;
using Todo.APi.DTO;
using Todo.APi.Service;

namespace Todo.APi.Controllers
{
    /// <summary>
    /// Controller for Task API. Directs request to repository methods
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodoTaskController : Controller
    {
        private readonly ITodoService _todoService;

        public TodoTaskController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTaskResponseDTO>>> GetAllTasks()
        {
            var todoTaskResponseDTOs = await _todoService.GetAllTasks();

            return Ok(todoTaskResponseDTOs);
        }
        
        // GET: api/tasks/5
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

        [HttpPost]
        public async Task<ActionResult<TodoTaskResponseDTO>> CreateTask([FromBody] TodoTaskRequestDTO todoTaskRequestDTO)
        {
            var todoTaskResponseDTO = await _todoService.CreateTask(todoTaskRequestDTO);

            // Returns 201 Created with the location of the new resource
            return CreatedAtAction(nameof(CreateTask), new { todoTaskResponseDTO.id }, todoTaskResponseDTO);
        }

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
