using IngaCode.Application.DTOs.ProjectsDTOs;

namespace IngaCode.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDto> CreateProjectAsync(ProjectCreateDto dto);
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto?> GetProjectByIdAsync(int id);
        Task UpdateProjectAsync(int id, ProjectUpdateDto dto);
        Task DeleteProjectAsync(int id);
    }
}
