namespace InventorySystem.helpers
{
    public class PaginatedList<T> where T : class
    {
        public IReadOnlyCollection<T> Items { get; }
        public int PageNumber { get; }
        public static int PageSize = 5;
        public int TotalPages { get; }

        public PaginatedList(IReadOnlyCollection<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;

        }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public static PaginatedList<T> CreateAsync(List<T> source, int pageNumber)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList();

            return new PaginatedList<T>(items, count, pageNumber, PageSize);
        }
    }
}
