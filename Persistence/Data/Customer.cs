using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Customer
    {
        public Customer()
        {
            TransportOrderRecipients = new HashSet<TransportOrder>();
            TransportOrderSenders = new HashSet<TransportOrder>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string IdCardNumber { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string IdCardFrontUrl { get; set; }
        public string IdCardBackUrl { get; set; }
        public string IntegrationId { get; set; }
        public int? IdCardId { get; set; }
        public int? BelongsToUserId { get; set; }
        public bool PhotosExported { get; set; }

        public virtual User BelongsToUser { get; set; }
        public virtual ICollection<TransportOrder> TransportOrderRecipients { get; set; }
        public virtual ICollection<TransportOrder> TransportOrderSenders { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
