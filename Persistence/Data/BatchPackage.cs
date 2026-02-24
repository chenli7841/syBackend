namespace Persistence.Data
{
    public partial class BatchPackage
    {
        public BatchPackage()
        {
        }

        public int Id { get; set; }
        public int BatchId { get; set; }
        public string CustomName { get; set; }
        public string TransportStatus { get; set; }
        public string PaymentStatus { get; set; }
        public string FinishStatus { get; set; }
        public virtual Batch Batch { get; set; }
    }
}
