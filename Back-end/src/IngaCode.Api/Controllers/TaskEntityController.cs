using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs;
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
        var tasks = await _taskEntityService.GetAllAsync();
        return Ok(tasks);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var task = await _taskEntityService.GetByNameAsync(name);
        if (task == null)
        {
            return NotFound("Task not Found");
        }
        return Ok(task);
    }

    [HttpPost]
    public async Task<IActionResult> AddTaskEntity(TaskEntityDto taskEntityDto)
    {
        await _taskEntityService.AddAsync(taskEntityDto);
        return Ok("Task saved successfully");
    }

    [HttpPut("{oldName}")]
    public async Task<IActionResult> UpdateTaskEntity(string oldName, [FromBody] TaskEntityDto taskEntityDto)
    {
        await _taskEntityService.UpdateAsync(taskEntityDto, oldName);
        return Ok("Task updated successfully");
    }

    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteTaskEntity(string name)
    {
        await _taskEntityService.DeleteAsync(name);
        return Ok("Task deleted successfully");
    }

}