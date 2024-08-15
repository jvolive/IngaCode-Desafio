using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace IngaCode.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskEntityController : ControllerBase
    {
        private readonly ITaskEntityService _taskEntityService;

        public TaskEntityController(ITaskEntityService taskEntityService)
        {
            _taskEntityService = taskEntityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskEntityService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await _taskEntityService.GetTaskByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskEntity task)
        {
            var createdTask = await _taskEntityService.CreateTaskAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskEntity task)
        {
            if (id != task.Id)
                return BadRequest();

            await _taskEntityService.UpdateTaskAsync(task);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _taskEntityService.DeleteTaskAsync(id);
            return NoContent();
        }

        [HttpGet("by-project/{projectId}")]
        public async Task<IActionResult> GetByProjectId(Guid projectId)
        {
            var tasks = await _taskEntityService.GetTasksByProjectIdAsync(projectId);
            return Ok(tasks);
        }

        [HttpGet("by-date/{date}")]
        public async Task<IActionResult> GetByDate(DateTime date)
        {
            var tasks = await _taskEntityService.GetTasksByDateAsync(date);
            return Ok(tasks);
        }
    }
}
