using SJZ.Timelines.Domain.TimelineAggregate;
using System;
using System.Threading.Tasks;

namespace SJZ.Timelines.Repository
{
    public class TimelineRepository : ITimelineRepository
    {
        public Task<Timeline> CreateAsync(Timeline entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Timeline> GetAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<Timeline> UpdateAsync(Timeline entity)
        {
            throw new NotImplementedException();
        }
    }
}
