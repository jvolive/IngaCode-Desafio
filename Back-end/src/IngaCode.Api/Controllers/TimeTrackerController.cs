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
}