using Microsoft.AspNetCore.Mvc;
using IngaCode.Application.Interfaces;

namespace IngaCode.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TimeTrackerController : ControllerBase
{
    private readonly ITimeTrackerService _timeTrackerService;

    public TimeTrackerController(ITimeTrackerService timeTrackerService)
    {
        _timeTrackerService = timeTrackerService;
    }

    [HttpPost("start")]
    public async Task<IActionResult> StartTracking(Guid taskId, Guid? collabId, [FromQuery] string timeZoneId)
    {
        try
        {
            var result = await _timeTrackerService.StartTrackingTimeAsync(taskId, collabId, timeZoneId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }

    [HttpPost("stop")]
    public async Task<IActionResult> StopTracking(Guid timeTrackerId)
    {
        try
        {
            var success = await _timeTrackerService.StopTrackingTimeAsync(timeTrackerId);
            return Ok(new { Success = success });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}