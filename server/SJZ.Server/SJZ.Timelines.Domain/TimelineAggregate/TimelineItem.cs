using SJZ.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.Timelines.Domain.TimelineAggregate
{
    public class TimelineItem : Entity<string>
    {
        public string Content { get; }

        protected TimelineItem() { }

        public TimelineItem(string content)
        {
            Content = content;
        }
    }
}
