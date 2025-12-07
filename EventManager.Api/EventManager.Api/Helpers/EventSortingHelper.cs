using EventManager.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Api.Helpers
{
    public static class EventSortingHelper
    {
        /// <summary>
        /// Orders the query by given property, defaults to Id
        /// </summary>
        public static IQueryable<Event> ApplySorting(IQueryable<Event> query, string? sortBy, bool sortDescending)
        {
            var property = typeof(Event).GetProperties()
                .FirstOrDefault(p => string.Equals(p.Name, sortBy, StringComparison.OrdinalIgnoreCase));

            var sortProperty = property?.Name ?? nameof(Event.Id);

            query = sortDescending
                ? query.OrderByDescending(e => EF.Property<object>(e, sortProperty))
                : query.OrderBy(e => EF.Property<object>(e, sortProperty));

            return query;
        }
    }
}
