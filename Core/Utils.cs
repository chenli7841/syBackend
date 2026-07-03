using Domain.Enums;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain
{
    public static class Utils
    {
        public static string GetScanStatusClass(OrderEntity order)
        {
            switch (order.ScanStatusType)
            {
                case OrderScanStatusType.SecondScan:
                return "secondDispatched";
                case OrderScanStatusType.ThirdScan:
                return "thirdDispatched";
                default:
                return "";
            }
        }

        public static string GetPackageBatchName(int routeType, string companyCode, string routeCode, string locationName, string recipientCode, string customName = null)
        {
            var nameParts = new List<string>();
            if (!string.IsNullOrWhiteSpace(customName))
            {
                nameParts.Add(customName);
            }
            if (routeType == (int)RouteType.Mixed) {
                nameParts.Add($"{companyCode}-{routeCode}-{locationName}-装箱打包");
            } else if (routeType == (int)RouteType.Direct) {
                if (string.IsNullOrWhiteSpace(recipientCode))
                {
                    nameParts.Add($"{companyCode}-{routeCode}-装箱打包");
                }
                else
                {
                    nameParts.Add($"{companyCode}-{routeCode}-{recipientCode}-装箱打包");
                }
            }
            return string.Join("-", nameParts);
        }
    }
}
