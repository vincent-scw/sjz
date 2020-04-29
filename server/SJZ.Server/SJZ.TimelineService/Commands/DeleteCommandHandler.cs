using MediatR;
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
