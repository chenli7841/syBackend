using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;

namespace WebUI.Models
{
    public class BatchInventoryResponse
    {
        public BatchInventoryResponse()
        {
            Routes = new List<RouteEntity>();
            Users = new List<UserEntity>();
        }

        public BatchGroupType GroupType { get; set; }
        public int? SelectedRouteId { get; set; }
        public int? SelectedWarehouseId { get; set; }
        public int? SelectedRecipientUserId { get; set; }
        public int? SelectedBelongsToUserId { get; set; }
        public IEnumerable<RouteEntity> Routes { get; set; }
        public IEnumerable<WarehouseEntity> Warehouses { get; internal set; }
        public IEnumerable<UserEntity> Users { get; set; }
        public IEnumerable<CompanyEntity> Companies { get; set; }
        public string CompanyIds { get; set; }
    }
}
