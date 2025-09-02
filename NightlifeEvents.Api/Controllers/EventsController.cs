using Microsoft.AspNetCore.Mvc;
using NightlifeEvents.Models;
using NightlifeEvents.Services;

namespace NightlifeEvents.Controllers
{
    [ApiController]
    [Route("api/")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost("createEvent")]
        public ActionResult<Event> CreateEvent(Event newEvent)
        {
            try
            {
                var created = _eventService.CreateEvent(newEvent);
                return CreatedAtAction(nameof(GetEventById), new { id = created.Id }, created);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events")]
        public ActionResult<IEnumerable<Event>> GetAllEvents() =>
            Ok(_eventService.GetAllEvents());

        [HttpGet("filter")]
        public ActionResult<IEnumerable<Event>> FilterEvents([FromQuery] string? city, [FromQuery] DateTime? date) =>
            Ok(_eventService.FilterEvents(city, date));

        [HttpGet("{id}")]
        public ActionResult<Event> GetEventById(int id)
        {
            var ev = _eventService.GetEventById(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }
    }
}
