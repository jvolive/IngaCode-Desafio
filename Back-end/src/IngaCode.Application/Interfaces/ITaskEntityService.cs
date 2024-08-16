using IngaCode.Application.DTOs.TaskEntity;

namespace IngaCode.Application.Interfaces
{
    public interface ITaskEntityService
    {
        Task<TaskEntityDto> CreateTaskAsync(TaskEntityCreateDto dto);
        Task<IEnumerable<TaskEntityDto>> GetAllTasksAsync();
        Task<TaskEntityDto?> GetTaskByIdAsync(int id);
        Task UpdateTaskAsync(int id, TaskEntityUpdateDto dto);
        Task DeleteTaskAsync(int id);
    }
}
