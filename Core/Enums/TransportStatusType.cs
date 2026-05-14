using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enums
{
    public static class TransportStatusType
    {
        public const string SEALING = "sealing";
        public static string GetDescription(string stage)
        {
            return stage switch
            {
                "transportfilling" => "运输装填",
                SEALING => "封箱",
                "delivering" => "正在派送",
                "done" => "已完成",
                _ => "",
            };
        }
    }
}
