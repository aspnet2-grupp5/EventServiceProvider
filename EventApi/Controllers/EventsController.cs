using EventApi.Documentation;
using EventApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace EventApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [Authorize(Roles ="Member,Admin")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get all Events")]
        [SwaggerResponse(200, "List of events", typeof(IEnumerable<Event>))]
        [SwaggerResponse(404, "No events found")]
        public async Task<IActionResult> GetEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            if (events == null || !events.Any())
            {
                return NotFound();
            }
            return Ok(events);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a new Event")]
        [SwaggerRequestExample(typeof(AddEventFormData), typeof(AddEventFormExample))]
        [SwaggerResponse(2001, "Event created", typeof(Event))]
        [SwaggerResponse(400, "Invalid model state")]
        public async Task<IActionResult?> CreateEventAsync([FromForm]AddEventFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventService.AddAsync(formData);
            return result
                ? Ok(result)
                : BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Update an Event")]
        [SwaggerRequestExample(typeof(EditEventformData), typeof(EditEventFormExampel))]
        [SwaggerResponse(200, "Event updated", typeof(Event))]
        [SwaggerResponse(400, "Invalid model state")]
        public async Task<IActionResult> UpdateEvent(string id, [FromForm] EditEventformData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventService.UpdateAsync(formData);
            if (!result)
                return NotFound();

            return Ok(result);
        }

        [Authorize(Roles = "Member,Admin")]
        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get event by ID")]
        [SwaggerResponse(200, "Event found", typeof(Event))]
        [SwaggerResponse(404, "Event not found")]
        public async Task<IActionResult> GetEventById(string id)
        {
            var ev = await _eventService.GetByIdAsync(id);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an Event")]
        [SwaggerResponse(204, "Event deleted")]
        [SwaggerResponse(404, "Event not found")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var result = await _eventService.DeleteByIdAsync(id);
            if (!result)
                return NotFound();
            return Ok(result);
        }
    }
}
