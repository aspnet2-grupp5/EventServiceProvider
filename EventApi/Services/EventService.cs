using EventApi.Factories;
using EventApi.Handlers;
using EventApi.Protos;
using EventApi.Repositories;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

public interface IEventService
{
    Task<EventReply> CreateEvent(Event request, ServerCallContext context);
    Task<EventReply> DeleteEvent(GetEventByIdRequest request, ServerCallContext context);
    Task<GetAllEventsReply> GetAllEvents(Empty request, ServerCallContext context);
    Task<GetEventByIdReply> GetEventById(GetEventByIdRequest request, ServerCallContext context);
    Task<GetAllEventsReply> GetEventsByCategory(CategoryIdRequest request, ServerCallContext context);
    Task<GetAllEventsReply> GetEventsByLocation(LocationIdRequest request, ServerCallContext context);
    Task<GetAllEventsReply> GetEventsByStatus(StatusIdRequest request, ServerCallContext context);
    Task<EventReply> UpdateEvent(Event request, ServerCallContext context);
}
public class EventService(IEventRepository eventRepository,
ICacheHandler<IEnumerable<Event>> cacheHandler) : EventProto.EventProtoBase, IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;
    private readonly ICacheHandler<IEnumerable<Event>> _cacheHandler = cacheHandler;
    private const string _cacheKey = "events_cache_key";
    private const string _cacheCategoryKey = "categories_cache_key";
    private const string _cacheStatusKey = "statuses_cache_key";
    private const string _cacheLocationKey = "locations_cache_key";
    public override async Task<GetAllEventsReply> GetAllEvents(Empty request, ServerCallContext context)
    {
        var cachedEvents = _cacheHandler.GetFromCache(_cacheKey);
        if (cachedEvents != null)
        {
            var cachedreply = new GetAllEventsReply();
            cachedreply.Events.AddRange(cachedEvents);
            return cachedreply;
        }

        var entities = await _eventRepository.GetAllAsync
            (
                orderByDescending: false,
                sortBy: x => x.Date,
                filterBy: null,
                i => i.Category,
                i => i.Status,
                i => i.Location
            );

        var events = entities.Select(EventFactory.ToModel).ToList();

        _cacheHandler.SetCache(_cacheKey, events);

        var reply = new GetAllEventsReply();
        reply.Events.AddRange(events);
        return reply;
    }
    public override async Task<GetEventByIdReply> GetEventById(GetEventByIdRequest request, ServerCallContext context)
    {
        var cachedEvents = _cacheHandler.GetFromCache(_cacheKey);
        if (cachedEvents != null)
        {
            var cachedEvent = cachedEvents.FirstOrDefault(x => x.EventId == request.EventId);
            if (cachedEvent != null)
            {
                return new GetEventByIdReply
                {
                    StatusCode = 200,
                    Event = cachedEvent
                };
            }
        }

        var entity = await _eventRepository.GetAsync
            (
                x => x.EventId == request.EventId,
                i => i.Category,
                i => i.Status,
                i => i.Location
            );

        if (entity == null)
        {
            return new GetEventByIdReply
            {
                StatusCode = 404,
            };
        }
        return new GetEventByIdReply
        {
            StatusCode = 200,
            Event = EventFactory.ToModel(entity)
        };

    }
    public override async Task<GetAllEventsReply> GetEventsByCategory(CategoryIdRequest request, ServerCallContext context)
    {
        var cachedEvents = _cacheHandler.GetFromCache(_cacheCategoryKey);
        if (cachedEvents != null)
        {
            var cachedEventsByCategory = cachedEvents.Where(x => x.Category.CategoryId == request.CategoryId).ToList();
            if (cachedEventsByCategory.Any())
            {
                var cachedreply = new GetAllEventsReply();
                cachedreply.Events.AddRange(cachedEventsByCategory);
                return cachedreply;
            }
        }

        var entities = await _eventRepository.GetAllAsync
            (
                orderByDescending: false,
                sortBy: x => x.Date,
                filterBy: x => x.Category.CategoryId == request.CategoryId,
                i => i.Category,
                i => i.Status,
                i => i.Location
            );

        var events = entities.Select(EventFactory.ToModel).ToList();
        var reply = new GetAllEventsReply();
        reply.Events.AddRange(events);
        return reply;
    }
    public override async Task<GetAllEventsReply> GetEventsByLocation(LocationIdRequest request, ServerCallContext context)
    {
        var cachedEvents = _cacheHandler.GetFromCache(_cacheLocationKey);
        if (cachedEvents != null)
        {
            var cachedEventsByLocation = cachedEvents.Where(x => x.Location.LocationId == request.LocationId).ToList();
            if (cachedEventsByLocation.Any())
            {
                var cachedreply = new GetAllEventsReply();
                cachedreply.Events.AddRange(cachedEventsByLocation);
                return cachedreply;
            }
        }
        var entities = await _eventRepository.GetAllAsync
            (
                orderByDescending: false,
                sortBy: x => x.Date,
                filterBy: x => x.Location.LocationId == request.LocationId,
                i => i.Category,
                i => i.Status,
                i => i.Location
            );

        var events = entities.Select(EventFactory.ToModel).ToList();
        var reply = new GetAllEventsReply();
        reply.Events.AddRange(events);
        return reply;
    }
    public override async Task<GetAllEventsReply> GetEventsByStatus(StatusIdRequest request, ServerCallContext context)
    {
        var cachedEvents = _cacheHandler.GetFromCache(_cacheStatusKey);
        if (cachedEvents != null)
        {
            var cachedEventsByStatus = cachedEvents.Where(x => x.Status.StatusId == request.StatusId).ToList();
            if (cachedEventsByStatus.Any())
            {
                var cachedreply = new GetAllEventsReply();
                cachedreply.Events.AddRange(cachedEventsByStatus);
                return cachedreply;
            }
        }
        var entities = await _eventRepository.GetAllAsync
            (
                orderByDescending: false,
                sortBy: x => x.Date,
                filterBy: x => x.Status.StatusId == request.StatusId,
                i => i.Category,
                i => i.Status,
                i => i.Location
            );

        var events = entities.Select(EventFactory.ToModel).ToList();
        var reply = new GetAllEventsReply();
        reply.Events.AddRange(events);
        return reply;
    }
    public override async Task<EventReply> CreateEvent(Event request, ServerCallContext context)
    {
        var entity = EventFactory.ToEntity(request);
        var result = await _eventRepository.AddAsync(entity);

        if (!result)
            return new EventReply { StatusCode = 500, Message = "Failed to create the event." };

        await UpdateCacheAsync();
        return new EventReply { StatusCode = 200, Message = "Event created successfully." };
    }

    public override async Task<EventReply> UpdateEvent(Event request, ServerCallContext context)
    {
        var entity = EventFactory.ToEntity(request);
        var result = await _eventRepository.UpdateAsync(entity);

        if (!result)
            return new EventReply { StatusCode = 500, Message = "Failed to update the event." };

        await UpdateCacheAsync();
        return new EventReply { StatusCode = 200, Message = "Event updated successfully." };
    }
    public override async Task<EventReply> DeleteEvent(GetEventByIdRequest request, ServerCallContext context)
    {
        var result = await _eventRepository.DeleteAsync(x => x.EventId == request.EventId);

        if (!result)
            return new EventReply { StatusCode = 500, Message = "Failed to delete the event." };

        await UpdateCacheAsync();

        return new EventReply { StatusCode = 200, Message = "Event deleted successfully." };
    }
    private async Task<IEnumerable<Event>> UpdateCacheAsync()
    {
        var events = await _eventRepository.GetAllAsync(
            orderByDescending: false,
            sortBy: x => x.Date,
            filterBy: null,
            i => i.Category,
            i => i.Status,
            i => i.Location
        );

        var models = events.Select(EventFactory.ToModel).ToList();
        _cacheHandler.SetCache(_cacheKey, models);

        return models;
    }

}


