using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces;

public interface ITimeTrackerRepository
{
    Task AddAsync(TimeTracker entity);
    Task<TimeTracker> GetByIdAsync(Guid id);
    Task UpdateAsync(TimeTracker entity);
    Task<IEnumerable<TimeTracker>> GetByTaskIdAsync(Guid taskId);
    Task DeleteAsync(Guid id);
}