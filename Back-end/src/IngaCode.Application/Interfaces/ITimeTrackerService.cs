using IngaCode.Domain.Entities;

namespace IngaCode.Application.Interfaces
{
    public interface ITimeTrackerService
    {
        Task AddTimeTrackerAsync(TimeTracker timeTracker);
        Task UpdateTimeTrackerAsync(TimeTracker timeTracker);
        Task DeleteTimeTrackerAsync(Guid id);
        Task<IEnumerable<TimeTracker>> GetAllTimeTrackersAsync();
        Task<TimeTracker> GetTimeTrackerByIdAsync(Guid id);
        Task<IEnumerable<TimeTracker>> GetTimeTrackersByTaskIdAsync(Guid taskId);
        Task<string> GetDailyTotalHoursAsync(DateTime date);
        Task<string> GetMonthlyTotalHoursAsync(DateTime month);
    }
}
