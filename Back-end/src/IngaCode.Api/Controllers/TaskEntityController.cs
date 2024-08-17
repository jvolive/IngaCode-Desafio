using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs.TaskEntity;
using Microsoft.AspNetCore.Authorization;

namespace IngaCode.WebApi.Controllers;

[Authorize]
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
    public async Task<ActionResult<IEnumerable<TaskEntityDto>>> GetAll()
    {
        var tasks = await _taskEntityService.GetAllTaskEntityAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskEntityDto>> GetById(Guid id)
    {
        var task = await _taskEntityService.GetTaskEntityByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskEntityDto>> Create([FromBody] TaskEntityEditDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Task data is null");
        }

        var task = await _taskEntityService.CreateTaskEntityAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TaskEntityEditDto dto)
    {
        if (dto == null || id != id)
        {
            return BadRequest("Task data is invalid");
        }

        var result = await _taskEntityService.UpdateTaskEntityAsync(id, dto);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _taskEntityService.DeleteTaskEntityAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}