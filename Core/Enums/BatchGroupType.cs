using System.ComponentModel;

namespace Domain.Enums
{
    public enum BatchGroupType
    {
        None = -1,
        [Description("每日到货")]
        DailyScan = 1,
        [Description("待发货")]
        PendingDispatch = 15,
        [Description("装箱打包")]
        Package = 20,
        [Description("托盘")]
        Pallet = 21,
        [Description("待确认")]
        PendingConfirmation = 25,
        [Description("出库扫描")]
        ExitGarageScan = 30,
        [Description("装车发货")]
        LoadDelivery = 40,
        [Description("仓库收货")]
        WarehouseReceive = 50,
        [Description("待提货")]
        PendingPickUp = 60,
        [Description("待派送")]
        PendingDelivery = 65,
        [Description("已派送")]
        Delivered = 66,
        [Description("每日退运")]
        DailyReturn = 70,
        [Description("违禁品")]
        Contraband = 71,
        [Description("账单")]
        Bill = 75,
        [Description("代理返利")]
        AgentCommission = 77,
        [Description("取货点")]
        PickUpLocation = 78,
        [Description("仓库费用")]
        WarehouseCost = 85,
        [Description("历史")]
        Done = 100
    }
}
