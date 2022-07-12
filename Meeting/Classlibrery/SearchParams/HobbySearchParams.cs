using DataAccess.Services.Pagination;

namespace DataAccess.SearchParams
{
    public class HobbySearchParams:PaginationParams
    {
        public string? KeyFeatures { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int HobbyId { get; set; }

    }
}
