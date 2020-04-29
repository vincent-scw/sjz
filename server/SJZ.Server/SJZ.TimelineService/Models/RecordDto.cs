using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Models
{
    public class RecordDto
    {
        public string Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}
