using Domain.Enums;
using Domain.Entities;

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
    }
}
