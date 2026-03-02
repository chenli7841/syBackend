using System.Collections.Generic;
using Domain.Enums;

namespace WebUI.Models
{
    public static class Constants
    {
        public static Dictionary<OrderStatusType, string> OrderStatusMap = new Dictionary<OrderStatusType, string>()
        {
            { OrderStatusType.OrderCreated, "运单已创建" },
            { OrderStatusType.CustomerChangeOrderNumber, "客户更改运单号" },
            { OrderStatusType.RequestCancel, "申请退运" },
            { OrderStatusType.EnterWarehouseAndScan, "入库扫描" },
            { OrderStatusType.PreCreateOrder, "预创建运单"},
            { OrderStatusType.CancelPackage, "包裹退运"},
            { OrderStatusType.PendingCustomerConfirm, "等待用户确认"},
            { OrderStatusType.PendingCustomerDispatch, "等待用户发货"},
            { OrderStatusType.PendingDispatch, "打包封装等待发出" },
            { OrderStatusType.CustomerConfirm, "客户确认" },
            { OrderStatusType.PendingConfirmationMoney, "等待确认运费" },
            { OrderStatusType.Dispatched, "已发货" },
            { OrderStatusType.PackagingScan, "装箱打包扫描" },
            { OrderStatusType.AddToPallet, "加入托盘" },
            { OrderStatusType.OnboardingScan, "装车扫描" },
            { OrderStatusType.PendingDeparture, "到达港口等待起航" },
            { OrderStatusType.InTransit, "运输中" },
            { OrderStatusType.ArrivedAtDestinationHarbour, "抵达目的港" },
            { OrderStatusType.ArrivedAtWarehouse, "抵达仓库" },
            { OrderStatusType.PendingPickup, "等待取货中" },
            { OrderStatusType.ArrivalScan, "到货扫描" },
            { OrderStatusType.ConfirmScan, "确认扫描" },
            { OrderStatusType.PendingDelivery, "等待派送" },
            { OrderStatusType.CustomerAlreadyPickedup, "客户已取货" },
            { OrderStatusType.AlreadyDelivered, "已派送" },
            { OrderStatusType.Completed, "已完成"},
            { OrderStatusType.ForbiddenItem, "违禁品"},

            // legacy statuses
            { OrderStatusType.DeliveredToChinaWareHouse_LateConfirmation, "请联系国内快递公司/国内仓库负责人"},
            { OrderStatusType.DeliveredToChinaWareHouse_Pending, "等待核对包裹状态" },
            { OrderStatusType.DeliveredToChinaWareHouse_Error, "录单晚 请联系仓库负责人确认仓库是否收货" },
            { OrderStatusType.ReceivedByEmployee, "货物已被{0}接收" },
            { OrderStatusType.ReceivedByChinaWareHouse, "已打包封装 等待发出" },
            { OrderStatusType.EnterWarehouse, "货物已入库"},
            { OrderStatusType.EnterPendingDeliver, "包裹已封装准备发出"},
            { OrderStatusType.PendingCollectMoney, "包裹需要付款"},
            { OrderStatusType.PackageCreated, "包裹建立等待称重"},
            { OrderStatusType.PackageInItlTransit, "包裹已进入邮政运输阶段(请在系统内查看单号)"},
            { OrderStatusType.MissingId, "收件人信息缺失/错误 货物进入待发区" },
            { OrderStatusType.FlightDelay, "航班延误" },
            { OrderStatusType.InvalidDomesticNumber, "单号信息有误/运单状态还未更新 请更新单号避免仓库无法收货" },
            { OrderStatusType.DuplicateName, "重名件待发货" },
            { OrderStatusType.MovedOut, "移出 进入待发区" },
            { OrderStatusType.MovedOutOfPendingDeliver, "包裹移出待发区"},
            { OrderStatusType.NeedVerify, "包裹已入库(请核对包裹数量)"},
            { OrderStatusType.ReturnedOrder, "包裹已退回"},
            { OrderStatusType.InPackaging, "包裹已打包" },
            { OrderStatusType.WaitForPackaging, "货物已接收 等待打包封装"},
            { OrderStatusType.SendToDistrict, "包裹已发往各取货点"},
            { OrderStatusType.Packaged, "包裹已封装" },
            { OrderStatusType.PackageSent, "货物已起航(请联系所在群群主充值)"},
            { OrderStatusType.PackageInTrt, "包裹到达多伦多"},
            { OrderStatusType.Paid, "客户已付款"},
            { OrderStatusType.OutOfWarehouse, "货物已确认" },
            { OrderStatusType.OutOfWarehouseTwice, "货物已二次确认" },
            { OrderStatusType.OutOfWarehouseThird, "货物已三次确认" },
            { OrderStatusType.UserRequestSend, "要求送货" },
            { OrderStatusType.InDelivery, "包裹已发出"},
            { OrderStatusType.InChinaTransit, "正在派送"},
            { OrderStatusType.DeliveredInChina, "已签收" },
            { OrderStatusType.PickedUpByClient, "客户已取货" },
            { OrderStatusType.ConfirmedInUserBatch, "已确认" },
        };

        public static string GmailAddress = "notification.eplus@gmail.com";
        public static string GmailPwd = "dybqcagazakncdqb";
    }
}
