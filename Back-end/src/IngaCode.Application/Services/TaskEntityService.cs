using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs;
using IngaCode.Application.DTOs.TaskEntity;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using AutoMapper;

namespace IngaCode.Application.Services;

public class TaskEntityService : ITaskEntityService
{
    private readonly ITaskEntityRepository _taskRepository;
    private readonly ITimeTrackerRepository _timeTrackerRepository;
    private readonly IMapper _mapper;

    public TaskEntityService(
        ITaskEntityRepository taskRepository,
        ITimeTrackerRepository timeTrackerRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;
        _timeTrackerRepository = timeTrackerRepository;
        _mapper = mapper;
    }

    public async Task<TaskEntityDto?> GetByIdAsync(Guid id)
    {
        var taskEntity = await _taskRepository.GetByIdAsync(id);
        if (taskEntity == null) return null;

        var taskDto = _mapper.Map<TaskEntityDto>(taskEntity);

        var timeTrackers = await _timeTrackerRepository.GetByTaskIdAsync(id);
        taskDto.TimeTrackers = _mapper.Map<IEnumerable<TimeTrackerDto>>(timeTrackers);

        return taskDto;
    }

    public async Task<IEnumerable<TaskEntityDto>> GetAllAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();
        var taskDtos = _mapper.Map<IEnumerable<TaskEntityDto>>(tasks);


        foreach (var taskDto in taskDtos)
        {
            var timeTrackers = await _timeTrackerRepository.GetByTaskIdAsync(taskDto.Id);
            taskDto.TimeTrackers = _mapper.Map<IEnumerable<TimeTrackerDto>>(timeTrackers);
        }

        return taskDtos;
    }

    public async Task<TaskEntityDto> CreateAsync(TaskEntityEditDto dto)
    {

        var taskEntity = _mapper.Map<TaskEntity>(dto);

        await _taskRepository.AddAsync(taskEntity);


        if (dto.TimeTrackers != null)
        {
            foreach (var trackerDto in dto.TimeTrackers)
            {
                var tracker = _mapper.Map<TimeTracker>(trackerDto);
                tracker.TaskId = taskEntity.Id;
                await _timeTrackerRepository.AddAsync(tracker);
            }
        }


        var createdTask = await _taskRepository.GetByIdAsync(taskEntity.Id);
        return _mapper.Map<TaskEntityDto>(createdTask);
    }

    public async Task<bool> UpdateAsync(TaskEntityEditDto dto)
    {
        var taskEntity = await _taskRepository.GetByIdAsync(dto.Id);
        if (taskEntity == null) return false;


        _mapper.Map(dto, taskEntity);
        await _taskRepository.UpdateAsync(taskEntity);

        if (dto.TimeTrackers != null)
        {
            var existingTrackers = await _timeTrackerRepository.GetByTaskIdAsync(dto.Id);
            foreach (var existingTracker in existingTrackers)
            {
                await _timeTrackerRepository.DeleteAsync(existingTracker.Id);
            }

            foreach (var trackerDto in dto.TimeTrackers)
            {
                var tracker = _mapper.Map<TimeTracker>(trackerDto);
                tracker.TaskId = taskEntity.Id;
                await _timeTrackerRepository.AddAsync(tracker);
            }
        }

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var timeTrackers = await _timeTrackerRepository.GetByTaskIdAsync(id);
            foreach (var tracker in timeTrackers)
            {
                await _timeTrackerRepository.DeleteAsync(tracker.Id);
            }

            await _taskRepository.DeleteAsync(id);
            return true;
        }
        catch
        {
            return false;
        }
    }
}

