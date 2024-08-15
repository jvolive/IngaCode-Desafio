using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;

namespace IngaCode.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {
            return await _projectRepository.GetAllAsync();
        }

        public async Task<Project> GetProjectByIdAsync(Guid id)
        {
            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            await _projectRepository.AddAsync(project);
            return project;
        }

        public async Task UpdateProjectAsync(Project project)
        {
            await _projectRepository.UpdateAsync(project);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Project>> GetProjectsByCriteriaAsync(string? name = null, DateTime? createdAfter = null, DateTime? createdBefore = null)
        {
            var projects = await _projectRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(name))
            {
                projects = projects.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            if (createdAfter.HasValue)
            {
                projects = projects.Where(p => p.CreatedAt >= createdAfter.Value);
            }

            if (createdBefore.HasValue)
            {
                projects = projects.Where(p => p.CreatedAt <= createdBefore.Value);
            }

            return projects;
        }
    }
}
