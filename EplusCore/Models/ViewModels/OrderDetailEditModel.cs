using System;
using System.Collections.Generic;
using Domain.Entities;

namespace WebUI.Models.ViewModels
{
    public class OrderDetailEditModel : OrderEntity
    {
        public OrderDetailEditModel()
        {
            ItemEditModels = new List<OrderItemEditModel>();
            Baggages = new List<OrderBaggageEntity>();
            ItemCategories = new List<string>();
            Routes = new List<RouteEntity>();
            BatchBoxes = new List<OrderBatchBoxModel>();
            Operations = new List<OrderOperationModel>();
        }

        public IList<OrderItemEditModel> ItemEditModels { get; set; }
        public IList<OrderBaggageEditModel> BaggageEditModels { get; set; }

        public IEnumerable<string> ItemCategories { get; set; }
        public IEnumerable<RouteEntity> Routes { get; set; }
        public IList<OrderBatchBoxModel> BatchBoxes { get; set; }
        public IEnumerable<OrderOperationModel> Operations { get; set; }
    }

    public class OrderOperationModel
    {
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string Operator { get; set; }
    }

    public class OrderBatchBoxModel
    {
        public int BatchId { get; set; }
        public int BoxId { get; set; }
        public string BatchName { get; set; }
        public int BoxNumber { get; set; }
    }

    public class OrderItemEditModel : OrderItemEntity
    {
        public ActionType Action { get; set; }
    }

    public class OrderBaggageEditModel : OrderBaggageEntity
    {
        public ActionType Action { get; set; }
    }

    public enum ActionType
    {
        Keep,
        Delete,
        Add
    }
}
