using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class SmsLogEntity
    {
        public string Content { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string SenderOrderStartNumber { get; set; }
        public string ErrorSummary { get; set; }
        public int Attempts { get; set; }
    }
}