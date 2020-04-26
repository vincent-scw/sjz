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
    public class MergeTimelineCommand : IRequest<string>
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string UserId { get; set; }
        [DataMember]
        public string Username { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public DateTimeOffset StartTime { get; set; }
        [DataMember]
        public bool IsCompleted { get; set; }
        [DataMember]
        public PeriodLevelType PeriodLevel { get; set; }
    }
}
