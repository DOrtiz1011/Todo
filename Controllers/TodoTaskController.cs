using Microsoft.AspNetCore.Mvc;
using Todo.Models;
using Todo.Repository;

namespace Todo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoTaskController : Controller
    {
        private readonly ITodoTaskRepository _todoTaskRepository;

        public TodoTaskController(ITodoTaskRepository todoTaskRepository)
        {
            _todoTaskRepository = todoTaskRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tasks = await _todoTaskRepository.GetAllAsync();

            return Ok(tasks);
        }
        
        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTask>> GetById(int id)
        {
            var task = await _todoTaskRepository.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound(new { Message = $"Task with ID {id} not found." });
            }

            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TodoTask>> Create([FromBody] TodoTask task)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _todoTaskRepository.AddAsync(task);

            // Returns 201 Created with the location of the new resource
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var deleted = await _todoTaskRepository.DeleteAsync(id);

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
        public async Task<IActionResult> Update([FromBody] TodoTask todoTask)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var updated = await _todoTaskRepository.UpdateAsync(todoTask);

            if (updated)
            {
                return NoContent();
            }
            else
            {
                return NotFound(new { Message = $"Task with ID {todoTask.Id} not found." });
            }
        }
    }
}
