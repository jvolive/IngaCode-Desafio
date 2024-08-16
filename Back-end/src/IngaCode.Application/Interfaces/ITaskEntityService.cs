using IngaCode.Application.DTOs;
using IngaCode.Application.DTOs.TaskEntity;

namespace IngaCode.Application.Interfaces;

public interface ITaskEntityService
{
    Task<TaskEntityDto> GetByIdAsync(Guid id);
    Task<IEnumerable<TaskEntityDto>> GetAllAsync();
    Task<TaskEntityDto> CreateAsync(TaskEntityEditDto taskEntityDto);
    Task<bool> UpdateAsync(TaskEntityEditDto taskEntityDto);
    Task<bool> DeleteAsync(Guid id);
}