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
                SEALING => "封箱", // 对应的运单类型是 "已发货" Dispatched，对应的运单状态是 "打包封装等待发出" PendingDispatch
                "delivering" => "正在派送",
                "done" => "已完成",
                _ => "",
            };
        }
    }
}
