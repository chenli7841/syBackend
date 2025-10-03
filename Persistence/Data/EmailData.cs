using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class EmailData
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int SenderUserId { get; set; }
        public int RecipientUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateSent { get; set; }
        public DateTime? DateSentSms { get; set; }
        public int? BatchId { get; set; }

        public virtual TransportOrder Order { get; set; }
        public virtual User Recipient { get; set; }
        public virtual User Sender { get; set; }
        public virtual Batch Batch { get; set; }
    }
}
