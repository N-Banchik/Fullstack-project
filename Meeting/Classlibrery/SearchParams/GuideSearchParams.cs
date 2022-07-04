using DataAccess.Services.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SearchParams
{
    public class GuideSearchParams : PaginationParams
    {
        public int Id { get; set; }
    }
}
