using System.ComponentModel.DataAnnotations;

namespace EventManager.Api.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Location { get; set; }

        public string? Country { get; set; }

        [Range(1, int.MaxValue)]
        public int? Capacity { get; set; }
    }
}
