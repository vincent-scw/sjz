using MediatR;
using SJZ.Timelines.Domain;
using SJZ.Timelines.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SJZ.TimelineService.Commands
{
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly ITimelineRepository _timelineRepository;
        public DeleteCommandHandler(ITimelineRepository timelineRepository)
        {
            _timelineRepository = timelineRepository;
        }

        public async Task<Unit> Handle(DeleteCommand request, CancellationToken cancellationToken)
        {
            var timeline = await _timelineRepository.GetAsync(request.TimelineId);
            if (timeline == null)
                throw new DomainException("TimelineNotFound");
            if (timeline.CreatedBy != request.UserId)
                throw new DomainException("AccessDenid");

            if (string.IsNullOrEmpty(request.RecordId))
            {
                await _timelineRepository.DeleteAsync(request.TimelineId);
            }
            else
            {
                await _timelineRepository.DeleteRecordAsync(request.RecordId);
            }

            return Unit.Value;
        }
    }
}
