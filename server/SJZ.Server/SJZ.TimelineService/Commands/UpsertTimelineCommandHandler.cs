using MediatR;
using SJZ.Common.Domain;
using SJZ.Timelines.Domain.TimelineAggregate;
using SJZ.Timelines.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Commands
{
    public class UpsertTimelineCommandHandler : IRequestHandler<UpsertTimelineCommand, string>
    {
        private readonly ITimelineRepository _timelineRepository;
        public UpsertTimelineCommandHandler(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }

        public async Task<string> Handle(UpsertTimelineCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.TimelineId))
            {
                var timeline = await _timelineRepository.CreateAsync(new Timeline(
                    request.Title,
                    request.Description,
                    request.IsCompleted,
                    Enumeration.FromValue<PeriodGroupLevel>(request.PeriodLevel),
                    request.UserId,
                    request.Username
                    ));
                return timeline.Id;
            }
            else
            {
                var timeline = await _timelineRepository.GetAsync(request.TimelineId);
                timeline.UpdateContent(request.Title, request.Description, request.IsCompleted, 
                    Enumeration.FromValue<PeriodGroupLevel>(request.PeriodLevel), request.UserId);
                await _timelineRepository.UpdateAsync(timeline);
                return request.TimelineId;
            }
        }
    }
}
