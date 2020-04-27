using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Commands
{
    [DataContract]
    public class UpsertRecordCommand : IRequest<string>
    {
        [DataMember]
        public string TimelineId { get; set; }
        [DataMember]
        public string RecordId { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public DateTimeOffset Date { get; set; }
        [DataMember]
        public string UserId { get; set; }
    }
}
