using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Models
{
    public class RecordDto
    {
        public string Content { get; private set; }
        public DateTimeOffset Date { get; private set; }
    }
}
