using SJZ.Common;
using SJZ.Timelines.Domain.TimelineAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SJZ.Timelines.Repository
{
    public interface ITimelineRepository
    {
        Task<Timeline> CreateAsync(Timeline entity);
        Task<Timeline> GetAsync(object id);
        Task DeleteAsync(object id);
        Task UpdateAsync(Timeline entity);

        Task<IEnumerable<Timeline>> GetListAsync(string userId);
        Task<Record> CreateRecordAsync(Record record);
        Task DeleteRecordAsync(string id);
        Task<Record> GetRecordAsync(string id);
        Task<IEnumerable<Record>> GetRecordsAsync(string timelineId);
        Task UpdateRecordAsync(Record record);
    }
}
