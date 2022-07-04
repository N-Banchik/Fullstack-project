using DataAccess.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SearchParams
{
    public class EventSearchParams : PaginationParams
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public string OrderBy { get; set; } = "Date";
        public string? EventName { get; set; }
        public int HobbyId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
    }
}
