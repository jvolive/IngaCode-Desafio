using IngaCode.Application.DTOs;

namespace IngaCode.Application.Interfaces;

public interface IProjectService : IService<ProjectDto>
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto> GetByNameAsync(string name);
}
