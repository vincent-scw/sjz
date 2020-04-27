using MongoDB.Bson;
using MongoDB.Bson.Serialization.IdGenerators;
using SJZ.Common.Domain;
using System;
using System.Collections.Generic;

namespace SJZ.Timelines.Domain.TimelineAggregate
{
    public class Timeline : Entity<string>, IAggregateRoot
    {
        public string Username { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public bool IsCompleted { get; private set; }
        public PeriodLevel PeriodLevel { get; private set; }

        private List<Record> _items;
        public IReadOnlyCollection<Record> Items => _items;

        protected Timeline() 
        {
            _items = new List<Record>();
        }

        public Timeline(string title, 
            string description,
            bool isCompleted,
            PeriodLevel periodLevel,
            string userid,
            string username)
            : this()
        {
            Id = shortid.ShortId.Generate(true, false, 12);

            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            PeriodLevel = periodLevel;
            CreatedBy = userid;
            Username = username;

            CreatedDate = DateTimeOffset.UtcNow;
            UpdatedDate = DateTimeOffset.UtcNow;
        }

        public void UpdateContent(string title, 
            string description, 
            bool isCompleted,
            PeriodLevel periodLevel,
            string userid)
        {
            if (userid != CreatedBy)
            {
                throw new DomainException("AccessDenied");
            }

            Title = title;
            Description = description;
            IsCompleted = isCompleted;
            PeriodLevel = periodLevel;
            UpdatedBy = userid;
            UpdatedDate = DateTimeOffset.UtcNow;
        }

        public void AddItem(Record item)
        {
            _items.Add(item);
        }

        public void AddItems(List<Record> items)
        {
            _items.AddRange(items);
        }
    }
}
