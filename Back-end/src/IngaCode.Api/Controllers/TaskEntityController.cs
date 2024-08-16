using IngaCode.Application.DTOs.TaskEntity;
using IngaCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskEntityController : ControllerBase
    {
        private readonly ITaskEntityService _taskEntityService;

        public TaskEntityController(ITaskEntityService taskEntityService)
        {
            _taskEntityService = taskEntityService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskEntityDto>>> GetAllTasks()
        {
            var tasks = await _taskEntityService.GetAllTasksAsync();
            return Ok(tasks);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TaskEntityDto>> GetTaskById(Guid id)
        {
            var task = await _taskEntityService.GetTaskByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<TaskEntityDto>> CreateTask([FromBody] TaskEntityCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }

            var task = await _taskEntityService.CreateTaskAsync(createDto);
            if (task != null)
            {
                return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
            }
            return BadRequest("Unable to create task.");
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTask(Guid id, [FromBody] TaskEntityUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            await _taskEntityService.UpdateTaskAsync(id, updateDto);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTask(Guid id)
        {
            await _taskEntityService.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}
