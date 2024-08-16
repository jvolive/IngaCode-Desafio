namespace IngaCode.Application.DTOs.TimeTrackerDTOs
{
    public class TimeTrackerDto
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeZone { get; set; }
    }

}