using Domain.Entities;
using Domain.Enums;

namespace WebUI.Models
{
    public static class OrderStatusEntityExtension
    {
        public static string GetDisplayText(this OrderStatusEntity status)
        {
            if (!Constants.OrderStatusMap.ContainsKey(status.Status))
            {
                return string.Empty;
            }

            return string.Format(Constants.OrderStatusMap[status.Status], status.Operator?.Name ?? "");
        }
    }

    public static class OrderScanStatusEntityExtension
    {
        public static string GetDisplayText(this OrderScanStatusEntity status)
        {
            switch (status.Status)
            {
                case OrderScanStatusType.SecondScan:
                return "二次确认扫描";
                case OrderScanStatusType.ThirdScan:
                return "三次确认扫描";
                default:
                return null;
            }
        }
    }
}
