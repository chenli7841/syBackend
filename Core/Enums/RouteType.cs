using System.ComponentModel;

namespace Domain.Enums
{
    public enum RouteType
    {
        [Description("集运")]
        Mixed = 1,
        [Description("直邮")]
        Direct = 2,
        [Description("团购")]
        Group = 3,
        [Description("回国")]
        China = 4
    }
}
