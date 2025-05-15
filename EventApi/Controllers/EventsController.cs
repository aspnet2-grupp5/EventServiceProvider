using EventApi.Data.Contexts;
using EventApi.Data.Entities;
using EventApi.Documentation;
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
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }
}

        [Authorize]
        [HttpGet]
        [SwaggerOperation(Summary = "Get all Events")]
        [SwaggerResponse(200, "List of events", typeof(IEnumerable<EventEntity>))]
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

        [HttpPost]
        [Consumes("multipart/form-data")]
        [SwaggerOperation(Summary = "Create a new Event")]
        [SwaggerRequestExample(typeof(AddEventFormData), typeof(AddEventFormExample))]
        [SwaggerResponse(201, "Event created", typeof(EventEntity))]
        [SwaggerResponse(400, "Invalid model state")]
        public async Task<EventEntity?> CreateEventAsync(AddEventFormData formData)
        {
            var newEvent = new EventEntity
            {
                EventId = Guid.NewGuid().ToString(),
                EventTitle = formData.EventTitle,
                Description = formData.Description,
                Date = formData.Date,
                Price = formData.Price,
                Quantity = formData.Quantity,
                SoldQuantity = 0,
                CategoryId = formData.CategoryId,
                LocationId = formData.LocationId,
                StatusId = formData.StatusId
            };

        }


        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update an Event")]
        [SwaggerResponse(204, "Event updated")]
        [SwaggerResponse(400, "Invalid model state")]
        public async Task<IActionResult> UpdateEvent(string id, [FromForm] EditEventformData formData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventService.UpdateEventAsync(id, formData);
            if (!result)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get event by ID")]
        [SwaggerResponse(200, "Event found", typeof(EventEntity))]
        [SwaggerResponse(404, "Event not found")]
        public async Task<IActionResult> GetEventById(string id)
        {
            var ev = await _eventService.GetEventByIdAsync(id);
            if (ev == null)
                return NotFound();

            return Ok(ev);
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Delete an Event")]
        [SwaggerResponse(204, "Event deleted")]
        [SwaggerResponse(404, "Event not found")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
}
