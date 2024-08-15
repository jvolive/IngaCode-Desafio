using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IngaCode.Application.DTOs.TimerTrackerDTOs
{
    public class TimeTrackerCreateDto
    {
        public Guid TaskId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeZone { get; set; }
    }
}