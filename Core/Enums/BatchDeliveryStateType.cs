using System.ComponentModel;

namespace Domain.Enums
{
    public enum BatchDeliveryStateType
    {
        None = 0,
        [Description("到达港口等待起航")]
        PendingDeparture = 1,
        [Description("运输中")]
        InTransit = 2,
        [Description("抵达目的港")]
        ArrivedAtDestinationHarbour = 3,
        [Description("抵达仓库")]
        ArrivedAtWarehouse = 4,
        [Description("等待取货中")]
        PendingPickup = 5
    }
}
