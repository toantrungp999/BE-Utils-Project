namespace Utils.Application.Dto
{
    public class PaginationDto<T>
    {
        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public IList<T> Items { get; set; }

        public bool HasPreviousPage { get; set; }

        public bool HasNextPage { get; set; }
    }
}
