using System.ComponentModel;

namespace Domain.Enums
{
    public enum CouponStatusType
    {
        [Description("已创建")]
        CouponCreated = 1,
        [Description("已打印")]
        CouponPrinted = 11,
        [Description("已寄送")]
        CouponMailed = 12,
        [Description("已指定用户")]
        CouponAssigned = 21,
        [Description("已生效")]
        ValidPeriodBegan = 31,
        [Description("已失效")]
        ValidPeriodEnded = 32,
        [Description("已使用")]
        CouponConsumed = 41
    }
}