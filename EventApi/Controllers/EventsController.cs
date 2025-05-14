using EventApi.Data.Contexts;
using EventApi.Documentation;
using EventApi.Entities;
using EventApi.Models;
using EventApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace EventApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class EventsController(IEventService eventService, EventsDbContext eventsDbContext) : ControllerBase
    {
        private readonly EventsDbContext _context = eventsDbContext;
        private readonly IEventService _eventService = eventService;

        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Get all Events")]
        [SwaggerResponse(200, "List of events", typeof(IEnumerable<EventEntity>))]
        [SwaggerResponse(404, "No events found")]
        public async Task<IActionResult> GetEvents() 
        {
            var events = await _context.Events.ToListAsync();
            if (events == null || !events.Any())
            {
                return NotFound();
            }
            return Ok(events);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a new Event")]
        [SwaggerRequestExample(typeof(AddEventFormData), typeof(AddEventFormExample))]
        [SwaggerResponse(201, "Event created", typeof(EventEntity))]
        [SwaggerResponse(400, "Invalid model state")]
        public async Task<IActionResult> CreateEvent([FromForm] AddEventFormData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(formData);

            var result = await _eventService.CreateEventAsync(formData); // your own service
            if (result == null)
                return BadRequest("Failed to create event");

            return CreatedAtAction(nameof(GetEvents), new { id = result.EventId }, result);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an Event")]
        [SwaggerResponse(204, "Event updated")]
        [SwaggerResponse(400, "Invalid model state")]
        public async Task<IActionResult> UpdateEvent(string id, [FormBody])
        {
            if (id != ev.EventId) 
                return BadRequest();

            _context.Entry(ev).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an Event")]
        [SwaggerResponse(204, "Event deleted")]
        [SwaggerResponse(404, "Event not found")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) 
                return NotFound();

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
