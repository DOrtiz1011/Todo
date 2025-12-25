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
    }
}
