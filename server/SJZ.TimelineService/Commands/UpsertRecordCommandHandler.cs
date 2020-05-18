using MediatR;
using SJZ.Timelines.Domain.TimelineAggregate;
using SJZ.Timelines.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Commands
{
    public class UpsertRecordCommandHandler : IRequestHandler<UpsertRecordCommand, string>
    {
        private readonly ITimelineRepository _timelineRepository;

        public UpsertRecordCommandHandler(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }

        public async Task<string> Handle(UpsertRecordCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.RecordId))
            {
                var record = await _timelineRepository.CreateRecordAsync(
                    new Record(request.TimelineId, request.Content, request.Date, request.UserId));
                return record.Id;
            }
            else
            {
                var record = await _timelineRepository.GetRecordAsync(request.RecordId);
                record.UpdateContent(request.Content, request.Date, request.UserId);
                await _timelineRepository.UpdateRecordAsync(record);
                return record.Id;
            }
        }
    }
}
