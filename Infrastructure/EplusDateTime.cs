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
                var chinaTimeZone = TimeZoneInfo.FindSystemTimeZoneById("China Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, chinaTimeZone);
            }
        }

        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime MinValue => DateTime.MinValue;
        public DateTime OrderStartTime => new DateTime(2018, 1, 1);
    }
}
