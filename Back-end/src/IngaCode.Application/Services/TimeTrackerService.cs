using IngaCode.Application.DTOs;
using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using AutoMapper;

namespace IngaCode.Application.Services;

public class TimeTrackerService : ITimeTrackerService
{
    private readonly ITimeTrackerRepository _timeTrackerRepository;
    private readonly ITaskEntityRepository _taskEntityRepository;
    private readonly IMapper _mapper;

    public TimeTrackerService(ITimeTrackerRepository timeTrackerRepository, ITaskEntityRepository taskEntityRepository, IMapper mapper)
    {
        _timeTrackerRepository = timeTrackerRepository;
        _taskEntityRepository = taskEntityRepository;
        _mapper = mapper;
    }

    public async Task<TimeTrackerDto> StartTrackingTimeAsync(Guid taskId, Guid? collabId, string timeZoneId)
    {
        if (string.IsNullOrWhiteSpace(timeZoneId))
            throw new ArgumentException("TimeZoneId cannot be null or empty", nameof(timeZoneId));

        var task = await _taskEntityRepository.GetByIdAsync(taskId);
        if (task == null)
            throw new Exception("Task not found");

        var timeTracker = new TimeTracker
        {
            Id = Guid.NewGuid(),
            StartDateTime = DateTime.UtcNow,
            TaskId = taskId,
            CollabId = collabId,
            TimeZoneId = timeZoneId
        };

        await _timeTrackerRepository.AddAsync(timeTracker);

        return _mapper.Map<TimeTrackerDto>(timeTracker);
    }

    public async Task<bool> StopTrackingTimeAsync(Guid timeTrackerId)
    {
        var timeTracker = await _timeTrackerRepository.GetByIdAsync(timeTrackerId);
        if (timeTracker == null)
            throw new Exception("TimeTracker not found");

        timeTracker.EndDateTime = DateTime.UtcNow;
        await _timeTrackerRepository.UpdateAsync(timeTracker);

        return true;
    }
}