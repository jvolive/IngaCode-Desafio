using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;

namespace IngaCode.Application.Services
{
    public class TaskEntityService
    {
        private readonly ITaskEntityRepository _taskRepository;
        private readonly ITimeTrackerRepository _timeTrackerRepository;

        public TaskEntityService(ITaskEntityRepository taskRepository, ITimeTrackerRepository timeTrackerRepository)
        {
            _taskRepository = taskRepository;
            _timeTrackerRepository = timeTrackerRepository;
        }

        public async Task<IEnumerable<TaskEntity>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<TaskEntity> GetTaskByIdAsync(Guid id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task<TaskEntity> CreateTaskAsync(TaskEntity task)
        {
            await _taskRepository.AddAsync(task);
            return task;
        }

        public async Task UpdateTaskAsync(TaskEntity task)
        {
            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByProjectIdAsync(Guid projectId)
        {
            return await _taskRepository.GetByProjectIdAsync(projectId);
        }

        public async Task<IEnumerable<TaskEntity>> GetTasksByDateAsync(DateTime date)
        {
            return await _taskRepository.GetTasksByDateAsync(date);
        }
    }
}
