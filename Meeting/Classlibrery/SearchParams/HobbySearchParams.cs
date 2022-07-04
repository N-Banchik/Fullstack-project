using DataAccess.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
