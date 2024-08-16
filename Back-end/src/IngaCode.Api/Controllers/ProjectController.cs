using IngaCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.DTOs.ProjectsDTOs;


namespace IngaCode.Api.Controllers
{
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
        public async Task<IActionResult> GetAll()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateDto dto)
        {
            var project = new Project
            {
                Name = dto.Name,
                Description = dto.Description
            };

            var createdProject = await _projectService.CreateProjectAsync(project);
            return CreatedAtAction(nameof(GetById), new { id = createdProject.Id }, createdProject);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, ProjectUpdateDto dto)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            project.Name = dto.Name;
            project.Description = dto.Description;

            await _projectService.UpdateProjectAsync(project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
