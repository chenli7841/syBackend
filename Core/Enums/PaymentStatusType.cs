using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public static class PaymentStatusType
    {
        public const string PAID = "paid";
        public static string GetDescription(string stage)
        {
            return stage switch
            {
                PAID => "已付款",
                _ => "",
            };
        }
    }
}
