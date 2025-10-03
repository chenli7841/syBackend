using System.ComponentModel;

namespace Domain.Enums
{
    public enum SmsContentType
    {
        [Description("分拣完毕")]
        Sorted = 0,
        [Description("运单入库")]
        InWarehouse = 1,
        [Description("收到/扣掉加币")]
        Transfer = 2,
        [Description("已称重计费")]
        Weighted = 3,
        [Description("包裹已发出")]
        Dispatched = 4,
        [Description("待确认")]
        PendingConfirmation = 5
    }
}
