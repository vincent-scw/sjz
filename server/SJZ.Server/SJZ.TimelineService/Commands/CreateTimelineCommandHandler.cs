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
    public class CreateTimelineCommandHandler : IRequestHandler<CreateTimelineCommand, string>
    {
        private readonly ITimelineRepository _timelineRepository;
        public CreateTimelineCommandHandler(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }

        public async Task<string> Handle(CreateTimelineCommand request, CancellationToken cancellationToken)
        {
            var timeline = await _timelineRepository.CreateAsync(new Timeline(
                request.Title,
                request.StartTime,
                request.IsCompleted,
                Enumeration.FromValue<PeriodLevel>(request.PeriodLevel),
                request.UserId,
                request.Username
                ));

            return timeline.Id;
        }
    }
}
