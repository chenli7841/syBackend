using System.Collections.Generic;

namespace Domain.Models
{
    public class PagedResult<T>
    {
        public PagedResult()
        {
            Items = new List<T>();
        }

        public int Total { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
