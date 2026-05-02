using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace WebUI.Models.ViewModels
{
    public class SystemContactUsViewModel
    {
        public SystemContactUsViewModel()
        {
        }

        public string WarehouseAddress { get; set; }
        public string ContactPhone { get; set; }
        public string BusinessHours {  get; set; }
        public string CustomerServiceWeChat { get; set; }

        public List<Tuple<string, string>> ToKeyValuePairs()
        {
            return new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(nameof(WarehouseAddress), WarehouseAddress),

                new Tuple<string, string>(nameof(ContactPhone), ContactPhone),
                new Tuple<string, string>(nameof(BusinessHours), BusinessHours),
                new Tuple<string, string>(nameof(CustomerServiceWeChat), CustomerServiceWeChat)
            };
        }

        public static List<string> GetKeys()
        {
            return new List<string>()
            {
                nameof(WarehouseAddress),
                nameof(ContactPhone),
                nameof(BusinessHours),
                nameof(CustomerServiceWeChat)
            };
        }

        public static SystemContactUsViewModel FromTuples(List<Tuple<string, string>> keyValues)
        {
            return new SystemContactUsViewModel()
            {
                WarehouseAddress = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(WarehouseAddress))?.Item2,
                ContactPhone = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(ContactPhone))?.Item2,
                BusinessHours = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(BusinessHours))?.Item2,
                CustomerServiceWeChat = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(CustomerServiceWeChat))?.Item2,
            };
        }
    }
}
