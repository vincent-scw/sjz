using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Commands
{
    public class DeleteCommand : IRequest
    {
        public string TimelineId { get; set; }
        public string RecordId { get; set; }
        public string UserId { get; set; }
    }
}
