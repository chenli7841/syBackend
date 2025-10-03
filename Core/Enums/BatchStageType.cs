using System.ComponentModel;

namespace Domain.Enums
{
    public enum BatchStageType
    {
        [Description("")]
        Undefined = 0,
        [Description("无")]
        None = -1,
        [Description("集货中")]
        Gathering = 1,
        [Description("装车发货")]
        LoadDelivery = 2,
        [Description("已起航")]
        Sailing = 3,
        [Description("清关中")]
        Clearing = 4,
        [Description("分拣中")]
        Sorting = 5,
        [Description("等待取货")]
        PendingPickUp = 6,
        [Description("完成")]
        Done = 10,
    }
}
