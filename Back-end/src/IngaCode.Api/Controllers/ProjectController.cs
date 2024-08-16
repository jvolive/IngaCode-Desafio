using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.DTOs.ProjectsDTOs;
using IngaCode.Application.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;

    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetProjects()
    {
        var projects = await _projectService.GetAllProjectsAsync();
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id)
    {
        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectDto>> CreateProject(ProjectEditDto projectEditDto)
    {
        var project = await _projectService.CreateProjectAsync(projectEditDto);
        return CreatedAtAction(nameof(GetProject), new { id = project.Id }, project);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id, ProjectEditDto projectEditDto)
    {
        if (id != projectEditDto.Id)
        {
            return BadRequest();
        }

        var updated = await _projectService.UpdateProjectAsync(projectEditDto);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var deleted = await _projectService.DeleteProjectAsync(id);
        if (!deleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
