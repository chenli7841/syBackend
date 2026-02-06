using System;
using Common;

namespace Infrastructure
{
    public class EplusDateTime : IDateTime
    {
        public DateTime UserNow
        {
            get
            {
                // China Standard Time is in Windows
                // In Linux, search for /usr/share/zoneinfo/, we can use Asia/Shanghai
                var chinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Asia/Shanghai");
                //var chinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, chinaTimeZone);
            }
        }

        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime MinValue => DateTime.MinValue;
        public DateTime OrderStartTime => new DateTime(2018, 1, 1);
    }
}
