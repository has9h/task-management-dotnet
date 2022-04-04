using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagement.Model;
using TaskManagement.Repository;

namespace TaskManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        // Get data from repository
        // Inject TaskRepository in constructor
        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _taskRepository.GetAllTasksAsync();

            return Ok(tasks);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTaskById([FromRoute] int id)
        {
            var task = await _taskRepository.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpGet("{title}")]
        public async Task<IActionResult> GetTaskByTitle([FromRoute] string title)
        {
            var task = await _taskRepository.GetTaskByTitleAsync(title);
            if (task == null)
            {
                return NotFound();
            }

            return Ok(task);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewTask([FromBody] TaskModel taskModel)
        {
            var id = await _taskRepository.AddTaskAsync(taskModel);
            return CreatedAtAction(nameof(GetTaskById), new { id=id, controller="task" }, id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask([FromRoute] int id, [FromBody] TaskModel taskModel)
        {
            await _taskRepository.UpdateTaskAsync(id, taskModel);
            return Ok();
        }
    }
}
