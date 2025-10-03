using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Domain.Models
{
    [DataContract, Serializable]
    public class ChinaOrder
    {
        private static ChinaOrder _instance;

        public static ChinaOrder NullChinaOrder => _instance ??= new ChinaOrder();
        
        [DataMember(Name = "state")]
        public int? State { get; set; }

        [DataMember(Name = "data")]
        public IList<ChinaOrderData> Data { get; set; }

        public ChinaOrder()
        {
            Data = new List<ChinaOrderData>();
        }
    }
}