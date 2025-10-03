using System.ComponentModel;

namespace Domain.Enums
{
    public enum RoleType
    {
        [Description("管理员")]
        Admin = 1,
        [Description("高级用户")]
        Advanced = 2,
        [Description("普通用户")]
        Regular = 3
    }
}
