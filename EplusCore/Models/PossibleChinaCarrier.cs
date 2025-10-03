using System;
using System.Runtime.Serialization;

namespace EplusCore.Models
{
    [DataContract, Serializable]
    public class PossibleChinaCarrier
    {
        [DataMember(Name = "comCode")]
        public string Code { get; set; }
    }
}