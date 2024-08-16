using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs.TaskEntity;

namespace IngaCode.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskEntityController : ControllerBase
{
    private readonly ITaskEntityService _taskEntityService;

    public TaskEntityController(ITaskEntityService taskEntityService)
    {
        _taskEntityService = taskEntityService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskEntityDto>> GetById(Guid id)
    {
        var task = await _taskEntityService.GetByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }
        return Ok(task);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskEntityDto>>> GetAll()
    {
        var tasks = await _taskEntityService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<ActionResult<TaskEntityDto>> Create([FromBody] TaskEntityEditDto dto)
    {
        if (dto == null)
        {
            return BadRequest("Task data is null");
        }

        var task = await _taskEntityService.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] TaskEntityEditDto dto)
    {
        if (dto == null || id != dto.Id)
        {
            return BadRequest("Task data is invalid");
        }

        var result = await _taskEntityService.UpdateAsync(dto);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var result = await _taskEntityService.DeleteAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}