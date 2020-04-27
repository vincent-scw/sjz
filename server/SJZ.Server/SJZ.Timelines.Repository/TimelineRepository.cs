﻿using MongoDB.Driver;
using SJZ.Timelines.Domain.TimelineAggregate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SJZ.Timelines.Repository
{
    public class TimelineRepository : ITimelineRepository
    {
        private readonly IMongoCollection<Timeline> _timelines;
        private readonly IMongoCollection<Record> _records;
        public TimelineRepository(MongoConfig options)
        {
            var client = new MongoClient(options.ConnectionString);
            var database = client.GetDatabase(options.DatabaseName);

            _timelines = database.GetCollection<Timeline>("timelines");
            _records = database.GetCollection<Record>("records");
        }

        public async Task<Timeline> CreateAsync(Timeline entity)
        {
            await _timelines.InsertOneAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(object id)
        {
            var timelineId = (id as string);
            await _records.DeleteManyAsync(r => r.TimelineId == timelineId);
            await _timelines.FindOneAndDeleteAsync(t => t.Id == timelineId);
        }

        public async Task<Timeline> GetAsync(object id)
        {
            var timeline = (await _timelines.FindAsync(t => t.Id == (id as string))).SingleOrDefault();
            if (timeline == null)
                return null;

            timeline.AddItems((await _records.FindAsync(r => r.TimelineId == timeline.Id)).ToList());
            return timeline;
        }

        public Task UpdateAsync(Timeline entity)
        {
            return _timelines.FindOneAndReplaceAsync(t => t.Id == entity.Id, entity);
        }

        public async Task<Record> CreateRecordAsync(Record record)
        {
            await _records.InsertOneAsync(record);
            return record;
        }

        public Task DeleteRecordAsync(string id)
        {
            return _records.FindOneAndDeleteAsync(r => r.Id == id);
        }

        public async Task<Record> GetRecordAsync(string id)
        {
            return (await _records.FindAsync(r => r.Id == id)).FirstOrDefault();
        }

        public async Task<IEnumerable<Record>> GetRecordsAsync(string timelineId)
        {
            return (await _records.FindAsync<Record>(r => r.TimelineId == timelineId)).ToList();
        }

        public Task UpdateRecordAsync(Record record)
        {
            return _records.FindOneAndReplaceAsync(replacement => replacement.Id == record.Id, record);
        }
    }
}
