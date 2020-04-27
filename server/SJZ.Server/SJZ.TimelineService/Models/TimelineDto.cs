using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Models
{
    public class TimelineDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public PeriodLevelType PeriodLevel { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public DateTimeOffset LastChanged { get; set; }

        public IEnumerable<RecordDto> Items { get; set; }
    }
}
