using MediatR;
using SJZ.TimelineService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Commands
{
    [DataContract]
    public class CreateTimelineCommand : IRequest<string>
    {
        [DataMember]
        public string UserId { get; }
        [DataMember]
        public string Username { get; }
        [DataMember]
        public string Title { get; }
        [DataMember]
        public DateTimeOffset StartTime { get; }
        [DataMember]
        public bool IsCompleted { get; }
        [DataMember]
        public PeriodLevelType PeriodLevel { get; }
    }
}
