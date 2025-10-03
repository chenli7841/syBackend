using System;
namespace Domain.Entities
{
    public class SMSLogEntity
    {
        public int Id { get; set; }
        public int? BatchId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
    }
}