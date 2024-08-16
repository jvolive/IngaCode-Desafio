using IngaCode.Application.DTOs.TimeTrackerDTOs;


namespace IngaCode.Application.Interfaces
{
    public interface ITimeTrackerService
    {
        Task<IEnumerable<TimeTrackerDto>> GetAllTimeTrackersAsync();
        Task<TimeTrackerDto?> GetTimeTrackerByIdAsync(Guid id);
        Task<bool> CreateTimeTrackerAsync(TimeTrackerCreateDto createDto);
        Task<bool> UpdateTimeTrackerAsync(Guid id, TimeTrackerUpdateDto updateDto);
        Task<bool> DeleteTimeTrackerAsync(Guid id);
    }

}
