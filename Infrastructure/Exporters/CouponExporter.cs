using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClosedXML.Excel;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Exporters
{
    public class CouponExporter : IExcelExporter<CouponEntity>
    {
        private readonly Dictionary<string, Func<CouponEntity, string>> _anonymousColumnMapper =
            new()
            {
                { "优惠券号码", c => c.CouponNumber },
                { "国内单号", c => c.DomesticNumber },
                { "使用人", c => c.ConsumedUser?.Name ?? "" },
                { "使用时间", c => c.Status?.FirstOrDefault(s => s.Status == CouponStatusType.CouponConsumed)?.Date.ToString("yyyy-MM-dd hh:mm:ss") ?? "" },
            };

        private readonly Dictionary<string, Func<CouponEntity, string>> _unanonymousColumnMapper =
            new()
            {
                { "优惠券号码", c => c.CouponNumber },
                { "国内单号", c => c.DomesticNumber },
                { "指定用户", c => c.AssignedUser?.Name ?? "" },
                { "使用人", c => c.ConsumedUser?.Name ?? "" },
                { "使用时间", c => c.Status?.FirstOrDefault(s => s.Status == CouponStatusType.CouponConsumed)?.Date.ToString("yyyy-MM-dd hh:mm:ss") ?? "" },
            };

        private readonly CouponBatchEntity _couponBatchEntity;

        public CouponExporter(CouponBatchEntity couponBatchEntity)
        {
            _couponBatchEntity = couponBatchEntity;
        }

        public XLWorkbook Export(IEnumerable<CouponEntity> coupons)
        {
            var wb = new XLWorkbook();
            var summaryWorksheet = wb.Worksheets.Add("总结");

            summaryWorksheet.Cell("A1").Value = "创建人";
            summaryWorksheet.Cell("B1").Value = _couponBatchEntity.CreatedBy.Name;
            summaryWorksheet.Cell("A2").Value = "创建时间";
            summaryWorksheet.Cell("B2").Value = _couponBatchEntity.CreateTime;
            summaryWorksheet.Cell("B2").Style.DateFormat.SetFormat("yyyy-MM-dd hh:mm:ss");
            summaryWorksheet.Cell("A3").Value = "类型";
            if (_couponBatchEntity.Anonymous == null)
            {
                summaryWorksheet.Cell("B3").Value = "未定";
            }
            else if (_couponBatchEntity.Anonymous == false)
            {
                summaryWorksheet.Cell("B3").Value = "记名";
            }
            else
            {
                summaryWorksheet.Cell("B3").Value = "不记名";
            }
            summaryWorksheet.Cell("A4").Value = "有效期起始";
            if (coupons.Any() && coupons.First().ValidFrom.HasValue)
            {
                summaryWorksheet.Cell("B4").Value = coupons.First().ValidFrom;
            }
            summaryWorksheet.Cell("B4").Style.DateFormat.SetFormat("yyyy-MM-dd hh:mm:ss");
            summaryWorksheet.Cell("A5").Value = "有效期结束";
            if (coupons.Any() && coupons.First().ValidUntil.HasValue)
            {
                summaryWorksheet.Cell("B5").Value = coupons.First().ValidUntil;
            }
            summaryWorksheet.Cell("B5").Style.DateFormat.SetFormat("yyyy-MM-dd hh:mm:ss");
            summaryWorksheet.Cell("A6").Value = "优惠金额";
            if (coupons.Any())
            {
                if (coupons.First().MinimumPrice > 0)
                {
                    summaryWorksheet.Cell("B6").Value = $"满{Decimal.Truncate(coupons.First().MinimumPrice)}减{Math.Abs(coupons.First().ShippingCost).ToString("0.00")}";                    
                }
                else
                {
                    summaryWorksheet.Cell("B6").Value = Math.Abs(coupons.First().ShippingCost).ToString("0.00");
                }
            }
            AutoFit(summaryWorksheet);
            
            var dt = new DataTable();
            if (_couponBatchEntity.Anonymous == false)
            {
                foreach (var key in _unanonymousColumnMapper.Keys)
                {
                    dt.Columns.Add(key);
                }

                foreach (var c in coupons)
                {
                    var row = dt.NewRow();

                    foreach (var key in _unanonymousColumnMapper.Keys)
                    {
                        row[key] = _unanonymousColumnMapper[key](c);
                    }
                    dt.Rows.Add(row);
                }
            }
            else
            {
                foreach (var key in _anonymousColumnMapper.Keys)
                {
                    dt.Columns.Add(key);
                }

                foreach (var c in coupons)
                {
                    var row = dt.NewRow();

                    foreach (var key in _anonymousColumnMapper.Keys)
                    {
                        row[key] = _anonymousColumnMapper[key](c);
                    }
                    dt.Rows.Add(row);
                }
            }

            var ws = wb.Worksheets.Add(dt, "优惠券列表");
            AutoFit(ws);
            return wb;
        }

        private void AutoFit(IXLWorksheet ws)
        {
            ws.Column("A").AdjustToContents();
            ws.Column("B").AdjustToContents();
            ws.Column("C").AdjustToContents();
            ws.Column("D").AdjustToContents();
            ws.Column("E").AdjustToContents();
            ws.Column("F").AdjustToContents();
            ws.Column("G").AdjustToContents();
            ws.Column("H").AdjustToContents();
            ws.Column("I").AdjustToContents();
        }
    }
}
