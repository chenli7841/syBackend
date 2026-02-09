using System;
using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Entities
{
    public class BatchEntity
    {
        public BatchEntity()
        {
            Boxes = new List<BatchBoxEntity>();
            OtherOrders = new List<string>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public BatchGroupType GroupType { get; set; }
        public DateTime DateCreated { get; set; }
        public IList<BatchBoxEntity> Boxes { get; set; }
        public string IntNumber { get; set; }
        public string IntCarrier { get; set; }
        public decimal Cost { get; set; }
        public decimal AddOnCost { get; set; }
        public decimal Duty { get; set; }
        public decimal StorageCost { get; set; }
        public decimal Discount { get; set; }
        public decimal InsuranceFee { get; set; }
        public decimal HeBaoCost { get; set; }
        public decimal WeightKg { get; set; }
        public decimal TotalExpense { get; set; }
        public BatchStageType Stage { get; set; }
        public decimal? TargetWeightKg { get; set; }
        public int? RecipientAddressId { get; set; }
        public UserEntity Creator { get; set; }
        public IList<string> OtherOrders { get; set; }
        // TODO: only keep Id
        public int? RecipientId { get; set; }
        public UserEntity Recipient { get; set; }
        public int? AgentId { get; set; }
        public int? PickUpLocationId { get; set; }
        public PickUpLocationEntity PickUpLocation { get; set; }
        public UserEntity Agent { get; set; }
        public int? ProgressId { get; set; }
        public DeliverProgressEntity Progress { get; set; }
        public int? MasterBatchId { get; set; }
        public BatchEntity MasterBatch { get; set; }
        public int? WarehouseId { get; set; }
        public RouteEntity Route { get; set; }
        public int? RouteId { get; set; }
        public decimal Commission { get; set; }
        public DateTime? DateEntered { get; set; }
        public string FlightInfo { get; set; }
        public string CargoNumber { get; set; }
        public DateTime? ArrivalTime { get; set; }
        public string Note { get; set; }
        public IEnumerable<WarehouseEntity> Warehouses { get; set; }
        public IEnumerable<CompanyEntity> Companies { get; set; }
        public int? CompanyId { get; set; }

        public OrderState GetOrderState(BatchGroupType? groupTypeToUse = null)
        {
            var groupType = groupTypeToUse ?? GroupType;

            if (Route != null)
            {
                if (Route.Type == RouteType.Direct)
                {
                    switch (groupType)
                    {
                        case BatchGroupType.LoadDelivery:
                            return OrderState.Dispatched;
                    }
                }
                else if (Route.Type == RouteType.Group)
                {
                    switch (groupType)
                    {
                        case BatchGroupType.ExitGarageScan:
                            return OrderState.Dispatched;
                    }
                }
                else if (Route.Type == RouteType.Mixed)
                {
                    switch (groupType)
                    {
                        case BatchGroupType.Package:
                            return OrderState.InWarehouse;

                        case BatchGroupType.ExitGarageScan:
                            return OrderState.Dispatched;

                        case BatchGroupType.LoadDelivery:
                        case BatchGroupType.WarehouseReceive:
                            return Stage == BatchStageType.Clearing ? OrderState.Arrived : OrderState.None;
                    }
                }
            }

            switch (groupType)
            {
                case BatchGroupType.DailyScan:
                    return OrderState.InWarehouse;

                case BatchGroupType.PendingDispatch:
                    return OrderState.PendingDispatch;

                case BatchGroupType.PendingConfirmation:
                    return OrderState.PendingConfirmation;

                case BatchGroupType.PendingPickUp:
                    return OrderState.PendingPickUp;

                case BatchGroupType.DailyReturn:
                    return OrderState.PendingReturn;

                case BatchGroupType.Contraband:
                    return OrderState.Dispatched;

                //case BatchGroupType.Done:
                //    return OrderState.Done;

                default:
                    return OrderState.None;
            }
        }

        public BatchGroupType GetNextGroupType(RouteEntity orderRoute = null)
        {
            var route = orderRoute ?? Route;

            if (route == null)
            {
                return BatchGroupType.None;
            }

            if (route.Type == RouteType.Direct)
            {
                switch (GroupType)
                {
                    case BatchGroupType.DailyScan:
                        return BatchGroupType.PendingDispatch;

                    case BatchGroupType.PendingDispatch:
                        return BatchGroupType.Package;

                    case BatchGroupType.Package:
                        return BatchGroupType.PendingConfirmation;

                    case BatchGroupType.PendingConfirmation:
                        return BatchGroupType.ExitGarageScan;

                    case BatchGroupType.ExitGarageScan:
                        return BatchGroupType.LoadDelivery;

                    case BatchGroupType.LoadDelivery:
                        return BatchGroupType.Done;
                }
            }
            else if (route.Type == RouteType.Group)
            {
                switch (GroupType)
                {
                    case BatchGroupType.DailyScan:
                        return BatchGroupType.PendingDispatch;

                    case BatchGroupType.PendingDispatch:
                        return BatchGroupType.Package;

                    case BatchGroupType.Package:
                        return BatchGroupType.PendingConfirmation;

                    case BatchGroupType.PendingConfirmation:
                        return BatchGroupType.ExitGarageScan;

                    case BatchGroupType.ExitGarageScan:
                        return BatchGroupType.LoadDelivery;

                    case BatchGroupType.LoadDelivery:
                        return BatchGroupType.WarehouseReceive;

                    case BatchGroupType.WarehouseReceive:
                        return BatchGroupType.PendingPickUp;
                }
            }
            else if (route.Type == RouteType.Mixed)
            {
                switch (GroupType)
                {
                    case BatchGroupType.PickUpLocation:
                    case BatchGroupType.AgentCommission:
                    case BatchGroupType.WarehouseCost:
                        return BatchGroupType.Done;
                }
            }

            return BatchGroupType.None;
        }

        public List<BatchActionType> GetActionTypes()
        {
            if (Route == null)
            {
                return new List<BatchActionType> {
                    BatchActionType.None,
                    BatchActionType.SplitByLocations,
                    BatchActionType.SplitByRecipients
                };
            }

            if (Route.Type == RouteType.Direct)
            {
                switch (GroupType)
                {
                    case BatchGroupType.PendingDispatch:
                    case BatchGroupType.Package:
                    case BatchGroupType.PendingConfirmation:
                        return new List<BatchActionType> { BatchActionType.Next };

                    case BatchGroupType.ExitGarageScan:
                        return new List<BatchActionType> { 
                            //BatchActionType.BalancePay 关闭直邮线出库扫面里的扣款功能，但下一步按键还要
                            BatchActionType.Next
                        };

                    case BatchGroupType.LoadDelivery:
                        return new List<BatchActionType> { BatchActionType.Commission, BatchActionType.UpdateLoadDeliveryProperties };
                }
            }
            else if (Route.Type == RouteType.Group)
            {
                switch (GroupType)
                {
                    case BatchGroupType.LoadDelivery:
                        return new List<BatchActionType> { BatchActionType.Next, BatchActionType.UpdateLoadDeliveryProperties };
                    case BatchGroupType.PendingDispatch:
                    case BatchGroupType.Package:
                    case BatchGroupType.PendingConfirmation:
                    case BatchGroupType.WarehouseReceive:
                        return new List<BatchActionType> { BatchActionType.Next };

                    case BatchGroupType.ExitGarageScan:
                        return new List<BatchActionType> { 
                            BatchActionType.BalancePay,
                            // 团购线路 - 关闭下一步，而余额扣款要log时间和操作人 BatchActionType.Next
                        };
                }
            }
            else if (Route.Type == RouteType.Mixed)
            {
                switch (GroupType)
                {
                    case BatchGroupType.LoadDelivery:
                        return new List<BatchActionType> { BatchActionType.SplitByLocations, BatchActionType.SplitByAgents, BatchActionType.SplitByNonAgent, BatchActionType.SplitByNonLocation, BatchActionType.UpdateLoadDeliveryProperties };

                    case BatchGroupType.WarehouseReceive:
                        return new List<BatchActionType> { BatchActionType.SplitByRecipients };

                    case BatchGroupType.PickUpLocation:
                        return new List<BatchActionType> { BatchActionType.BalancePay, BatchActionType.SplitByRecipients };
                    case BatchGroupType.WarehouseCost:
                        return new List<BatchActionType> { BatchActionType.BalancePay };

                    case BatchGroupType.AgentCommission:
                        return new List<BatchActionType> { BatchActionType.Commission };
                }
            }

            if (GroupType == BatchGroupType.Bill)
            {
                return new List<BatchActionType> { BatchActionType.Commission };
            }

            return new List<BatchActionType> { BatchActionType.None };
        }
    }

    public class BatchBoxEntity
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public string Name { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? ActualWeightKg { get; set; }
        public int BatchId { get; set; }
        public IEnumerable<OrderEntity> Orders { get; set; }
    }

    public class RouteBatchCount
    {
        public int RouteId { get; set; }
        public int BatchCount { get; set; }
    }

    public class BatchOtherOrderEntity
    {
        public string OtherOrder { get; set; }
        public int BatchId { get; set; }
        public string BatchName { get; set; }
        public UserEntity Creator { get; set; }
        public DateTime? DateCreated { get; set; }
    }

    public class OrderScanStatusEntity
    {
        public int OrderId { get; set; }
        public OrderScanStatusType Status { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class OrderCostSummaryEntity
    {
        public int BatchId { get; set; }
        public decimal TotalItemCost { get; set; }
        public decimal TotalOversizeCost { get; set; }
        public decimal TotalWarehouseCost { get; set; }
        public decimal TotalFumigationCost { get; set; }
        public decimal TotalPortMisCost { get; set; }
        // 保险费用，而非保险价值
        public decimal TotalInsurance { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalDuty { get; set; }

        public decimal TotalShippingCost { get; set; }
        public decimal TotalStorageCost { get; set; }
        public decimal TotalDistrictAdditionalCost { get; set; }
    }

    public class CouponBatchEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
        public int CreatedById { get; set; }
        public bool? Anonymous { get; set; }
        public int NumberOfCoupons { get; set; }
        public List<CouponEntity> Coupons { get; set; }
        public UserEntity CreatedBy { get; set; }
        public string PhotoUrl { get; set; }
        public string EmailContent { get; set; }
        public string SmsContent { get; set; }
    }

    public class PalletBatchEntity
    {
        public PalletBatchEntity()
        {
            Boxes = new List<BatchBoxEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public BatchGroupType GroupType { get; set; }
        public IList<BatchBoxEntity> Boxes { get; set; }
        public int WarehouseId { get; set; }
        public string Note { get; set; }
        public string Destination { get; set; }
        public IEnumerable<WarehouseEntity> Warehouses { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? WeightKg { get; set; }
        public string ShipFlightNumber { get; set; }
        public string State { get; set; }
    }

    public class LoadDeliveryBatchEntity
    {
        public LoadDeliveryBatchEntity()
        {
            Boxes = new List<BatchBoxEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public BatchGroupType GroupType { get; set; }
        public IList<BatchBoxEntity> Boxes { get; set; }
        public int WarehouseId { get; set; }
        public string Note { get; set; }
        public string Destination { get; set; }
        public IEnumerable<WarehouseEntity> Warehouses { get; set; }
        public string ShipFlightNumber { get; set; }
        public string State { get; set; }
    }

    public class WarehouseReceiveBatchEntity
    {
        public WarehouseReceiveBatchEntity()
        {
            Boxes = new List<BatchBoxEntity>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public BatchGroupType GroupType { get; set; }
        public IList<BatchBoxEntity> Boxes { get; set; }
        public int WarehouseId { get; set; }
        public string Note { get; set; }
        public string Destination { get; set; }
        public IEnumerable<WarehouseEntity> Warehouses { get; set; }
        public string ShipFlightNumber { get; set; }
        public string State { get; set; }
    }
}
