using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Domain.Entities;
using Persistence.Data;

namespace Persistence
{
    public class DbModelToEntityMappingProfile : Profile
    {
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
                .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId));

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
                .ForMember(dest => dest.Boxes, opt => opt.MapFrom(src => GetAllBatchBoxes(src)));

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
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy));
            CreateMap<Company, CompanyEntity>();
        }

        private ICollection<BatchBox> GetAllBatchBoxes(Batch batch)
        {
            var boxes = new List<BatchBox>();
            if (batch.BatchBoxMaps != null)
            {
                foreach (var m in batch.BatchBoxMaps)
                {
                    boxes.Add(m.BatchBox);
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
