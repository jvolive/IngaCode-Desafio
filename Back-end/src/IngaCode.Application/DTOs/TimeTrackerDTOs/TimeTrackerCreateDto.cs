namespace IngaCode.Application.DTOs.TimeTrackerDTOs
{
    public class TimeTrackerCreateDto
    {
        public Guid TaskId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeZone { get; set; }
        public object Id { get; set; }
    }
}