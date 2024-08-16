using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces
{
    public interface ITaskEntityRepository : IRepository<TaskEntity>
    {
        Task<IEnumerable<TaskEntity>> GetByProjectIdAsync(Guid projectId);
        Task<IEnumerable<TaskEntity>> GetTasksByDateAsync(DateTime date);
    }
}
