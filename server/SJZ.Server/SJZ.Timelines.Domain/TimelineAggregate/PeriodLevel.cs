using SJZ.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.Timelines.Domain.TimelineAggregate
{
    public class PeriodLevel : Enumeration
    {
        public static readonly PeriodLevel Any = new PeriodLevel(0, "Any");
        public static readonly PeriodLevel ByDay = new PeriodLevel(1, "ByDay");
        public static readonly PeriodLevel ByMonth = new PeriodLevel(2, "ByMonth");
        public static readonly PeriodLevel ByYear = new PeriodLevel(3, "ByYear");

        protected PeriodLevel(int code, string name) : base(code, name, name)
        {
        }
    }
}
