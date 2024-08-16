using IngaCode.Domain.Entities;

namespace IngaCode.Domain.Interfaces;

public interface ITimeTrackerRepository
{
    Task AddAsync(TimeTracker entity);
    Task<TimeTracker> GetByIdAsync(Guid id);
    Task UpdateAsync(TimeTracker entity);
}