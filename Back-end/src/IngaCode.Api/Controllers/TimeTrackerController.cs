using IngaCode.Application.DTOs.TimeTrackerDTOs;
using IngaCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTrackerController : ControllerBase
    {
        private readonly ITimeTrackerService _timeTrackerService;

        public TimeTrackerController(ITimeTrackerService timeTrackerService)
        {
            _timeTrackerService = timeTrackerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TimeTrackerDto>>> GetAllTimeTrackers()
        {
            var timeTrackers = await _timeTrackerService.GetAllTimeTrackersAsync();
            return Ok(timeTrackers);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<TimeTrackerDto>> GetTimeTrackerById(Guid id)
        {
            var timeTracker = await _timeTrackerService.GetTimeTrackerByIdAsync(id);
            if (timeTracker == null)
            {
                return NotFound();
            }
            return Ok(timeTracker);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTimeTracker([FromBody] TimeTrackerCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest();
            }

            var result = await _timeTrackerService.CreateTimeTrackerAsync(createDto);
            if (result)
            {
                return CreatedAtAction(nameof(GetTimeTrackerById), new { id = createDto.Id }, createDto);
            }
            return BadRequest("Unable to create time tracker.");
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> UpdateTimeTracker(Guid id, [FromBody] TimeTrackerUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id)
            {
                return BadRequest();
            }

            var result = await _timeTrackerService.UpdateTimeTrackerAsync(id, updateDto);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteTimeTracker(Guid id)
        {
            var result = await _timeTrackerService.DeleteTimeTrackerAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
