namespace WebUI.Models.ViewModels
{
    public class BatchBoxPrintModel
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string BatchName { get; set; }
        public int OrderCount { get; set; }
        public decimal Weight { get; set; }
    }
}
