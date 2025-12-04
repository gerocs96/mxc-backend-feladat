namespace EventManager.Api.Models.Pagination
{
    /// <summary>
    /// Represents pagination parameters sent by the client
    /// </summary>
    public class PageRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public string? SortBy { get; set; }
        public bool SortDescending { get; set; } = false;
    }
}
