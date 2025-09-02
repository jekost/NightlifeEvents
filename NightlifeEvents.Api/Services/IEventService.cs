using NightlifeEvents.Models;

namespace NightlifeEvents.Services
{
    public interface IEventService
    {
        Event CreateEvent(Event newEvent);
        IEnumerable<Event> GetAllEvents();
        IEnumerable<Event> FilterEvents(string? city, DateTime? date);
        Event? GetEventById(int id);
    }
}
