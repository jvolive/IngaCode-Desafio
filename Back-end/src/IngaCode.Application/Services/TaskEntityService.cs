using IngaCode.Application.DTOs;
using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using AutoMapper;

namespace IngaCode.Application.Services;

public class TaskEntityService : ITaskEntityService
{
    private readonly ITaskEntityRepository _taskEntityRepository;
    private readonly IMapper _mapper;

    public TaskEntityService(
        ITaskEntityRepository taskEntityRepository,
        ITimeTrackerRepository timeTrackerRepository,
        IMapper mapper)
    {
        _taskEntityRepository = taskEntityRepository;

        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskEntityDto>> GetAllAsync()
    {
        var tasks = await _taskEntityRepository.GetAllAsync();
        var tasksDto = _mapper.Map<IEnumerable<TaskEntityDto>>(tasks);
        return tasksDto;
    }

    public async Task<TaskEntityDto> GetByNameAsync(string name)
    {
        var task = await _taskEntityRepository.GetByNameAsync(name);
        var taskDto = _mapper.Map<TaskEntityDto>(task);
        return taskDto;
    }

    public async Task AddAsync(TaskEntityDto taskEntityDto)
    {
        var taskEntity = _mapper.Map<TaskEntity>(taskEntityDto);
        await _taskEntityRepository.AddAsync(taskEntity);
    }

    public async Task UpdateAsync(TaskEntityDto taskEntityDto, string oldName)
    {
        var taskEntity = _mapper.Map<TaskEntity>(taskEntityDto);
        await _taskEntityRepository.UpdateByNameAsync(taskEntity, oldName);
    }

    public async Task DeleteAsync(string name)
    {
        await _taskEntityRepository.DeleteAsync(name);
    }
}