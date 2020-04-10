using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using SJZ.Common.Domain;
using System;
using System.Collections.Generic;

namespace SJZ.Timelines.Domain.TimelineAggregate
{
    public class Timeline : Entity<string>, IAggregateRoot
    {
        public string Username { get; }
        public string Title { get; }
        public DateTimeOffset StartTime { get; }
        public bool IsCompleted { get; }
        public PeriodLevel PeriodLevel { get; }

        private List<TimelineItem> _items;
        public IReadOnlyCollection<TimelineItem> Items => _items;

        protected Timeline() 
        {
            _items = new List<TimelineItem>();
        }

        public Timeline(string title, 
            DateTimeOffset startTime,
            bool isCompleted,
            PeriodLevel periodLevel,
            string userid,
            string username)
            : this()
        {
            Id = StringObjectIdGenerator.Instance.GenerateId("timelines", this).ToString();

            Title = title;
            StartTime = startTime;
            IsCompleted = isCompleted;
            PeriodLevel = periodLevel;
            CreatedBy = userid;
            Username = username;
        }

        public void AddItem(TimelineItem item)
        {
            _items.Add(item);
        }

        public void AddItems(List<TimelineItem> items)
        {
            _items.AddRange(items);
        }
    }
}
