using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces
{
    public interface ITaskEntityRepository : IRepository<TaskEntity>
    {
        Task AddAsync(TaskEntity entity);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<TaskEntity>> GetAllAsync();
        Task<TaskEntity> GetByIdAsync(Guid id);
        Task<TaskEntity> GetByNameAsync(string name);
        Task UpdateAsync(TaskEntity entity);
        Task<IEnumerable<TaskEntity>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskEntity>> GetTasksByDateAsync(DateTime date);
    }
}
