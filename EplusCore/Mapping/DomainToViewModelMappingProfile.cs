using System.Linq;
using AutoMapper;
using Domain.Entities;
using Domain.Models.Extensions;
using WebUI.Models.ViewModels;

namespace WebUI.Mapping
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMapForOrder();

            CreateMapForBatch();

            CreateMap<WarehouseEntity, WarehouseViewModel>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photo));
            CreateMap<WarehouseViewModel, WarehouseEntity>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.PhotoUrl));

            CreateMap<RouteEntity, RouteViewModel>()
                .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src => src.Photo));
            CreateMap<RouteViewModel, RouteEntity>()
                .ForMember(dest => dest.Photo, opt => opt.MapFrom(src => src.PhotoUrl));

            CreateMap<TransactionEntity, TransactionViewModel>();
            CreateMap<UserEntity, UserDetailViewModel>();
            CreateMap<UserEntity, UserInventoryViewModel>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.GetDescription()))
                .ForMember(dest => dest.BelongsToName, opt => opt.MapFrom(src => src.BelongsTo == null ? "" : src.BelongsTo.Name));
        }

        private void CreateMapForOrder()
        {
            CreateMap<OrderEntity, OrderInventoryViewModel>()
                .ForMember(dest => dest.StateText, opt => opt.MapFrom(src => src.State.GetDescription()))
                .ForMember(dest => dest.LatestStatus, opt => opt.MapFrom(src => src.Status.OrderByDescending(os => os.Date).FirstOrDefault() ?? new OrderStatusEntity()));

            CreateMap<OrderEntity, OrderDetailViewModel>()
                .ForMember(dest => dest.InsuranceClaim, opt => opt.MapFrom(src => src.Insurance * 10));

            CreateMap<OrderStatusEntity, OrderStatusViewModel>();

            CreateMap<OrderItemEntity, OrderItemEditModel>();
            CreateMap<OrderBaggageEntity, OrderBaggageEditModel>();
            CreateMap<OrderEntity, OrderDetailEditModel>()
                .ForMember(dest => dest.BaggageEditModels, opt => opt.MapFrom(src => src.Baggages))
                .ForMember(dest => dest.ItemEditModels, opt => opt.MapFrom(src => src.Items));
            CreateMap<OrderDetailEditModel, OrderEntity>();
            CreateMap<TodoItemEntity, TodoItemInventoryViewModel>()
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedBy.Name))
                .ForMember(dest => dest.CreatedByUserId, opt => opt.MapFrom(src => src.CreatedBy.Id))
                .ForMember(dest => dest.StatusText, opt => opt.MapFrom(src => src.Status.GetDescription()));
        }

        private void CreateMapForBatch()
        {
            CreateMap<BatchEntity, BatchViewModel>()
                .ForMember(dest => dest.Orders, opt => opt.MapFrom(src => src.Boxes.SelectMany(o => o.Orders)))
                .ForMember(dest => dest.StageDescription, opt => opt.MapFrom(src => src.Stage.GetDescription()));
        }
    }
}
