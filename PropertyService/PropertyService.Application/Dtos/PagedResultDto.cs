using System.Collections.Generic;

namespace PropertyService.Application.Dtos
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public long TotalItems { get; set; }
        public int TotalPages { get; set; }
    }
}