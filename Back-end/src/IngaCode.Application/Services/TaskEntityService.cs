using AutoMapper;
using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs.TaskEntity;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;

namespace IngaCode.Application.Services
{
    public class TaskEntityService : ITaskEntityService
    {
        private readonly ITaskEntityRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskEntityService(ITaskEntityRepository taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskEntityDto> CreateTaskAsync(TaskEntityCreateDto dto)
        {
            var taskEntity = _mapper.Map<TaskEntity>(dto);
            await _taskRepository.AddAsync(taskEntity);
            return _mapper.Map<TaskEntityDto>(taskEntity);
        }

        public async Task<IEnumerable<TaskEntityDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskEntityDto>>(tasks);
        }

        public async Task<TaskEntityDto?> GetTaskByIdAsync(Guid id)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            return taskEntity == null ? null : _mapper.Map<TaskEntityDto>(taskEntity);
        }

        public async Task UpdateTaskAsync(Guid id, TaskEntityUpdateDto dto)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            if (taskEntity != null)
            {
                _mapper.Map(dto, taskEntity);
                await _taskRepository.UpdateAsync(taskEntity);
            }
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
