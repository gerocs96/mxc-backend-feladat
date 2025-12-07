namespace EventManager.Api.Models.Pagination
{
    /// <summary>
    /// Represents pagination parameters sent by the client
    /// </summary>
    public class PageRequest
    {
        public const int MAXPAGESIZE = 100;

        public int PageNumber { get; set; } = 1;
        private int _pageSize { get; set; } = 10;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value;
        }

        // Sorting
        public string SortBy { get; set; } = "Id";
        public bool SortDescending { get; set; } = false;
    }
}
