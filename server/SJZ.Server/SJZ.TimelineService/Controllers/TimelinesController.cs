using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SJZ.Timelines.Repository;

namespace SJZ.TimelineService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimelinesController : ControllerBase
    {
        private readonly ITimelineRepository _timelineRepository;
        private readonly ILogger<TimelinesController> _logger;

        public TimelinesController(
            ITimelineRepository timelineRepository,
            ILogger<TimelinesController> logger)
        {
            _timelineRepository = timelineRepository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<ActionResult> Get(string id)
        {
            var timeline = await _timelineRepository.GetAsync(id);
            if (timeline == null)
                return NotFound();
            return Ok(timeline);
        }
    }
}
