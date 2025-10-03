using System.ComponentModel;

namespace Domain.Enums
{
    public enum BatchActionType
    {
        None,
        [Description("下一步")]
        Next,
        [Description("余额扣款")]
        BalancePay,
        [Description("拆分")]
        Split,
        [Description("按取货点拆分")]
        SplitByLocations,
        [Description("按客户归属拆分")]
        SplitByRecipients,
        [Description("按群主拆分")]
        SplitByAgents,
        [Description("按非自有取货点拆分")]
        SplitByNonLocation,
        [Description("按非自有代理拆分")]
        SplitByNonAgent,
        [Description("返利")]
        Commission,
        [Description("更新批次名")]
        UpdateLoadDeliveryProperties
    }
}
