using System.ComponentModel;

namespace Domain.Enums
{
    public enum OrderState
    {
        None = -1,
        [Description("未匹配")]
        Draft = 0,
        [Description("未入库")]
        Created = 1,
        [Description("已入库")]
        InWarehouse = 5,
        [Description("待发货")]
        PendingDispatch = 10,
        [Description("已发货")]
        Dispatched = 20,
        [Description("已到货")]
        Arrived = 26,
        [Description("待提货")]
        PendingPickUp = 30,
        [Description("正在派送")]
        InDelivery = 35,
        [Description("已派送")]
        Delivered = 40,
        [Description("待确认")]
        PendingConfirmation = 60,
        [Description("已确认")]
        Confirmed = 65,
        [Description("待退运")]
        PendingReturn = 70,
        [Description("已退运")]
        Returned = 75,
        [Description("违禁品")]
        Illegal = 80,
        [Description("历史")]
        Done = 100,
    }
}
