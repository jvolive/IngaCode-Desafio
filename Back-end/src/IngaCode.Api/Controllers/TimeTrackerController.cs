using IngaCode.Application.DTOs;
using IngaCode.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IngaCode.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeTrackerController : ControllerBase
{
    private readonly ITimeTrackerService _timeTrackerService;

    public TimeTrackerController(ITimeTrackerService timeTrackerService)
    {
        _timeTrackerService = timeTrackerService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var timeTracker = await _timeTrackerService.GetByIdAsync(id);
        if (timeTracker == null)
            return NotFound();

        return Ok(timeTracker);
    }

    [HttpGet("task/{taskId}")]
    public async Task<IActionResult> GetByTaskId(Guid taskId)
    {
        var timeTrackers = await _timeTrackerService.GetByTaskIdAsync(taskId);
        return Ok(timeTrackers);
    }

    [HttpPost]
    public async Task<IActionResult> AddTimeTracker([FromBody] TimeTrackerDto timeTrackerDto)
    {
        await _timeTrackerService.AddAsync(timeTrackerDto);
        return Ok("Time Tracker saved successfully");
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTimeTracker(Guid id, [FromBody] TimeTrackerDto timeTrackerDto)
    {
        await _timeTrackerService.UpdateAsync(timeTrackerDto, id);
        return Ok("Updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimeTracker(Guid id)
    {
        await _timeTrackerService.DeleteAsync(id);
        return Ok("Deleted successfully");
    }

    [HttpPut("start/task/{taskId}")]
    public async Task<IActionResult> StartTimeTracker(Guid taskId, [FromBody] Guid id, DateTime startDateTime)
    {
        await _timeTrackerService.StartTimeTrackerAsync(id, taskId, startDateTime);
        return Ok("Time tracker start successfully");
    }

    [HttpPut("stop/task/{taskId}")]
    public async Task<IActionResult> StopTimeTracker(Guid taskId, [FromQuery] Guid id, [FromBody] DateTime endDateTime)
    {
        await _timeTrackerService.StopTimeTrackerAsync(id, taskId, endDateTime);
        return Ok("Time tracker stopped successfully.");
    }
}