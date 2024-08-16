using AutoMapper;
using IngaCode.Application.Interfaces;
using IngaCode.Application.DTOs;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<TaskDto> CreateTaskAsync(TaskCreateDto dto)
        {
            var taskEntity = _mapper.Map<TaskEntity>(dto);
            await _taskRepository.AddAsync(taskEntity);
            return _mapper.Map<TaskDto>(taskEntity);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto?> GetTaskByIdAsync(int id)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            return taskEntity == null ? null : _mapper.Map<TaskDto>(taskEntity);
        }

        public async Task UpdateTaskAsync(int id, TaskUpdateDto dto)
        {
            var taskEntity = await _taskRepository.GetByIdAsync(id);
            if (taskEntity != null)
            {
                _mapper.Map(dto, taskEntity);
                await _taskRepository.UpdateAsync(taskEntity);
            }
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }
    }
}
