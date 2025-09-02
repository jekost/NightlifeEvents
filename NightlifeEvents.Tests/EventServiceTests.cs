using System;
using System.Linq;
using NightlifeEvents.Models;
using NightlifeEvents.Services;
using Xunit;

namespace NightlifeEvents.Tests
{
    public class EventServiceTests
    {
        private readonly EventService _service;

        public EventServiceTests()
        {
            _service = new EventService();
        }

        [Fact]
        public void CreateEvent_ShouldAddNewEvent()
        {
            var newEvent = new Event
            {
                Title = "Test Event",
                City = "TestCity",
                Description = "Test Description",
                Date = DateTime.UtcNow.AddDays(1),
                TicketPrice = 15
            };

            var created = _service.CreateEvent(newEvent);

            Assert.NotNull(created);
            Assert.True(created.Id > 0);
            Assert.Contains(_service.GetAllEvents(), e => e.Id == created.Id);
        }

        [Fact]
        public void CreateEvent_ShouldThrowIfInvalid()
        {
            var invalidEvent = new Event(); // missing all fields

            Assert.Throws<ArgumentException>(() => _service.CreateEvent(invalidEvent));
        }

        [Fact]
        public void GetAllEvents_ShouldReturnUpcomingEventsOnly()
        {
            var events = _service.GetAllEvents();
            Assert.All(events, e => Assert.True(e.Date >= DateTime.UtcNow));
        }

        [Fact]
        public void FilterEvents_ByCity_ShouldReturnMatchingEvents()
        {
            var city = "Berlin";
            var results = _service.FilterEvents(city, null);
            Assert.All(results, e => Assert.Equal(city, e.City, ignoreCase: true));
        }

        [Fact]
        public void FilterEvents_ByDate_ShouldReturnMatchingEvents()
        {
            var date = DateTime.UtcNow.AddDays(2).Date;
            var results = _service.FilterEvents(null, date);
            Assert.All(results, e => Assert.Equal(date, e.Date.Date));
        }

        [Fact]
        public void FilterEvents_ByCityAndDate_ShouldReturnMatchingEvents()
        {
            var city = "Berlin";
            var date = DateTime.UtcNow.AddDays(2).Date;
            var results = _service.FilterEvents(city, date);
            Assert.All(results, e =>
            {
                Assert.Equal(city, e.City, ignoreCase: true);
                Assert.Equal(date, e.Date.Date);
            });
        }

        [Fact]
        public void GetEventById_ShouldReturnCorrectEvent()
        {
            var firstEvent = _service.GetAllEvents().First();
            var fetched = _service.GetEventById(firstEvent.Id);
            Assert.NotNull(fetched);
            Assert.Equal(firstEvent.Id, fetched!.Id);
        }

        [Fact]
        public void CreateEvent_ShouldOverwriteIfIdExists()
        {
            // Arrange
            var existing = _service.GetAllEvents().First();
            var updatedEvent = new Event
            {
                Id = existing.Id,
                Title = "Updated Title",
                City = existing.City,
                Description = existing.Description,
                Date = existing.Date.AddDays(1),
                TicketPrice = existing.TicketPrice + 5
            };

            // Act
            var result = _service.CreateEvent(updatedEvent);

            // Assert
            var fetched = _service.GetEventById(existing.Id);
            Assert.Equal("Updated Title", fetched!.Title);
            Assert.Equal(existing.Id, fetched.Id);
        }
    }
}
