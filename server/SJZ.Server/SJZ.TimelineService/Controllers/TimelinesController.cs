using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public async Task<ActionResult> GetAsync(string id)
        {
            var timeline = await _timelineRepository.GetAsync(id);
            if (timeline == null)
                return NotFound();
            return Ok(_mapper.Map<TimelineDto>(timeline));
        }

        [HttpPut]
        public async Task<IActionResult> CreateAsnyc([FromBody]CreateTimelineCommand command)
        {
            var result = await _mediator.Send(command);
            if (string.IsNullOrEmpty(result))
                return BadRequest(); 
            else
                return Ok(new { TimelineId = result });
        }
    }
}
