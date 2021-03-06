﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Models
{
    public class TimelineDto
    {
        public string TimelineId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public PeriodGroupLevelType PeriodGroupLevel { get; set; }
        public string OwnerId { get; set; }
        public string OwnerName { get; set; }
        public DateTimeOffset LastChanged { get; set; }

        public IEnumerable<RecordDto> Items { get; set; }
    }
}
