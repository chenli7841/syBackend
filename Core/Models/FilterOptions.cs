namespace Domain.Models
{
    public class FilterOptions
    {
        public FilterOptions()
        {
            PageSize = 20;
            Skip = 0;
        }

        public int PageSize { get; set; }
        public int Skip { get; set; }
    }
}