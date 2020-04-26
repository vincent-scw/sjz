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
    public class MergeTimelineCommandHandler : IRequestHandler<MergeTimelineCommand, string>
    {
        private readonly ITimelineRepository _timelineRepository;
        public MergeTimelineCommandHandler(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }

        public async Task<string> Handle(MergeTimelineCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                var timeline = await _timelineRepository.CreateAsync(new Timeline(
                    request.Title,
                    request.Description,
                    request.StartTime,
                    request.IsCompleted,
                    Enumeration.FromValue<PeriodLevel>(request.PeriodLevel),
                    request.UserId,
                    request.Username
                    ));
                return timeline.Id;
            }
            else
            {
                var timeline = await _timelineRepository.GetAsync(request.Id);
                timeline.UpdateContent(request.Title, request.Description, request.StartTime, request.IsCompleted, 
                    Enumeration.FromValue<PeriodLevel>(request.PeriodLevel), request.UserId);
                await _timelineRepository.UpdateAsync(timeline);
                return request.Id;
            }
        }
    }
}
