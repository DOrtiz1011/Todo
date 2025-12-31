using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Todo.DTO;
using Todo.Models;
using Todo.Repository;

namespace Todo.Controllers
{
    /// <summary>
    /// Controller for Task API. Directs request to repository methods
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TodoTaskController : Controller
    {
        private readonly ITodoTaskRepository _todoTaskRepository;
        private readonly IMapper             _mapper;

        public TodoTaskController(ITodoTaskRepository todoTaskRepository, IMapper mapper)
        {
            _todoTaskRepository = todoTaskRepository;
            _mapper             = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoTaskResponseDTO>>> GetAllAsync()
        {
            var todoTasks           = await _todoTaskRepository.GetAllAsync();
            var todoTaskResponseDTO = _mapper.Map<IEnumerable<TodoTaskResponseDTO>>(todoTasks);

            return Ok(todoTaskResponseDTO);
        }
        
        // GET: api/tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoTaskResponseDTO>> GetById(int id)
        {
            var todoTask = await _todoTaskRepository.GetByIdAsync(id);

            if (todoTask == null)
            {
                return NotFound(new { Message = $"Task with ID {id} not found." });
            }

            var todoTaskResponseDTO = _mapper.Map<TodoTaskResponseDTO>(todoTask);

            return Ok(todoTaskResponseDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TodoTaskResponseDTO>> Create([FromBody] TodoTaskRequestDTO todoTaskRequestDTO)
        {
            var todoTask = _mapper.Map<TodoTask>(todoTaskRequestDTO);

            await _todoTaskRepository.AddAsync(todoTask);

            var todoTaskResponseDTO = _mapper.Map<TodoTaskResponseDTO>(todoTask);

            // Returns 201 Created with the location of the new resource
            return CreatedAtAction(nameof(GetById), new { todoTaskResponseDTO.id }, todoTaskResponseDTO);
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
        public async Task<IActionResult> Update([FromBody] TodoTaskRequestDTO todoTaskRequestDTO)
        {
            var todoTask = _mapper.Map<TodoTask>(todoTaskRequestDTO);

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
