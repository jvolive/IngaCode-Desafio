using IngaCode.Application.DTOs.TaskEntity;

namespace IngaCode.Application.Interfaces;

public interface ITaskEntityService
{
    Task<TaskEntityDto> GetTaskEntityByIdAsync(Guid id);
    Task<IEnumerable<TaskEntityDto>> GetAllTaskEntityAsync();
    Task<TaskEntityDto> CreateTaskEntityAsync(TaskEntityEditDto taskEntityDto);
    Task<bool> UpdateTaskEntityAsync(Guid id, TaskEntityEditDto taskEntityDto);
    Task<bool> DeleteTaskEntityAsync(Guid id);
}