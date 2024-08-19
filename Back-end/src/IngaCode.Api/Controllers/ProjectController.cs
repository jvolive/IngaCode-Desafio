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
            return NotFound("Project not found");
        }
        return Ok(project);
    }

    [HttpPost]
    public async Task<IActionResult> AddProject(ProjectDto projectDto)
    {
        await _projectService.AddAsync(projectDto);
        return Ok("Project saved successfully");
    }

    [HttpPut("{oldName}")]
    public async Task<IActionResult> UpdateProject(string oldName, [FromBody] ProjectDto projectDto)
    {
        await _projectService.UpdateAsync(projectDto, oldName);
        return Ok("Project updated successfully");
    }


    [HttpDelete("{name}")]
    public async Task<IActionResult> DeleteProject(string name)
    {
        await _projectService.DeleteAsync(name);
        return Ok("Project deleted successfully");
    }
}