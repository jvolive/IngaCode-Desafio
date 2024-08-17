using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs;
using Microsoft.AspNetCore.Authorization;

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
            return NotFound();
        }
        return Ok(task);
    }
}