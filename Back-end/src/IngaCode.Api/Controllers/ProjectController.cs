using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
    {
        var projects = await _projectService.GetAllAsync();
        return Ok(projects);
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetByName(string name)
    {
        var project = await _projectService.GetByNameAsync(name);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }





}

