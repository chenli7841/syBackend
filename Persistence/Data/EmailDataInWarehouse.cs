using System;

#nullable disable

namespace Persistence.Data
{
    public partial class EmailDataInWarehouse
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SenderUserId { get; set; }
        public int RecipientUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateSentEmail { get; set; }
    }
}
