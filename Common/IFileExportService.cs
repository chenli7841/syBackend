using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Common
{
    public interface IFileExportService
    {
        IDisposable Export<T>(IEnumerable<T> records, string format, UserEntity user = null, CouponBatchEntity couponBatch = null);
    }
}
