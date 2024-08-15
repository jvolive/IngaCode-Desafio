using IngaCode.Domain.Entities;

namespace IngaCode.Application.Interfaces
{
    public interface ITaskEntityService
    {
        Task<IEnumerable<TaskEntity>> GetAllTasksAsync();
        Task<TaskEntity> GetTaskByIdAsync(Guid id);
        Task<TaskEntity> CreateTaskAsync(TaskEntity task);
        Task UpdateTaskAsync(TaskEntity task);
        Task DeleteTaskAsync(Guid id);
        Task<IEnumerable<TaskEntity>> GetTasksByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskEntity>> GetTasksByDateAsync(DateTime date);
    }
}
