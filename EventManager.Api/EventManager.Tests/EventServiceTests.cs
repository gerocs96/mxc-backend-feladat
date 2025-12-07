using EventManager.Api.Data;
using EventManager.Api.DTOs;
using EventManager.Api.Models;
using EventManager.Api.Models.Pagination;
using EventManager.Api.Services;
using EventManager.Tests.Utilities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Tests
{
    public class EventServiceTests
    {
        [Fact]
        public async Task CreateAsync_ShouldCreateEvent()
        {
            var db = InMemoryDbContextFactory.CreateContext();
            var service = new EventService(db);

            var dto = new EventDTO
            {
                Name = "Test Event",
                Location = "Budapest",
                Country = "Hungary",
                Capacity = 100
            };

            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("Test Event", result.Name);
            Assert.Equal(1, db.Events.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnNull()
        {
            var db = InMemoryDbContextFactory.CreateContext();
            var service = new EventService(db);

            var result = await service.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteEvent()
        {
            var db = InMemoryDbContextFactory.CreateContext();
            var service = new EventService(db);

            db.Events.Add(new Event { Id = 1, Name = "Test Event", Location = "Test City" });
            db.SaveChanges();

            var success = await service.DeleteAsync(1);

            Assert.True(success);
            Assert.Empty(db.Events);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnCorrectPage()
        {
            var db = InMemoryDbContextFactory.CreateContext();
            var service = new EventService(db);

            for (int i = 1; i <= 20; i++)
            {
                db.Events.Add(new Event { Name = $"Test Event {i}", Location = $"Test City {i}" });
            }

            db.SaveChanges();

            var request = new PageRequest { PageNumber = 2, PageSize = 5 };

            var result = await service.GetPagedAsync(request);

            Assert.Equal(5, result.Items.Count());
            Assert.Equal(20, result.ItemCount);
        }

        [Fact]
        public void CreateAsync_ShouldFailValidationWhenCapacityIsNegative()
        {
            var ev = new Event
            {
                Name = "Test Event",
                Location = "Budapest",
                Capacity = -5
            };

            var context = new ValidationContext(ev);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(ev, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Event.Capacity)));
        }

        [Fact]
        public void CreateAsync_ShouldFailValidationWhenLocationTooLong()
        {
            var ev = new Event
            {
                Name = "Test Event",
                Location = new string('A', 101),
                Capacity = 50
            };

            var context = new ValidationContext(ev);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(ev, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains(nameof(Event.Location)));
        }
    }
}
