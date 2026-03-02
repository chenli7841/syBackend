using System.ComponentModel;

namespace Domain.Enums
{
    public enum OrderStatusType
    {
        [Description("运单创建")]
        OrderCreated = 1,
        [Description("客户更改运单号")]
        CustomerChangeOrderNumber = 2,
        [Description("申请退运")]
        RequestCancel = 3,
        [Description("入库扫描")]
        EnterWarehouseAndScan = 4,
        [Description("预创建运单")]
        PreCreateOrder = 5,
        [Description("包裹退运")]
        CancelPackage = 6,
        [Description("等待用户确认")]
        PendingCustomerConfirm = 7,
        [Description("等待用户发货")]
        PendingCustomerDispatch = 8,
        [Description("打包封装等待发出")]
        PendingDispatch = 9,
        [Description("等待确认运费")]
        PendingConfirmationMoney = 10,
        [Description("客户确认")]
        CustomerConfirm = 11,
        [Description("已发货")]
        Dispatched = 12,
        [Description("装箱打包扫描")]
        PackagingScan = 13,
        [Description("加入托盘")]
        AddToPallet = 14,
        [Description("装车扫描")]
        OnboardingScan = 15,
        [Description("到达港口等待起航")]
        PendingDeparture = 16,
        [Description("运输中")]
        InTransit = 17,
        [Description("抵达目的港")]
        ArrivedAtDestinationHarbour = 18,
        [Description("抵达仓库")]
        ArrivedAtWarehouse = 19,
        [Description("等待取货中")]
        PendingPickup = 20,
        [Description("到货扫描")]
        ArrivalScan = 21,
        [Description("确认扫描")]
        ConfirmScan = 22,
        [Description("等待派送")]
        PendingDelivery = 23,
        [Description("客户已取货")]
        CustomerAlreadyPickedup = 24,
        [Description("已派送")]
        AlreadyDelivered = 25,
        [Description("已完成")]
        Completed = 99,
        [Description("违禁品")]
        ForbiddenItem = 100,
        [Description("未签收")]
        NotSigned = 26,
        [Description("录单晚")]
        CreateOrderLate = 27,
        [Description("无派送信息")]
        NoDeliveryInfo = 28,

        // legacy statuses
        DeliveredToChinaWareHouse_LateConfirmation = -13,
        DeliveredToChinaWareHouse_Pending = -14,
        DeliveredToChinaWareHouse_Error = -15,
        ReceivedByEmployee = -16,
        SendToNextStop = -17,
        ReceivedByChinaWareHouse = -20,
        EnterWarehouse = -21,
        EnterPendingDeliver = -22,
        PendingCollectMoney = -23,
        PackageCreated = -24,
        PackageInItlTransit = -25,
        MissingId = 30,
        FlightDelay = 31,
        InvalidDomesticNumber = 32,
        DuplicateName = 40,
        MovedOut = 41,
        MovedOutOfPendingDeliver = 42,
        NeedVerify = 43,
        ReturnedOrder = 44,
        InPackaging = 60,
        WaitForPackaging = 62,
        SendToDistrict = 63,
        Packaged = 64,
        PackageSent = 65,
        PackageInTrt = 66,
        [Description("已付款")]
        Paid = 67,
        OutOfWarehouse = 68,
        OutOfWarehouseTwice = 69,
        BeginDomesticPart = 71,
        OutOfWarehouseThird = 72,
        WaitingForClearance = 80,
        ArrivedShipWarehouse = 81,
        BeginIntPart = 91,
        InDelivery = 92,
        UserRequestSend = 95,
        ArrivedAtCan = 101,
        ArrivedAtTrt = 102,
        InChinaTransit = 700,
        DeliveredInChina = 1000,
        PickedUpByClient = 1100,
        ConfirmedInUserBatch = 2000,
    }
}
