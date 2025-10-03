using System;
using System.Collections.Generic;
using Common;
using Infrastructure.Exporters;
using Domain.Services;
using Domain.Entities;

namespace Infrastructure
{
    public class FileExportService: IFileExportService
    {
        private readonly IUserService _userService;
        public FileExportService(IUserService userService)
        {
            _userService = userService;
        }

        public IDisposable Export<T>(IEnumerable<T> records, string format, UserEntity user = null, CouponBatchEntity couponBatch = null)
        {
            IExcelExporter<T> exporter;
            if (format == "user")
            {
                exporter = (IExcelExporter<T>) new UserExporter();
            }
            else if (format == "bill")
            {
                exporter = (IExcelExporter<T>) new TransactionExporter(user);
            }
            else if (format == "coupon")
            {
                exporter = (IExcelExporter<T>) new CouponExporter(couponBatch);
            }
            else
            {
                exporter = (IExcelExporter<T>) new HaiYunDetailExcelExporter();
            }
            return exporter.Export(records);
        }
    }
}
