using EventManager.Api.Data;
using EventManager.Api.DTOs;
using EventManager.Api.Models;
using EventManager.Api.Models.Pagination;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Services
{
    public class EventService : IEventService
    {
        private readonly ApplicationDbContext _db;

        public EventService(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<PageResponse<EventDTO>> GetPagedAsync(PageRequest request)
        {
            var property = typeof(Event).GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, request.SortBy, StringComparison.OrdinalIgnoreCase));

            var sortProperty = property?.Name ?? "Id";

            var query = _db.Events.AsQueryable();
            query = request.SortDescending
                ? query.OrderByDescending(e => EF.Property<object>(e, sortProperty))
                : query.OrderBy(e => EF.Property<object>(e, sortProperty));

            var total = await query.CountAsync();
            var items = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(e => new EventDTO
                {
                    Id = e.Id,
                    Name = e.Name,
                    Location = e.Location,
                    Country = e.Country,
                    Capacity = e.Capacity
                })
                .ToListAsync();

            return new PageResponse<EventDTO>
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                ItemCount = total,
                Items = items
            };
        }

        public async Task<EventDTO?> GetByIdAsync(int id)
        {
            var e = await _db.Events.FindAsync(id);
            if (e == null)
                return null;

            return new EventDTO
            {
                Id = e.Id,
                Name = e.Name,
                Location = e.Location,
                Country = e.Country,
                Capacity = e.Capacity
            };
        }

        public async Task<EventDTO> CreateAsync(EventDTO dto)
        {
            var entity = new Event
            {
                Name = dto.Name,
                Location = dto.Location,
                Country = dto.Country,
                Capacity = dto.Capacity
            };

            _db.Events.Add(entity);
            await _db.SaveChangesAsync();

            dto.Id = entity.Id;
            return dto;
        }

        public async Task<bool> UpdateAsync(EventDTO dto)
        {
            var found = await _db.Events.FindAsync(dto.Id);
            if (found == null)
                return false;

            found.Name = dto.Name;
            found.Location = dto.Location;
            found.Country = dto.Country;
            found.Capacity = dto.Capacity;

            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var found = await _db.Events.FindAsync(id);
            if (found == null)
                return false;

            _db.Events.Remove(found);
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
