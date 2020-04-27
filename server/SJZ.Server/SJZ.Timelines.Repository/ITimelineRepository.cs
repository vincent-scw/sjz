using SJZ.Common;
using SJZ.Timelines.Domain.TimelineAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SJZ.Timelines.Repository
{
    public interface ITimelineRepository : IRepository<Timeline>
    {
        Task<Record> CreateRecordAsync(Record record);
        Task DeleteRecordAsync(string id);
        Task<Record> GetRecordAsync(string id);
        Task<IEnumerable<Record>> GetRecordsAsync(string timelineId);
        Task UpdateRecordAsync(Record record);
    }
}
