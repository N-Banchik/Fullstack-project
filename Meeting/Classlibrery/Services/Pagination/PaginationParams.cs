using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services.Pagination
{
    public class PaginationParams
    {
        private const int MaxPageSize = 20;

        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get { return _pageSize; }
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}
