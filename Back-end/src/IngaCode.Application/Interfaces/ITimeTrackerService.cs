using IngaCode.Application.DTOs;

namespace IngaCode.Application.Interfaces;
public interface ITimeTrackerService
{
    Task<IEnumerable<TimeTrackerDto>> GetByTaskIdAsync(Guid taskId);
    Task<TimeTrackerDto> GetByIdAsync(Guid id);
    Task AddAsync(TimeTrackerDto timeTrackerDto);
    Task UpdateAsync(TimeTrackerDto timeTrackerDto, Guid id);
    Task DeleteAsync(Guid id);
    Task StartTimeTrackerAsync(Guid id, Guid taskId, DateTime startDateTime);
    Task StopTimeTrackerAsync(Guid id, Guid taskId, DateTime endDateTime);
}

