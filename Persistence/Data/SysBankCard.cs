using System;
using System.Collections.Generic;

#nullable disable

namespace Persistence.Data
{
    public partial class SysBankCard
    {
        public long Id { get; set; }
        public string BankCardBelongs { get; set; }
        public bool? BankCardType { get; set; }
        public string BankCardPerson { get; set; }
        public string IdCard { get; set; }
        public string BankCardNumber { get; set; }
        public long? AppUserId { get; set; }
        public long? ShopId { get; set; }
        public DateTime? CreateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
