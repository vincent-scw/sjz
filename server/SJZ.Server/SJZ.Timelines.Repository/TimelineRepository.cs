using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SJZ.Timelines.Domain.TimelineAggregate;
using System;
using System.Threading.Tasks;

namespace SJZ.Timelines.Repository
{
    public class TimelineRepository : ITimelineRepository
    {
        private readonly IMongoCollection<Timeline> _timelines;
        public TimelineRepository(MongoConfig options)
        {
            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);

            _timelines = database.GetCollection<Timeline>("timelines");
        }

        public async Task<Timeline> CreateAsync(Timeline entity)
        {
            await _timelines.InsertOneAsync(entity);
            return entity;
        }

        public Task DeleteAsync(object id)
        {
            return _timelines.DeleteOneAsync(t => t.Id == (id as string));
        }

        public async Task<Timeline> GetAsync(object id)
        {
            return (await _timelines.FindAsync(t => t.Id == (id as string))).SingleOrDefault();
        }

        public Task UpdateAsync(Timeline entity)
        {
            return _timelines.ReplaceOneAsync(t => t.Id == entity.Id, entity);
        }
    }
}
