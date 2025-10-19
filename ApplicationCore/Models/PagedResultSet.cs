using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Models
{
    public class PagedResultSet<T> where T : class
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public long Count { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;
        public IEnumerable<T> Data { get; set; }
        public PagedResultSet(IEnumerable<T> data, int pageIndex, int pageSize, long count)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}
