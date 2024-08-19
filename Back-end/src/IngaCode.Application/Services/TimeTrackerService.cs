using IngaCode.Application.DTOs;
using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using AutoMapper;

namespace IngaCode.Application.Services;

public class TimeTrackerService : ITimeTrackerService
{
    private readonly ITimeTrackerRepository _timeTrackerRepository;
    private readonly IMapper _mapper;

    public TimeTrackerService(ITimeTrackerRepository timeTrackerRepository, IMapper mapper)
    {
        _timeTrackerRepository = timeTrackerRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TimeTrackerDto>> GetByTaskIdAsync(Guid taskId)
    {
        var timeTrackers = await _timeTrackerRepository.GetByTaskIdAsync(taskId);
        return _mapper.Map<IEnumerable<TimeTrackerDto>>(timeTrackers);
    }

    public async Task<TimeTrackerDto> GetByIdAsync(Guid id)
    {
        var timeTracker = await _timeTrackerRepository.GetByIdAsync(id);
        return _mapper.Map<TimeTrackerDto>(timeTracker);
    }

    public async Task AddAsync(TimeTrackerDto timeTrackerDto)
    {
        var timeTracker = _mapper.Map<TimeTracker>(timeTrackerDto);
        await _timeTrackerRepository.AddAsync(timeTracker);
    }

    public async Task UpdateAsync(TimeTrackerDto timeTrackerDto, Guid id)
    {
        var timeTracker = _mapper.Map<TimeTracker>(timeTrackerDto);
        await _timeTrackerRepository.UpdateAsync(timeTracker, id);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _timeTrackerRepository.DeleteAsync(id);
    }
}
