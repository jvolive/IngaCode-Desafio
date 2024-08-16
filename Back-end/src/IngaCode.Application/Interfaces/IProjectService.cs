using IngaCode.Application.DTOs.ProjectsDTOs;

namespace IngaCode.Application.Interfaces;

public interface IProjectService
{
    Task<ProjectDto> GetByIdAsync(Guid id);
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto> CreateAsync(ProjectEditDto projectDto);
    Task<bool> UpdateAsync(ProjectEditDto projectDto);
    Task<bool> DeleteAsync(Guid id);
}
