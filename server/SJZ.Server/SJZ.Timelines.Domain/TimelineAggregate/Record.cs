using MongoDB.Bson.Serialization.IdGenerators;
using SJZ.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.Timelines.Domain.TimelineAggregate
{
    public class Record : Entity<string>
    {
        public string TimelineId { get; }
        public string Content { get; private set; }

        protected Record() { }

        public Record(string timelineId, string content)
        {
            Id = StringObjectIdGenerator.Instance.GenerateId("records", this).ToString();

            TimelineId = timelineId;
            Content = content;
            CreatedDate = DateTimeOffset.UtcNow;
        }

        public void UpdateContent(string content)
        {
            Content = content;
        }
    }
}
