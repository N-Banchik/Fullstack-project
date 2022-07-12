using Microsoft.EntityFrameworkCore;

namespace DataAccess.Services.Pagination
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public PaginatedList(IEnumerable<T> items, int count, int pagenumber, int pagesize)
        {
            TotalCount = count;
            PageSize = pagesize;
            CurrentPage = pagenumber;
            TotalPages = (int)Math.Ceiling(count / (float)pagesize);
            this.AddRange(items);
        }
        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pagenumber, int pagesize)
        {
            int count = await source.CountAsync();
            var items = await source.Skip((pagenumber - 1) * pagesize).Take(pagesize).ToListAsync();
            return new PaginatedList<T>(items, count, pagenumber, pagesize);
        }

    }


}
