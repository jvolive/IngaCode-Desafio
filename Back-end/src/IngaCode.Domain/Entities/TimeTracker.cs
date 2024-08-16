namespace IngaCode.Domain.Entities;

public class TimeTracker
{
    public Guid Id { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime? EndDateTime { get; set; }
    public string TimeZoneId { get; set; }
    public Guid TaskId { get; set; }
    public Guid? CollabId { get; set; }
}