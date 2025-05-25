using EventApi.Factories;
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
public class EventService(IEventRepository eventRepository) : EventProto.EventProtoBase, IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public override async Task<GetAllEventsReply> GetAllEvents(Empty request, ServerCallContext context)
    {
        var entities = await _eventRepository.GetAllAsync(
            orderByDescending: false,
            sortBy: x => x.Date,
            filterBy: null,
            i => i.Category,
            i => i.Status,
            i => i.Location);

        var events = entities.Select(EventFactory.ToModel).ToList();
        var reply = new GetAllEventsReply();
        reply.Events.AddRange(events);
        return reply;
    }

    public override async Task<GetEventByIdReply> GetEventById(GetEventByIdRequest request, ServerCallContext context)
    {
        var entity = await _eventRepository.GetAsync(x => x.EventId == request.EventId,
            i => i.Category,
            i => i.Status,
            i => i.Location);
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
        var entities = await _eventRepository.GetAllAsync(
            orderByDescending: false,
            sortBy: x => x.Date,
            filterBy: x => x.Category.CategoryId == request.CategoryId,
            i => i.Category,
            i => i.Status,
            i => i.Location);

        var events = entities.Select(EventFactory.ToModel).ToList();
        var reply = new GetAllEventsReply();
        reply.Events.AddRange(events);
        return reply;
    }

    public override async Task<GetAllEventsReply> GetEventsByLocation(LocationIdRequest request, ServerCallContext context)
    {
        var entities = await _eventRepository.GetAllAsync(
            orderByDescending: false,
            sortBy: x => x.Date,
            filterBy: x => x.Location.LocationId == request.LocationId,
            i => i.Category,
            i => i.Status,
            i => i.Location);

        var events = entities.Select(EventFactory.ToModel).ToList();
        var reply = new GetAllEventsReply();
        reply.Events.AddRange(events);
        return reply;
    }

    public override async Task<GetAllEventsReply> GetEventsByStatus(StatusIdRequest request, ServerCallContext context)
    {
        var entities = await _eventRepository.GetAllAsync(
            orderByDescending: false,
            sortBy: x => x.Date,
            filterBy: x => x.Status.StatusId == request.StatusId,
            i => i.Category,
            i => i.Status,
            i => i.Location);
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
            return new EventReply { StatusCode = 500, Message = "Not created" };

        return new EventReply { StatusCode = 200, Message = "Created" };
    }

    public override async Task<EventReply> UpdateEvent(Event request, ServerCallContext context)
    {

        var entity = EventFactory.ToEntity(request);
        var result = await _eventRepository.UpdateAsync(entity);
        if (!result)
            return new EventReply
            {
                StatusCode = 500,
                Message = "not OK"

            };

        return new EventReply { StatusCode = 200, Message = "ok" };
    }

    public override async Task<EventReply> DeleteEvent(GetEventByIdRequest request, ServerCallContext context)
    {
        var result = await _eventRepository.DeleteAsync
            (x => x.EventId == request.EventId);

        if (!result)

            return new EventReply { StatusCode = 500, Message = "not OK" };

        return new EventReply { StatusCode = 200, Message = "ok" };
    }

}
