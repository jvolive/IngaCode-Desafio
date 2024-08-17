using IngaCode.Application.DTOs;

namespace IngaCode.Application.Interfaces;

public interface ITimeTrackerService
{
    Task<TimeTrackerDto> StartTrackingTimeAsync(Guid taskId, Guid? collabId, string timeZoneId);
    Task<bool> StopTrackingTimeAsync(Guid timeTrackerId);
}
