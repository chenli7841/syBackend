using System.Collections.Generic;
using System;

namespace Domain.Models
{
    public class SmsRequest
    {
        public string OrderStartNumber { get; set; }
        public string MobilePhoneNumber { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime? DateSentSms { get; set; }
        public DateTime? DateSentEmail { get; set; }
        public string EmailMessage { get; set; }
        public int Level { get; set; }
        public string FullName { get; set; }
        public string BelongsTo { get; set; }
        public IEnumerable<int> EmailDataIds { get; set; }
    }
}