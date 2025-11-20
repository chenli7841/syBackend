using System.ComponentModel;

namespace Domain.Enums
{
    public enum RoleType
    {
        [Description("高级管理员")]
        Admin = 1,
        [Description("高级用户")]
        Advanced = 2,
        [Description("普通用户")]
        Regular = 3,
        [Description("中国仓库")]
        ChinaWarehouse = 4,
        [Description("加拿大仓库")]
        CanadaWarehouse = 5,
        [Description("管理员")]
        SubAdmin = 6,
        [Description("高级管理员")]
        SuperAdmin = 7,

    }
}
