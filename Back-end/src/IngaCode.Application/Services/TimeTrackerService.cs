using IngaCode.Application.DTOs.TimeTrackerDTOs;
using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using IngaCode.Domain.Interfaces;
using AutoMapper;

namespace IngaCode.Application.Services
{
    public class TimeTrackerService : ITimeTrackerService
    {
        private readonly ITimeTrackerRepository _timeTrackerRepository;
        private readonly ITaskEntityRepository _taskRepository;
        private readonly IMapper _mapper;

        public TimeTrackerService(ITimeTrackerRepository timeTrackerRepository, ITaskEntityRepository taskRepository, IMapper mapper)
        {
            _timeTrackerRepository = timeTrackerRepository;
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<bool> CreateTimeTrackerAsync(TimeTrackerCreateDto createDto)
        {
            var timeTracker = _mapper.Map<TimeTracker>(createDto);

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
            return true;
        }

        public async Task<bool> UpdateTimeTrackerAsync(Guid id, TimeTrackerUpdateDto updateDto)
        {
            var timeTracker = _mapper.Map<TimeTracker>(updateDto);
            timeTracker.Id = id;

            if (timeTracker.StartDate > timeTracker.EndDate)
            {
                throw new ArgumentException("O tempo de início deve ser menor ou igual ao tempo de término.");
            }

            if (timeTracker.TaskId == Guid.Empty)
            {
                throw new ArgumentException("O ID da tarefa não pode ser vazio.");
            }

            var overlapping = await _timeTrackerRepository.GetOverlappingTimeTrackersAsync(timeTracker.TaskId, timeTracker.StartDate, timeTracker.EndDate, timeTracker.Id);
            if (overlapping.Any())
            {
                throw new ArgumentException("O intervalo de tempo colide com um intervalo existente.");
            }

            await _timeTrackerRepository.UpdateAsync(timeTracker);
            return true;
        }

        public async Task<bool> DeleteTimeTrackerAsync(Guid id)
        {
            await _timeTrackerRepository.DeleteAsync(id);
            return true;
        }

        public async Task<IEnumerable<TimeTrackerDto>> GetAllTimeTrackersAsync()
        {
            var timeTrackers = await _timeTrackerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TimeTrackerDto>>(timeTrackers);
        }

        public async Task<TimeTrackerDto?> GetTimeTrackerByIdAsync(Guid id)
        {
            var timeTracker = await _timeTrackerRepository.GetByIdAsync(id);
            return _mapper.Map<TimeTrackerDto>(timeTracker);
        }

        public async Task<IEnumerable<TimeTrackerDto>> GetTimeTrackersByTaskIdAsync(Guid taskId)
        {
            var timeTrackers = await _timeTrackerRepository.GetByTaskIdAsync(taskId);
            return _mapper.Map<IEnumerable<TimeTrackerDto>>(timeTrackers);
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
