using IngaCode.Application.DTOs;
using IngaCode.Domain.Entities;

namespace IngaCode.Application.Interfaces;

public interface IProjectService : IService<Project>
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto> GetByNameAsync(string name);
}
