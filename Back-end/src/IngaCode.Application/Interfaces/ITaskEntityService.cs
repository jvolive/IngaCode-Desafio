using IngaCode.Application.DTOs.TaskEntity;

namespace IngaCode.Application.Interfaces
{
    public interface ITaskEntityService
    {
        Task<TaskEntityDto> CreateTaskAsync(TaskEntityCreateDto dto);
        Task<IEnumerable<TaskEntityDto>> GetAllTasksAsync();
        Task<TaskEntityDto?> GetTaskByIdAsync(Guid id);
        Task UpdateTaskAsync(Guid id, TaskEntityUpdateDto dto);
        Task DeleteTaskAsync(Guid id);
    }
}
