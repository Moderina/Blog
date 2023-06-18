namespace Blog.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public List<Post> Posts { get; private set; }

        public PaginatedList(List<Post> posts, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            //this.AddRange(items);
            Posts = posts;
        }

        public bool HasPreviousPage => PageIndex > 1;

        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(
            List<Post> source, int pageIndex, int pageSize)
        {
            var count = source.Count;
            var posts = source.Skip(
                (pageIndex - 1) * pageSize)
                .Take(pageSize).ToList();
            return new PaginatedList<T>(posts, count, pageIndex, pageSize);
        }
    }
}
