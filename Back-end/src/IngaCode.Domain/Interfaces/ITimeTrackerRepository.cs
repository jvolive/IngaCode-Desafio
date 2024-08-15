using IngaCode.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IngaCode.Domain.Interfaces
{
    public interface ITimeTrackerRepository : IRepository<TimeTracker>
    {
        Task<IEnumerable<TimeTracker>> GetOverlappingTimeTrackersAsync(Guid taskId, DateTime startDate, DateTime endDate, Guid? excludeId = null);
        Task<IEnumerable<TimeTracker>> GetByTaskIdAsync(Guid taskId);
        Task<IEnumerable<TimeTracker>> GetTimeTrackersByDateAsync(DateTime date);
        Task<IEnumerable<TimeTracker>> GetTimeTrackersByMonthAsync(DateTime month);
    }
}
