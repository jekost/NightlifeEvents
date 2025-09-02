using NightlifeEvents.Models;

namespace NightlifeEvents.Services
{
    public class EventService : IEventService
    {
        private readonly List<Event> _events = new();
        private int _nextId = 1;

        public EventService()
        {
            CreateEvent(new Event
            {
                Title = "Berlin Night Party",
                City = "Berlin",
                Description = "Techno night in Berlin.",
                Date = DateTime.UtcNow.AddDays(2),
                TicketPrice = 25
            });

            CreateEvent(new Event
            {
                Title = "Watergate Revival",
                City = "Berlin",
                Description = "Birthday party for Alexander Justappen.",
                Date = DateTime.UtcNow.AddDays(43),
                TicketPrice = 20
            });

            CreateEvent(new Event
            {
                Title = "Paris Jazz Festival",
                City = "Paris",
                Description = "Smooth jazz evening.",
                Date = DateTime.UtcNow.AddDays(5),
                TicketPrice = 40
            });

            CreateEvent(new Event
            {
                Title = "Möku sünnipäev",
                City = "Tartu",
                Description = "Sparta party.",
                Date = DateTime.UtcNow.AddDays(12),
                TicketPrice = 10
            });

            CreateEvent(new Event
            {
                Title = "Oktoberfest",
                City = "München",
                Description = "Bier, Schnitzel und Lederhosen.",
                Date = new DateTime(2025, 9, 20, 13, 0, 0),
                TicketPrice = 40
            });
        }

        public Event CreateEvent(Event newEvent)
        {
            if (newEvent == null ||
                string.IsNullOrWhiteSpace(newEvent.Title) ||
                string.IsNullOrWhiteSpace(newEvent.City) ||
                string.IsNullOrWhiteSpace(newEvent.Description) ||
                newEvent.Date == default ||
                newEvent.TicketPrice < 0)
            {
                throw new ArgumentException(
                    "An event must have a title, city, description, date, and a positive ticket price.");
            }

            if (newEvent.Date < DateTime.UtcNow)
                throw new ArgumentException("Event date cannot be in the past.");


            // Check if an event with the same ID already exists
            var existingEvent = _events.FirstOrDefault(e => e.Id == newEvent.Id);
            if (existingEvent != null)
            {
                // Overwrite the existing event
                existingEvent.Title = newEvent.Title;
                existingEvent.City = newEvent.City;
                existingEvent.Description = newEvent.Description;
                existingEvent.Date = newEvent.Date;
                existingEvent.TicketPrice = newEvent.TicketPrice;
            }
            else
            {
                // Assign a new ID and add to the list
                newEvent.Id = _nextId++;
                _events.Add(newEvent);
            }

            return newEvent;
        }

        public IEnumerable<Event> GetAllEvents() =>
            _events.Where(e => e.Date >= DateTime.UtcNow);

        public IEnumerable<Event> FilterEvents(string? city, DateTime? date)
        {
            var query = _events.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(city))
                query = query.Where(e => e.City.Equals(city, StringComparison.OrdinalIgnoreCase));

            if (date.HasValue)
                query = query.Where(e => e.Date.Date == date.Value.Date);

            return query;
        }

        public Event? GetEventById(int id) =>
            _events.FirstOrDefault(e => e.Id == id);
    }
}
