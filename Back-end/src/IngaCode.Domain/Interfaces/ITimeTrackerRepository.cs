using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces;

public interface ITimeTrackerRepository
{
    Task<IEnumerable<TimeTracker>> GetByTaskIdAsync(Guid taskId);
    Task<TimeTracker> GetByIdAsync(Guid id);
    Task AddAsync(TimeTracker entity);
    Task UpdateAsync(TimeTracker entity, Guid id);
    Task DeleteAsync(Guid id);
    Task StartTimetrackerAsync(Guid id, DateTime startDateTime);
    Task StopTimetrackerAsync(Guid id, DateTime endDateTime);
}