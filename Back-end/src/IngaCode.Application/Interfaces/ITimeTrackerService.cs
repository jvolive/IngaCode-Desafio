using IngaCode.Application.DTOs;
using IngaCode.Application.DTOs.TimeTrackerDTOs;

namespace IngaCode.Application.Interfaces;

public interface ITimeTrackerService
{
    Task<TimeTrackerDto> CreateTimeTrackerAsync(TimeTrackerEditDto timeTrackerCreateDto);
    Task<TimeTrackerDto> UpdateTimeTrackerAsync(Guid id, TimeTrackerEditDto timeTrackerEditDto);
    Task<TimeTrackerDto> GetTimeTrackerByIdAsync(Guid id);
    Task<IEnumerable<TimeTrackerDto>> GetAllTimeTrackersAsync();
    Task DeleteTimeTrackerAsync(Guid id);
}

