using EventApi.Factories;
using EventApi.Models;
using EventApi.Repositories;

public interface IEventService
{
    Task<IEnumerable<Event>> GetAllEventsAsync();
    Task<Event?> GetByIdAsync(string id);
    Task<IEnumerable<Event>> GetByCategoryIdAsync(string categoryId);
    Task<IEnumerable<Event>> GetByLocationIdAsync(string locationId);
    Task<IEnumerable<Event>> GetEventsByStatusNameAsync(string statusName);
    Task <bool> AddAsync(AddEventFormData formData);
    Task<bool> UpdateAsync(EditEventformData formData);
    Task<bool> DeleteByIdAsync(string id);
}
public class EventService(IEventRepository eventRepository) : IEventService
{
    private readonly IEventRepository _eventRepository = eventRepository;

    public async Task<IEnumerable<Event>> GetAllEventsAsync()
    {
        var entities = await _eventRepository.GetAllEventsAsync();
        var events = entities.Select(EventFactory.ToModel).ToList();
        return events;
    }

    public async Task<Event?> GetByIdAsync(string id)
    {
        var entity = await _eventRepository.GetByIdAsync(id);
        return EventFactory.ToModel(entity);
    }

    public async Task<IEnumerable<Event>> GetByCategoryIdAsync(string categoryId)
    {
        var entities = await _eventRepository.GetByCategoryIdAsync(categoryId);
        return entities.Select(EventFactory.ToModel);
    }

    public async Task<IEnumerable<Event>> GetByLocationIdAsync(string locationId)
    {
        var entities = await _eventRepository.GetByLocationIdAsync(locationId);
        return entities.Select(EventFactory.ToModel);
    }

    public async Task<IEnumerable<Event>> GetEventsByStatusNameAsync(string statusName)
    {
        var entities = await _eventRepository.GetByStatusNameAsync(statusName);
        return entities.Select(EventFactory.ToModel);
    }

    public async Task<bool> AddAsync(AddEventFormData formData)
    {
        if (formData == null)
            return false;

        var eventEntity = EventFactory.ToEntity(formData);
        await _eventRepository.AddAsync(eventEntity); 
        return true; 
    }

    public async Task<bool> UpdateAsync(EditEventformData formData)
    {
        if (formData == null)
            return false;

        var entity = EventFactory.ToEntity(formData);
        await _eventRepository.UpdateAsync(entity);
        return true; 
    }

    public async Task <bool> DeleteByIdAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
            return false;

        await _eventRepository.DeleteByIdAsync(id);
        return true;
    }
 
}
