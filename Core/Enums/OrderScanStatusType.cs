using System.ComponentModel;

namespace Domain.Enums
{
    public enum OrderScanStatusType
    {
        [Description("未知")]
        Unknown = 0,
        [Description("二次扫描")]
        SecondScan = 2,
        [Description("三次扫描")]
        ThirdScan = 3
    }
}
