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

        public async Task AddTimeTrackerAsync(TimeTrackerCreateDto timeTrackerDto)
        {
            var timeTracker = _mapper.Map<TimeTracker>(timeTrackerDto);

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

        public async Task UpdateTimeTrackerAsync(TimeTrackerUpdateDto timeTrackerDto)
        {
            var timeTracker = _mapper.Map<TimeTracker>(timeTrackerDto);

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

        public async Task<IEnumerable<TimeTrackerResponseDto>> GetAllTimeTrackersAsync()
        {
            var timeTrackers = await _timeTrackerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TimeTrackerResponseDto>>(timeTrackers);
        }

        public async Task<TimeTrackerResponseDto> GetTimeTrackerByIdAsync(Guid id)
        {
            var timeTracker = await _timeTrackerRepository.GetByIdAsync(id);
            return _mapper.Map<TimeTrackerResponseDto>(timeTracker);
        }

        public async Task<IEnumerable<TimeTrackerResponseDto>> GetTimeTrackersByTaskIdAsync(Guid taskId)
        {
            var timeTrackers = await _timeTrackerRepository.GetByTaskIdAsync(taskId);
            return _mapper.Map<IEnumerable<TimeTrackerResponseDto>>(timeTrackers);
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
