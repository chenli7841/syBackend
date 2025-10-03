using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class IntegrationIdCard
    {
        public int Id { get; set; }
        public string IdCardFrontUrl { get; set; }
        public string IdCardBackUrl { get; set; }
        public string IntegrationId { get; set; }
        public string Name { get; set; }
        public string IdCardNumber { get; set; }
        public string Address { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
    }
}
