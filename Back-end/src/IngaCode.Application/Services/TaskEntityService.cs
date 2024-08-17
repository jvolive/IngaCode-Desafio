using IngaCode.Application.DTOs;
using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using AutoMapper;

namespace IngaCode.Application.Services;

public class TaskEntityService : ITaskEntityService
{
    private readonly ITaskEntityRepository _taskRepository;
    private readonly IMapper _mapper;

    public TaskEntityService(
        ITaskEntityRepository taskRepository,
        ITimeTrackerRepository timeTrackerRepository,
        IMapper mapper)
    {
        _taskRepository = taskRepository;

        _mapper = mapper;
    }

    public async Task<IEnumerable<TaskEntityDto>> GetAllAsync()
    {
        var tasks = await _taskRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TaskEntityDto>>(tasks);
    }

    public async Task<TaskEntityDto> GetByNameAsync(string name)
    {
        var task = await _taskRepository.GetByNameAsync(name);
        return _mapper.Map<TaskEntityDto>(task);
    }

    public async Task AddAsync(TaskEntity entity)
    {
        await _taskRepository.AddAsync(entity);
    }

    public async Task UpdateAsync(TaskEntity entity)
    {
        await _taskRepository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(string name)
    {
        await _taskRepository.DeleteAsync(name);
    }
}