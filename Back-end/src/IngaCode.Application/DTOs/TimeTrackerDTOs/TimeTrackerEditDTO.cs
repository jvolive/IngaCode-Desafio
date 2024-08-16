namespace IngaCode.Application.DTOs.TimeTrackerDTOs;

public class TimeTrackerEditDto
{
    public Guid TaskId { get; set; }
    public Guid? CollabId { get; set; }
    public string TimeZoneId { get; set; }
}