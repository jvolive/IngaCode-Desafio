using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs.ProjectsDTOs;
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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ProjectEditDto projectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdProject = await _projectService.CreateProjectAsync(projectDto);
        return CreatedAtAction(nameof(GetProjectById), new { id = createdProject.Id }, createdProject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id, [FromBody] ProjectEditDto projectDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var success = await _projectService.UpdateProjectAsync(id, projectDto);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var success = await _projectService.DeleteProjectAsync(id);
        if (!success)
        {
            return NotFound();
        }

        return NoContent();
    }
}

