namespace EventManager.Api.Models.Pagination
{
    /// <summary>
    /// Represents paginated data returned to the client
    /// </summary>
    public class PageResponse<T>
    {
        public IEnumerable<T> Items { get; set; } = new List<T>();
        public int ItemCount { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)ItemCount / PageSize);
    }
}
