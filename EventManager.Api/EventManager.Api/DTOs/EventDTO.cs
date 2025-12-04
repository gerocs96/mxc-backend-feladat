namespace EventManager.Api.DTOs
{
    public class EventDTO
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string? Country { get; set; }

        public int? Capacity { get; set; }
    }
}
