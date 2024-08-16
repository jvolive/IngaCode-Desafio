using IngaCode.Application.DTOs.ProjectsDTOs;

namespace IngaCode.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(ProjectCreateDto dto);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(Guid id);
        Task UpdateProjectAsync(Guid id, ProjectUpdateDto dto);
        Task DeleteProjectAsync(Guid id);
    }
}
