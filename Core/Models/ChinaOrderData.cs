using System;
using System.Runtime.Serialization;

namespace Domain.Models
{
    [DataContract, Serializable]
    public class ChinaOrderData
    {
        [DataMember(Name = "time")]
        public DateTime Time { get; set; }

        [DataMember(Name = "context")]
        public string Context { get; set; }
    }
}