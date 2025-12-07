using EventManager.Api.DTOs;
using EventManager.Api.Models.Pagination;
using EventManager.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService service)
        {
            _eventService = service;
        }

        [HttpPost("paged")]
        public async Task<ActionResult<List<EventDTO>>> GetPaged([FromBody] PageRequest request)
        {
            return Ok(await _eventService.GetPagedAsync(request));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetById(int id)
        {
            var result = await _eventService.GetByIdAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<EventDTO>> Create([FromBody] EventDTO dto)
        {
            var entity = await _eventService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = entity.Id }, entity);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> Update([FromBody] EventDTO dto)
        {
            var success = await _eventService.UpdateAsync(dto);

            if (!success)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            var success = await _eventService.DeleteAsync(id);

            if (!success)
                return NotFound();

            return NoContent();
        }
    }
}
