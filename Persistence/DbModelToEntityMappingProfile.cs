using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain;
using Domain.Entities;
using Domain.Enums;
using Domain.Models.Extensions;
using Persistence.Data;

namespace Persistence
{
    public class DbModelToEntityMappingProfile : Profile
    {
        private string GetCargoNumber(TransportOrder src)
        {
            var batch = src.BatchBoxOrderMaps.Select(bbom => bbom.BatchBox).Select(box => box.Batch).FirstOrDefault(b => b != null && b.LoadDeliveryBatches != null && b.LoadDeliveryBatches.Count > 0);
            if (batch == null)
            {
                return "";
            }
            return batch.LoadDeliveryBatches.First().CargoNumber;
        }

        private string GetLoadDeliveryBatchName(TransportOrder src)
        {
            var batch = src.BatchBoxOrderMaps.Select(bbom => bbom.BatchBox).Select(box => box.Batch).FirstOrDefault(b => b != null && b.LoadDeliveryBatches != null && b.LoadDeliveryBatches.Count > 0);
            return batch?.Name ?? "";
        }

        public static List<TransportOrder> GetOrders(Batch batch)
        {
            var orders = new List<TransportOrder>();
            var orderIds = new HashSet<int>();
            foreach (var b in batch.BatchBoxes)
            {
                foreach(var bbom in b.BatchBoxOrderMaps)
                {
                    if (!orderIds.Contains(bbom.OrderId))
                    {
                        orders.Add(bbom.Order);
                        orderIds.Add(bbom.OrderId);
                    }

                }
            }
            foreach (var bbm in batch.BatchBoxMaps)
            {
                foreach(var bbom in bbm.BatchBox.BatchBoxOrderMaps)
                {
                    if (!orderIds.Contains(bbom.OrderId))
                    {
                        orders.Add(bbom.Order);
                        orderIds.Add(bbom.OrderId);
                    }
                }
            }
            return orders;
        }

        private static int GetTotalOrders(Batch batch)
        {
            return GetOrders(batch).Count;
        }

        private static decimal GetTotalChargedWeightKg(Batch batch)
        {
            decimal totalChargedWeightKg = 0;
            var orders = GetOrders(batch);
            foreach (var o in orders)
            {
                totalChargedWeightKg += o.WeightKg ?? 0;
            }
            return totalChargedWeightKg;
        }

        private static decimal GetTotalWeightKg(Batch batch)
        {
            decimal totalWeightKg = 0;
            var orders = GetOrders(batch);
            foreach (var o in orders)
            {
                foreach (var b in o.OrderBaggages)
                {
                    totalWeightKg += b.WeightKg;
                }
            }
            return totalWeightKg;
        }

        private static decimal GetBaseShippingCost(Batch batch)
        {
            decimal batchBaseShippingCost = 0;
            var orders = GetOrders(batch);
            foreach (var o in orders)
            {
                //ItemCost + (---Duty-- -) + OversizeCost + FumigationCost + WarehouseCost + PortMisCost + StorageCost + rate * weight + (---InsuranceCost-- -) - Discount
                var baseShippingCost = o.ItemCost + o.OversizeCost + o.FumigationCost + o.WarehouseCost + o.PortMisCost + o.StorageCost + o.WeightKg * o.PickUpLocation.DistrictAdditionalCost;
            }
            return batchBaseShippingCost;
        }

        private static decimal GetTotalDuty(Batch batch)
        {
            decimal totalDuty = 0;
            var orders = GetOrders(batch);
            foreach (var o in orders)
            {
                totalDuty += o.Duty;
            }
            return totalDuty;
        }

        private static decimal GetInsuranceFee(Batch batch)
        {
            decimal totalInsuranceFee = 0;
            var orders = GetOrders(batch);
            foreach (var o in orders)
            {
                totalInsuranceFee += o.InsuranceCost ?? 0;
            }
            return totalInsuranceFee;
        }

        private static decimal GetRecipientCredit(Batch batch)
        {
            User user = null;
            if (batch.Route.Type == (int)(RouteType.Mixed))
            {
                user = batch.PickUpLocation?.BelongsTo;
            }
            else if (batch.Route.Type == (int)(RouteType.Direct))
            {
                user = batch.RecipientUser;
            }
            return user == null ? 0 : user.Credit;
        }

        private static decimal GetRecipientBalance(Batch batch)
        {
            User user = null;
            if (batch.Route.Type == (int)(RouteType.Mixed))
            {
                user = batch.PickUpLocation?.BelongsTo;
            }
            else if (batch.Route.Type == (int)(RouteType.Direct))
            {
                user = batch.RecipientUser;
            }
            return user == null ? 0 : user.Balance;
        }

        private static decimal GetTotalVolume(Batch batch)
        {
            decimal totalVolume = 0;
            var orders = GetOrders(batch);
            foreach (var o in orders)
            {
                totalVolume += o.TotalVolume ?? 0;
            }
            return totalVolume;
        }

        private static string GetToDoItemBatchInfo(TodoItem todoItem)
        {
            if (todoItem.Batch == null)
            {
                return "";
            }
            return Domain.Utils.GetPackageBatchName(todoItem.Batch.Route.Type, todoItem.Batch.Company.Code, todoItem.Batch.Route.Code, todoItem.Batch.PickUpLocation?.Name, todoItem.Batch.RecipientUser.OrderStartNumber, todoItem.Batch.Name);
        }
        private static string GetToDoItemCustomerInfo(TodoItem todoItem)
        {
            if (todoItem.Batch == null)
            {
                return "";
            }
            if (todoItem.Batch.RecipientUser == null)
            {
                return "";
            }
            return todoItem.Batch.RecipientUser.OrderStartNumber;
        }

        public DbModelToEntityMappingProfile()
        {
            CreateMap<User, UserEntity>()
                .ForMember(dest => dest.SelectedPickUpLocationId, opt => opt.MapFrom(src => src.PickUpLocationId))
                .ForMember(dest => dest.SelectedPickUpLocation, opt => opt.MapFrom(src => src.PickUpLocationNavigation))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.OrderStartNumber))
                .ForMember(dest => dest.BelongsTo, opt => opt.MapFrom(src => src.BelongsToNavigation))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Customer.Name))
                .ForMember(dest => dest.Province, opt => opt.MapFrom(src => src.Customer.Province))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Customer.City))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Customer.Address))
                .ForMember(dest => dest.OrderStartNumber, opt => opt.MapFrom(src => src.OrderStartNumber));
            CreateMap<User, TodoItemAssigneeEntity>()
                .ForMember(dest => dest.OrderStartNumber, opt => opt.MapFrom(src => src.OrderStartNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Customer.Name));
            CreateMap<User, TodoItemCustomerEntity>()
                .ForMember(dest => dest.OrderStartNumber, opt => opt.MapFrom(src => src.OrderStartNumber))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Customer.Name));
            CreateMap<TransportOrder, TodoItemOrderEntity>();
            CreateMap<PickUpLocation, PickUpLocationEntity>()
                .ForMember(dest => dest.IsSpecial, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Owner, opt => opt.MapFrom(src => src.BelongsTo));
            CreateMap<SysShippingAddress, ShippingAddressEntity>();

            CreateMap<TransportOrder, OrderEntity>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.ChinaItems))
                .ForMember(dest => dest.IntNumber, opt => opt.MapFrom(src => src.SecondTrackNumber))
                .ForMember(dest => dest.IntCarrier, opt => opt.MapFrom(src => src.SecondCarrier))
                .ForMember(dest => dest.Route, opt => opt.MapFrom(src => src.RouteNavigation))
                .ForMember(dest => dest.WarehouseNotes, opt => opt.MapFrom(src => src.HiddenNotes))
                .ForMember(dest => dest.CustomerNotes, opt => opt.MapFrom(src => src.Memo))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.Insurance, opt => opt.MapFrom(src => src.Insurance ?? 0))
                .ForMember(dest => dest.Baggages, opt => opt.MapFrom(src => src.OrderBaggages))
                .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.OrderPhotos))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatuses))
                .ForMember(dest => dest.InternalStatus, opt => opt.MapFrom(src => src.OrderInternalStatuses))
                .ForMember(dest => dest.PickUpLocation, opt => opt.MapFrom(src => src.PickUpLocation))
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId))
                .ForMember(dest => dest.CargoNumber, opt => opt.MapFrom(src => GetCargoNumber(src)))
                .ForMember(dest => dest.LoadDeliveryBatchName, opt => opt.MapFrom(src => GetLoadDeliveryBatchName(src)));

            // TODO: use IDateTime
            CreateMap<OrderStatusInternal, OrderStatusEntity>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.Operator, opt => opt.MapFrom(src => src.User));
            CreateMap<OrderStatus, OrderStatusEntity>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DateCreated ?? DateTime.MinValue))
                .ForMember(dest => dest.Operator, opt => opt.MapFrom(src => src.User));
            CreateMap<CouponStatus, CouponStatusEntity>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.DateCreated))
                .ForMember(dest => dest.Operator, opt => opt.MapFrom(src => src.User));
            CreateMap<OrderBaggage, OrderBaggageEntity>();
            CreateMap<OrderBaggageEntity, OrderBaggage>();
            CreateMap<ChinaItem, OrderItemEntity>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ChineseName));
            CreateMap<OrderItemEntity, ChinaItem>()
                .ForMember(dest => dest.EnglishName, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.ChineseName, opt => opt.MapFrom(src => src.Name));
            CreateMap<OrderStatusEntity, OrderStatus>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Operator.Id))
                .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => src.Date));
            CreateMap<OrderPhoto, OrderPhotoEntity>();

            CreateMap<Warehouse, WarehouseEntity>();
            CreateMap<Route, RouteEntity>();
            CreateMap<DeliverProgress, DeliverProgressEntity>()
                .ForMember(dest => dest.IsDeleted, opt => opt.MapFrom(src => src.Hide));

            CreateMap<BatchBox, BatchBoxEntity>()
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.BatchBoxOrderMaps.Select(m => m.Order)));
            CreateMap<Batch, BatchEntity>()
                .ForMember(dest => dest.OtherOrders, opt => opt.MapFrom(src => src.BatchOtherOrders.Select(bo => bo.OtherOrder)))
                .ForMember(dest => dest.Creator, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.WeightKg, opt => opt.MapFrom(src => src.PaidWeightKg ?? 0))
                .ForMember(dest => dest.Boxes, opt => opt.MapFrom(src => GetAllBatchBoxes(src)))
                .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.RecipientUserId))
                .ForMember(dest => dest.Recipient, opt => opt.MapFrom(src => src.RecipientUser))
                .ForMember(dest => dest.AgentId, opt => opt.MapFrom(src => src.BelongsToUserId))
                .ForMember(dest => dest.Agent, opt => opt.MapFrom(src => src.BelongsToUser))
                .ForMember(dest => dest.PickUpLocation, opt => opt.MapFrom(src => src.PickUpLocation))
                .ForMember(dest => dest.FlightInfo, opt => opt.MapFrom(src => GetLoadDeliveryBatch(src).FlightInfo))
                .ForMember(dest => dest.CargoNumber, opt => opt.MapFrom(src => GetLoadDeliveryBatch(src).CargoNumber))
                .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src => GetLoadDeliveryBatch(src).ArrivalTime));

            CreateMap<Batch, PalletBatchEntity>()
                .ForMember(dest => dest.WeightKg, opt => opt.MapFrom(src => src.BatchPallets != null && src.BatchPallets.Any() ? src.BatchPallets.First().WeightKg : 0))
                .ForMember(dest => dest.Length, opt => opt.MapFrom(src => src.BatchPallets != null && src.BatchPallets.Any() ? src.BatchPallets.First().Length : 0))
                .ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.BatchPallets != null && src.BatchPallets.Any() ? src.BatchPallets.First().Width : 0))
                .ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.BatchPallets != null && src.BatchPallets.Any() ? src.BatchPallets.First().Height: 0))
                .ForMember(dest => dest.CustomName, opt => opt.MapFrom(src => src.BatchPallets != null && src.BatchPallets.Any() ? src.BatchPallets.First().CustomName : null))
                .ForMember(dest => dest.Boxes, opt => opt.MapFrom(src => GetAllBatchBoxes(src)))
                .ForMember(dest => dest.ShipFlightNumber, opt => opt.MapFrom(src => src.MasterBatch != null && src.MasterBatch.LoadDeliveryBatches != null && src.MasterBatch.LoadDeliveryBatches.Count > 0 ? src.MasterBatch.LoadDeliveryBatches.First().FlightInfo : ""))
                .ForMember(dest => dest.LoadDeliveryStage, opt => opt.MapFrom(src => ((BatchStageType)src.MasterBatch.Stage).GetDescription()))
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(src => src.Company.Code));

            CreateMap<Batch, PackageBatchEntity>()
                .ForMember(dest => dest.CustomName, opt => opt.MapFrom(src => src.BatchPackages != null && src.BatchPackages.Any() ? src.BatchPackages.First().CustomName : null))
                .ForMember(dest => dest.TransportStatus, opt => opt.MapFrom(src => src.BatchPackages != null && src.BatchPackages.Any() ? src.BatchPackages.First().TransportStatus : null))
                .ForMember(dest => dest.PaymentStatus, opt => opt.MapFrom(src => src.BatchPackages != null && src.BatchPackages.Any() ? src.BatchPackages.First().PaymentStatus : null))
                .ForMember(dest => dest.FinishStatus, opt => opt.MapFrom(src => src.BatchPackages != null && src.BatchPackages.Any() ? src.BatchPackages.First().FinishStatus : null))
                .ForMember(dest => dest.Boxes, opt => opt.MapFrom(src => GetAllBatchBoxes(src)))
                .ForMember(dest => dest.RecipientCodeName, opt => opt.MapFrom(src => $"{src.RecipientUser.OrderStartNumber} - {src.RecipientUser.Customer.Name}"))
                .ForMember(dest => dest.RecipientCode, opt => opt.MapFrom(src => src.RecipientUser.OrderStartNumber))
                .ForMember(dest => dest.RecipientId, opt => opt.MapFrom(src => src.RecipientUserId))
                .ForMember(dest => dest.PickUpLocationName, opt => opt.MapFrom(src => src.RecipientUser.PickUpLocationNavigation.Name))
                .ForMember(dest => dest.ShipFlightNumber, opt => opt.MapFrom(src => src.MasterBatch != null && src.MasterBatch.LoadDeliveryBatches != null && src.MasterBatch.LoadDeliveryBatches.Count > 0 ? src.MasterBatch.LoadDeliveryBatches.First().FlightInfo : ""))
                .ForMember(dest => dest.LoadDeliveryStage, opt => opt.MapFrom(src => ((BatchStageType)src.MasterBatch.Stage).GetDescription()))
                .ForMember(dest => dest.TotalOrders, opt => opt.MapFrom(src => GetTotalOrders(src)))
                .ForMember(dest => dest.TotalChargedWeightKg, opt => opt.MapFrom(src => GetTotalChargedWeightKg(src)))
                .ForMember(dest => dest.TotalVolume, opt => opt.MapFrom(src => GetTotalVolume(src)))
                .ForMember(dest => dest.TotalWeightKg, opt => opt.MapFrom(src => GetTotalWeightKg(src)))
                .ForMember(dest => dest.Duty, opt => opt.MapFrom(src => GetTotalDuty(src)))
                .ForMember(dest => dest.InsuranceFee, opt => opt.MapFrom(src => GetInsuranceFee(src)))
                .ForMember(dest => dest.RecipientCredit, opt => opt.MapFrom(src => GetRecipientCredit(src)))
                .ForMember(dest => dest.RecipientBalance, opt => opt.MapFrom(src => GetRecipientBalance(src)))
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(src => src.Company.Code));


            CreateMap<BalanceHistory, TransactionEntity>();

            CreateMap<SystemPhoto, SystemPhotoEntity>();
            CreateMap<BaseAdvert, SystemPhotoEntity>()
                .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.AdPictureKey));
            CreateMap<SystemSetting, SystemSettingsEntity>();
            CreateMap<SupportUser, SupportUserEntity>();
            CreateMap<RingCentralCredential, RingCentralCredentialEntity>();
            CreateMap<SMSLog, SMSLogEntity>();
            CreateMap<Coupon, CouponEntity>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.CouponStatuses));
            CreateMap<CouponBatch, CouponBatchEntity>()
                .ForMember(dest => dest.Coupons, opt => opt.MapFrom(src => src.Coupons))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
            CreateMap<Area, PickUpLocationAreaEntity>();
            CreateMap<TodoItem, TodoItemEntity>()
                .ForMember(dest => dest.Assignees, opt => opt.MapFrom(src => src.TodoItemAssignees.Select(a => a.Assignee)))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.BatchInfo, opt => opt.MapFrom(src => GetToDoItemBatchInfo(src)))
                .ForMember(dest => dest.CustomerInfo, opt => opt.MapFrom(src => GetToDoItemCustomerInfo(src)));
            CreateMap<Company, CompanyEntity>();
        }

        private static ICollection<BatchBox> GetAllBatchBoxes(Batch batch)
        {
            var boxes = new List<BatchBox>();
            if (batch.BatchBoxMaps != null)
            {
                foreach (var m in batch.BatchBoxMaps)
                {
                    boxes.Add(new BatchBox
                    {
                        Id = m.BatchBox.Id,
                        BatchId = m.BatchBox.BatchId,
                        Length = m.BatchBox.Length,
                        Width = m.BatchBox.Width,
                        Height = m.BatchBox.Height,
                        ActualWeightKg = m.BatchBox.ActualWeightKg,
                        Name = m.BatchBox.Name,
                        Number = m.BatchBox.Number,
                        OriginalObjectNumber = m.OriginalObjectNumber,
                        BatchBoxOrderMaps = m.BatchBox.BatchBoxOrderMaps,
                        BatchBoxMaps = m.BatchBox.BatchBoxMaps,
                        Batch = m.BatchBox.Batch,
                    });
                }
            }
            boxes.AddRange(batch.BatchBoxes.Where(bb => !boxes.Any(box => box.Id == bb.Id)));
            return boxes;
        }
        
        private LoadDeliveryBatch GetLoadDeliveryBatch(Batch batch)
        {
            if (batch.LoadDeliveryBatches?.Count() > 0)
            {
                return batch.LoadDeliveryBatches.First();
            }
            return new LoadDeliveryBatch();
        }
    }
}
