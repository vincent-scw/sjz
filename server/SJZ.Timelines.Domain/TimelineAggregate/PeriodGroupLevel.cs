using SJZ.Common.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace SJZ.Timelines.Domain.TimelineAggregate
{
    public class PeriodGroupLevel : Enumeration
    {
        public static readonly PeriodGroupLevel ByDay = new PeriodGroupLevel(1, "ByDay");
        public static readonly PeriodGroupLevel ByMonth = new PeriodGroupLevel(2, "ByMonth");
        public static readonly PeriodGroupLevel ByYear = new PeriodGroupLevel(3, "ByYear");

        protected PeriodGroupLevel(int code, string name) : base(code, name, name)
        {
        }
    }
}
