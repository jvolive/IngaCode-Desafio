using IngaCode.Application.Interfaces;
using IngaCode.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeTrackerController : ControllerBase
    {
        private readonly ITimeTrackerService _timeTrackerService;

        public TimeTrackerController(ITimeTrackerService timeTrackerService)
        {
            _timeTrackerService = timeTrackerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var timeTrackers = await _timeTrackerService.GetAllTimeTrackersAsync();
            return Ok(timeTrackers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var timeTracker = await _timeTrackerService.GetTimeTrackerByIdAsync(id);

            if (timeTracker == null)
                return NotFound();

            return Ok(timeTracker);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TimeTracker timeTracker)
        {
            await _timeTrackerService.AddTimeTrackerAsync(timeTracker);
            return CreatedAtAction(nameof(GetById), new { id = timeTracker.Id }, timeTracker);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TimeTracker timeTracker)
        {
            if (id != timeTracker.Id)
                return BadRequest();

            await _timeTrackerService.UpdateTimeTrackerAsync(timeTracker);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _timeTrackerService.DeleteTimeTrackerAsync(id);
            return NoContent();
        }

        [HttpGet("daily/{date}")]
        public async Task<IActionResult> GetDailyTotalHours(DateTime date)
        {
            var totalHours = await _timeTrackerService.GetDailyTotalHoursAsync(date);
            return Ok(new { totalHours });
        }

        [HttpGet("monthly/{month}")]
        public async Task<IActionResult> GetMonthlyTotalHours(DateTime month)
        {
            var totalHours = await _timeTrackerService.GetMonthlyTotalHoursAsync(month);
            return Ok(new { totalHours });
        }
    }
}
