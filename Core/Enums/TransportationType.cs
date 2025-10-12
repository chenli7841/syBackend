using System.ComponentModel;

namespace Domain.Enums
{
    public enum TransportationType
    {
        [Description("海运")]
        Ocean = 0,
        [Description("空运")]
        Air = 1,
        [Description("陆运")]
        Land = 2,
    }
}
