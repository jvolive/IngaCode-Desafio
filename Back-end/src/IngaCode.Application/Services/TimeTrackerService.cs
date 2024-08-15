using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;

namespace IngaCode.Application.Services
{
    public class TimeTrackerService
    {
        private readonly ITimeTrackerRepository _timeTrackerRepository;
        private readonly ITaskEntityRepository _taskRepository;

        public TimeTrackerService(ITimeTrackerRepository timeTrackerRepository, ITaskEntityRepository taskRepository)
        {
            _timeTrackerRepository = timeTrackerRepository;
            _taskRepository = taskRepository;
        }

        public async Task AddTimeTrackerAsync(TimeTracker timeTracker)
        {
            if (timeTracker.StartDate > timeTracker.EndDate)
            {
                throw new ArgumentException("O tempo de início deve ser menor ou igual ao tempo de término.");
            }

            var overlapping = await _timeTrackerRepository.GetOverlappingTimeTrackersAsync(timeTracker.TaskId, timeTracker.StartDate, timeTracker.EndDate);
            if (overlapping.Any())
            {
                throw new ArgumentException("O intervalo de tempo colide com um intervalo existente.");
            }

            await _timeTrackerRepository.AddAsync(timeTracker);
        }

        public async Task UpdateTimeTrackerAsync(TimeTracker timeTracker)
        {
            if (timeTracker.StartDate > timeTracker.EndDate)
            {
                throw new ArgumentException("O tempo de início deve ser menor ou igual ao tempo de término.");
            }
            var overlapping = await _timeTrackerRepository.GetOverlappingTimeTrackersAsync(timeTracker.TaskId, timeTracker.StartDate, timeTracker.EndDate, timeTracker.Id);
            if (overlapping.Any())
            {
                throw new ArgumentException("O intervalo de tempo colide com um intervalo existente.");
            }

            await _timeTrackerRepository.UpdateAsync(timeTracker);
        }

        public async Task DeleteTimeTrackerAsync(Guid id)
        {
            await _timeTrackerRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<TimeTracker>> GetAllTimeTrackersAsync()
        {
            return await _timeTrackerRepository.GetAllAsync();
        }

        public async Task<TimeTracker> GetTimeTrackerByIdAsync(Guid id)
        {
            return await _timeTrackerRepository.GetByIdAsync(id);
        }
        public async Task<IEnumerable<TimeTracker>> GetTimeTrackersByTaskIdAsync(Guid taskId)
        {
            return await _timeTrackerRepository.GetByTaskIdAsync(taskId);
        }

        public async Task<string> GetDailyTotalHoursAsync(DateTime date)
        {
            var timeTrackers = await _timeTrackerRepository.GetTimeTrackersByDateAsync(date);
            var totalSeconds = timeTrackers.Sum(tt => (tt.EndDate - tt.StartDate).TotalSeconds);
            var totalHours = TimeSpan.FromSeconds(totalSeconds);

            return $"{totalHours.Hours:D2}:{totalHours.Minutes:D2}";
        }

        public async Task<string> GetMonthlyTotalHoursAsync(DateTime month)
        {
            var timeTrackers = await _timeTrackerRepository.GetTimeTrackersByMonthAsync(month);
            var totalSeconds = timeTrackers.Sum(tt => (tt.EndDate - tt.StartDate).TotalSeconds);
            var totalHours = TimeSpan.FromSeconds(totalSeconds);

            return $"{totalHours.Hours:D2}:{totalHours.Minutes:D2}";
        }
    }
}
