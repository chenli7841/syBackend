using System;

namespace Common
{
    public interface IDateTime
    {
        DateTime UserNow { get; }
        DateTime UtcNow { get; }
        DateTime MinValue { get; }
        DateTime OrderStartTime { get; }
    }
}
