using IngaCode.Domain.Entities;

namespace IngaCode.Application.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<Project>> GetAllProjectsAsync();
        Task<Project> GetProjectByIdAsync(Guid id);
        Task<Project> CreateProjectAsync(Project project);
        Task UpdateProjectAsync(Project project);
        Task DeleteProjectAsync(Guid id);
        Task<IEnumerable<Project>> GetProjectsByCriteriaAsync(string? name = null, DateTime? createdAfter = null, DateTime? createdBefore = null);
    }
}
