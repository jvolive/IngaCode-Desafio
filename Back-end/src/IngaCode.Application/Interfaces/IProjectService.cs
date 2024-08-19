using IngaCode.Application.DTOs;
using IngaCode.Domain.Entities;

namespace IngaCode.Application.Interfaces;

public interface IProjectService : IService<ProjectDto>
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto> GetByNameAsync(string name);
    Task UpdateAsync(ProjectDto projectDto, string T);
}
