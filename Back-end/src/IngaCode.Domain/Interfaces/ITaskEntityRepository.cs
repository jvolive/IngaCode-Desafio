using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces
{
    public interface ITaskEntityRepository : IRepository<TaskEntity>
    {
        Task<TaskEntity> GetByNameAsync(string name);
    }
}
