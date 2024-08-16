using IngaCode.Application.DTOs.ProjectsDTOs;

namespace IngaCode.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDto> GetProjectByIdAsync(Guid id);
    Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
    Task<ProjectDto> CreateProjectAsync(ProjectEditDto projectDto);
    Task<bool> UpdateProjectAsync(Guid id, ProjectEditDto projectDto);
    Task<bool> DeleteProjectAsync(Guid id);
}
