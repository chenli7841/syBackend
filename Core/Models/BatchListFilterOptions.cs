using System.Collections.Generic;
using Domain.Enums;

namespace Domain.Models
{
    public class BatchListFilterOptions : FilterOptions
    {
        public BatchListFilterOptions()
        {
            Ids = new List<int>();
            RecipientIds = new List<int>();
            BelongsToUserIds = new List<int>();
        }

        public BatchGroupType? GroupType { get; set; }
        public int? WarehouseId { get; set; }
        public int? RouteId { get; set; }
        public List<int> Ids { get; set; }
        // 客户归属
        public List<int> RecipientIds { get; set; }
        // 代理归属
        public List<int> BelongsToUserIds { get; set; }
        // 隐藏已完成的批次
        public bool HideCompletedBatches { get; set; }
    }
}
