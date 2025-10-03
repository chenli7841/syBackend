namespace WebUI.Models.ApiRequest
{
    public class PreviewOrderExportRequest
    {
        public string[] Filters { get; set; }
        public int PageSize { get; set; } = 300;
    }
}
