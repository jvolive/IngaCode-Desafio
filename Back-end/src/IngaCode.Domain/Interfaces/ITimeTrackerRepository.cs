using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces
{
    public interface ITimeTrackerRepository : IRepository<TimeTracker>
    {
        Task<IEnumerable<TimeTracker>> GetByTaskIdAsync(Guid taskId);
        Task<IEnumerable<TimeTracker>> GetByCollaboratorIdAsync(Guid collaboratorId);
    }
}
