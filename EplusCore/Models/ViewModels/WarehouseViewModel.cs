namespace WebUI.Models.ViewModels
{
    public class WarehouseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Contact { get; set; }
        public string PhotoUrl { get; set; }
        public string PhotoData { get; set; }
        public int? DisplaySequence { get; set; }
    }
}
