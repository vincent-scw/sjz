using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SJZ.Timelines.Domain.TimelineAggregate;
using SJZ.Timelines.Repository;
using SJZ.TimelineService.Commands;
using SJZ.TimelineService.Models;

namespace SJZ.TimelineService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimelinesController : ControllerBase
    {
        private readonly ITimelineRepository _timelineRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<TimelinesController> _logger;

        public TimelinesController(
            ITimelineRepository timelineRepository,
            IMediator mediator,
            IMapper mapper,
            ILogger<TimelinesController> logger)
        {
            _timelineRepository = timelineRepository;
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetAsync(string id)
        {
            var timeline = await _timelineRepository.GetAsync(id);
            if (timeline == null)
                return NotFound();
            return Ok(_mapper.Map<TimelineDto>(timeline));
        }

        [HttpGet]
        public async Task<IActionResult> GetListAsync([FromQuery] string userId)
        {
            var list = await _timelineRepository.GetListAsync(userId);
            return Ok(list.Select(x => _mapper.Map<TimelineDto>(x)));
        }

        [HttpPost]
        public async Task<IActionResult> UpsertAsnyc([FromBody] UpsertTimelineCommand command)
        {
            var result = await _mediator.Send(command);
            if (string.IsNullOrEmpty(result))
                return BadRequest(); 
            else
                return Ok(new { TimelineId = result });
        }

        [HttpPost("{timelineId}/Records")]
        public async Task<IActionResult> UpsertAsync(string timelineId, [FromBody] UpsertRecordCommand command)
        {
            if (string.IsNullOrEmpty(command.TimelineId))
                command.TimelineId = timelineId;

            var result = await _mediator.Send(command);
            if (string.IsNullOrEmpty(result))
                return BadRequest();
            else
                return Ok(new { TimelineId = timelineId, RecordId = result });
        }

        [HttpDelete("{timelineId}")]
        public async Task<IActionResult> DeleteAsync(string timelineId)
        {
            var command = new DeleteCommand { TimelineId = timelineId };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{timelineId}/Records/{recordId}")]
        public async Task<IActionResult> DeleteAsync(string timelineId, string recordId)
        {
            var command = new DeleteCommand { TimelineId = timelineId, RecordId = recordId };
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
