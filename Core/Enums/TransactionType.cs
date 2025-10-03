using System.ComponentModel;

namespace Domain.Enums
{
    public enum TransactionType
    {
        [Description("运单支付")]
        ReceiveOrder = 0,
        [Description("余额调整")]
        Adjust = 1,
        [Description("活动充值")]
        EventDeposit = 2,
        [Description("理赔款项")]
        Claim = 3,
        [Description("余额扣减")]
        Deduct = 4,
        [Description("给自己充值")]
        SelfDeposit = 5,
        [Description("给他人充值")]
        OtherDeposit = 6,
        [Description("收取运费")]
        ShippingCost = 7,
        [Description("运费返利")]
        ShippingCommission = 8,
        [Description("仓库使用费")]
        WarehouseCost = 9,
        [Description("批次扣款")]
        BatchDeduct = 10,
        [Description("结算调整")]
        AccountingAdjust = 11,
        [Description("现金出账")]
        CashOut = 12
    }
}
