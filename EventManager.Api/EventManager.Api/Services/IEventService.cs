using EventManager.Api.DTOs;
using EventManager.Api.Models.Pagination;

namespace EventManager.Api.Services
{
    public interface IEventService
    {
        /// <summary>
        /// Returns a paginated list of events
        /// </summary>
        Task<PageResponse<EventDTO>> GetPagedAsync(PageRequest request);

        /// <summary>
        /// Returns a single event by id
        /// </summary>
        Task<EventDTO?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new event and returns the created dto
        /// </summary>
        Task<EventDTO> CreateAsync(EventDTO dto);

        /// <summary>
        /// Updates an existing event
        /// </summary>
        Task<bool> UpdateAsync(EventDTO dto);

        /// <summary>
        /// Deletes an event
        /// </summary>
        Task<bool> DeleteAsync(int id);
    }
}
