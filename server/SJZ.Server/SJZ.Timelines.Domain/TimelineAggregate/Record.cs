﻿using MongoDB.Bson.Serialization.IdGenerators;
using SJZ.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.Timelines.Domain.TimelineAggregate
{
    public class Record : Entity<string>
    {
        public string TimelineId { get; private set; }
        public string Content { get; private set; }
        public DateTimeOffset Date { get; private set; }

        protected Record() { }

        public Record(string timelineId, string content, DateTimeOffset date, string userId)
        {
            Id = StringObjectIdGenerator.Instance.GenerateId("records", this).ToString();

            TimelineId = timelineId;
            Content = content;
            Date = date;
            CreatedDate = DateTimeOffset.UtcNow;
            CreatedBy = userId;
        }

        public void UpdateContent(string content, DateTimeOffset date, string userId)
        {
            Content = content;
            Date = date;
            UpdatedDate = DateTimeOffset.UtcNow;
            UpdatedBy = userId;
        }
    }
}
