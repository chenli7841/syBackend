using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class Testuser
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string OrderStartNumber { get; set; }
        public string PickUpLocationId { get; set; }
        public string BelongsToId { get; set; }
        public string CanadaPhoneNumber { get; set; }
        public string Level { get; set; }
        public string Balance { get; set; }
        public string ClearingPortCost { get; set; }
        public string WeChat { get; set; }
        public string IsUpdated { get; set; }
        public string PostalCode { get; set; }
        public string ChinaPhoneNumber { get; set; }
        public string AddOnCost { get; set; }
        public string Credit { get; set; }
        public string StorageCost { get; set; }
        public string Avatar { get; set; }
        public string QrPath { get; set; }
        public string NickName { get; set; }
    }
}
