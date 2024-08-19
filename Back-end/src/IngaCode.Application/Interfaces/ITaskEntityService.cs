using IngaCode.Application.DTOs;

namespace IngaCode.Application.Interfaces;

public interface ITaskEntityService : IService<TaskEntityDto>
{
    Task<IEnumerable<TaskEntityDto>> GetAllAsync();
    Task<TaskEntityDto> GetByNameAsync(string name);
    Task UpdateAsync(TaskEntityDto taskEntityDto, string T);
}