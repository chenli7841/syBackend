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
        public string OceanRuleImageURL { get; set; }
        public string FlightRuleImageURL { get; set; }
        public string CommonQuestionImageURL { get; set; }
        public string InsurancePolicyImageURL { get; set; }
        public string OrderCreateGuidelineImageURL { get; set; }

        public List<Tuple<string, string>> ToKeyValuePairs()
        {
            return new List<Tuple<string, string>>()
            {
                new Tuple<string, string>(nameof(WarehouseAddress), WarehouseAddress),
                new Tuple<string, string>(nameof(ContactPhone), ContactPhone),
                new Tuple<string, string>(nameof(BusinessHours), BusinessHours),
                new Tuple<string, string>(nameof(CustomerServiceWeChat), CustomerServiceWeChat),
                new Tuple<string, string>(nameof(OceanRuleImageURL), OceanRuleImageURL),
                new Tuple<string, string>(nameof(FlightRuleImageURL), FlightRuleImageURL),
                new Tuple<string, string>(nameof(CommonQuestionImageURL), CommonQuestionImageURL),
                new Tuple<string, string>(nameof(InsurancePolicyImageURL), InsurancePolicyImageURL),
                new Tuple<string, string>(nameof(OrderCreateGuidelineImageURL), OrderCreateGuidelineImageURL),
            };
        }

        public static List<string> GetKeys()
        {
            return new List<string>()
            {
                nameof(WarehouseAddress),
                nameof(ContactPhone),
                nameof(BusinessHours),
                nameof(CustomerServiceWeChat),
                nameof(OceanRuleImageURL),
                nameof(FlightRuleImageURL),
                nameof(CommonQuestionImageURL),
                nameof(InsurancePolicyImageURL),
                nameof(OrderCreateGuidelineImageURL),
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
                OceanRuleImageURL = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(OceanRuleImageURL))?.Item2,
                FlightRuleImageURL = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(FlightRuleImageURL))?.Item2,
                CommonQuestionImageURL = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(CommonQuestionImageURL))?.Item2,
                InsurancePolicyImageURL = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(InsurancePolicyImageURL))?.Item2,
                OrderCreateGuidelineImageURL = keyValues.FirstOrDefault(kv => kv.Item1 == nameof(OrderCreateGuidelineImageURL))?.Item2,
            };
        }
    }
}
