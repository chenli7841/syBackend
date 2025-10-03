using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClosedXML.Excel;
using Domain.Entities;
using Domain.Models.Extensions;

namespace Infrastructure.Exporters
{
    class TransactionExporter : IExcelExporter<TransactionEntity>
    {
        private static string GetAmount(TransactionEntity t, UserEntity u)
        {
            return t.ToUser.Id == u.Id ? (t.ToUserActualAmount ?? t.Amount).ToString() : (t.FromUserDisplayAmount ?? t.Amount).ToString();
        }

        private readonly UserEntity _userEntity;

        private readonly Dictionary<
            string, 
            (Func<TransactionEntity, UserEntity, string> data, Type dataType)
        > _columnMapper1 =
            new()
            {
                { "日期", ((t, u) => t.Date.ToString("yyyy-MM-dd hh:mm:ss"), typeof(DateTime)) },
                { "支付方", ((t, u) => t.FromUser.OrderStartNumber, typeof(int)) },
                { "收款人", ((t, u) => t.ToUser.OrderStartNumber, typeof(int)) },
                { "备注", ((t, u) => t.Notes, typeof(string)) },
                { "交易类型", ((t, u) => t.Type.GetDescription(), typeof(string)) },
                { "交易额", ((t, u) => GetAmount(t, u), typeof(decimal)) },
                { "运单号", ((t, u) => t.Order?.OrderNumber, typeof(string)) },
                { "收费重量", ((t, u) => t.Order?.WeightKg.ToString("0.00"), typeof(decimal)) },
                { "方式", ((t, u) => t.Method, typeof(string)) },
            };

        private readonly Dictionary<
            string, 
            (Func<TransactionEntity, UserEntity, string> data, Type dataType)
        > _columnMapper2 =
            new()
            {
                { "日期", ((t, u) => t.Date.ToString("yyyy-MM-dd hh:mm:ss"), typeof(DateTime)) },
                { "支付方", ((t, u) => t.FromUser.OrderStartNumber, typeof(int)) },
                { "收款人", ((t, u) => t.ToUser.OrderStartNumber, typeof(int)) },
                { "备注", ((t, u) => t.Notes, typeof(string)) },
                { "交易类型", ((t, u) => t.Type.GetDescription(), typeof(string)) },
                { "交易额", ((t, u) => GetAmount(t, u), typeof(decimal)) },
                { "批次名", ((t, u) => t.Batch?.Name, typeof(string)) },
                { "收费重量", ((t, u) => t.Batch.WeightKg.ToString("0.00"), typeof(decimal)) },
                { "方式", ((t, u) => t.Method, typeof(string)) },
            };

        private readonly Dictionary<
            string, 
            (Func<TransactionEntity, UserEntity, string> data, Type dataType)
        > _columnMapper3 =
            new()
            {
                { "日期", ((t, u) => t.Date.ToString("yyyy-MM-dd hh:mm:ss"), typeof(DateTime)) },
                { "支付方", ((t, u) => t.FromUser.OrderStartNumber, typeof(int)) },
                { "收款人", ((t, u) => t.ToUser.OrderStartNumber, typeof(int)) },
                { "备注", ((t, u) => t.Notes, typeof(string)) },
                { "交易类型", ((t, u) => t.Type.GetDescription(), typeof(string)) },
                { "交易额", ((t, u) => GetAmount(t, u), typeof(decimal)) },
                { "方式", ((t, u) => t.Method, typeof(string)) },
            };

        Func<TransactionEntity, bool> filter1 = t => t.Order != null && t.Method == "现金支付";
        Func<TransactionEntity, bool> filter2 = t => t.Order != null && t.Method == "余额支付";
        Func<TransactionEntity, bool> filter3 = t => t.Batch != null && t.Type == Domain.Enums.TransactionType.ShippingCommission;
        Func<TransactionEntity, bool> filter4 = t => t.Batch != null && (t.Type == Domain.Enums.TransactionType.WarehouseCost || t.Type == Domain.Enums.TransactionType.BatchDeduct);

        public TransactionExporter(UserEntity userEntity)
        {
            _userEntity = userEntity;
        }

        public XLWorkbook Export(IEnumerable<TransactionEntity> transactions)
        {
            var wb = new XLWorkbook();

            var summaryWorksheet = wb.Worksheets.Add("总结");
            summaryWorksheet.Cell("A1").Value = "代理账单";
            summaryWorksheet.Cell("B1").Value = _userEntity.Name;
            summaryWorksheet.Cell("A2").Value = "日期";
            summaryWorksheet.Cell("B2").Value = DateTime.Now;
            summaryWorksheet.Cell("B2").Style.DateFormat.SetFormat("yyyy-MM-dd hh:mm:ss");
            summaryWorksheet.Cell("B3").Value = "总重量";
            summaryWorksheet.Cell("C3").Value = "总运费";
            summaryWorksheet.Cell("A4").Value = "现金收货";
            summaryWorksheet.Cell("B4").FormulaA1 = "=SUM(现金收货!H:H)";
            summaryWorksheet.Cell("C4").FormulaA1 = "=SUMIF(现金收货!F:F, \">0\")-SUMIF(现金收货!F:F, \"<0\")";
            summaryWorksheet.Cell("A5").Value = "余额收货";
            summaryWorksheet.Cell("B5").FormulaA1 = "=SUM(余额收货!H:H)";
            summaryWorksheet.Cell("C5").FormulaA1 = "=SUMIF(余额收货!F:F, \">0\")-SUMIF(余额收货!F:F, \"<0\")";
            summaryWorksheet.Cell("A6").Value = "系统返利";
            summaryWorksheet.Cell("B6").FormulaA1 = "=SUM(系统返利!H:H)";
            summaryWorksheet.Cell("C6").FormulaA1 = "=SUMIF(系统返利!F:F, \">0\")-SUMIF(系统返利!F:F, \"<0\")";
            summaryWorksheet.Cell("A7").Value = "系统扣款";
            summaryWorksheet.Cell("B7").FormulaA1 = "=SUM(系统扣款!H:H)";
            summaryWorksheet.Cell("C7").FormulaA1 = "=SUMIF(系统扣款!F:F, \">0\")-SUMIF(系统扣款!F:F, \"<0\")";
            summaryWorksheet.Cell("A8").Value = "其他";
            summaryWorksheet.Cell("C8").FormulaA1 = "=SUMIF(其他!F:F, \">0\")-SUMIF(其他!F:F, \"<0\")";
            summaryWorksheet.Cell("A10").Value = "结余";
            summaryWorksheet.Cell("B10").Value = _userEntity.Balance;
            summaryWorksheet.Cell("B10").Style.NumberFormat.SetFormat("0.00");
            summaryWorksheet.Column("B").AdjustToContents();
            

            var dt1 = new DataTable();
            foreach (var key in _columnMapper1.Keys)
            {
                dt1.Columns.Add(key, _columnMapper1[key].dataType);
            }

            foreach (var transaction in transactions.Where(filter1))
            {
                var row = dt1.NewRow();

                foreach (var key in _columnMapper1.Keys)
                {
                    row[key] = _columnMapper1[key].data(transaction, _userEntity);
                }
                dt1.Rows.Add(row);
            }

            var ws = wb.Worksheets.Add(dt1, "现金收货");
            SetFormat(ws);

            var dt2 = new DataTable();
            foreach (var key in _columnMapper1.Keys)
            {
                dt2.Columns.Add(key, _columnMapper1[key].dataType);
            }

            foreach (var transaction in transactions.Where(filter2))
            {
                var row = dt2.NewRow();

                foreach (var key in _columnMapper1.Keys)
                {
                    row[key] = _columnMapper1[key].data(transaction, _userEntity);
                }
                dt2.Rows.Add(row);
            }

            ws = wb.Worksheets.Add(dt2, "余额收货");
            SetFormat(ws);

            var dt3 = new DataTable();
            foreach (var key in _columnMapper2.Keys)
            {
                dt3.Columns.Add(key, _columnMapper2[key].dataType);
            }

            foreach (var transaction in transactions.Where(filter3))
            {
                var row = dt3.NewRow();

                foreach (var key in _columnMapper2.Keys)
                {
                    row[key] = _columnMapper2[key].data(transaction, _userEntity);
                }
                dt3.Rows.Add(row);
            }
            ws = wb.Worksheets.Add(dt3, "系统返利");
            SetFormat(ws);

            var dt4 = new DataTable();
            foreach (var key in _columnMapper2.Keys)
            {
                dt4.Columns.Add(key, _columnMapper2[key].dataType);
            }

            foreach (var transaction in transactions.Where(filter4))
            {
                var row = dt4.NewRow();

                foreach (var key in _columnMapper2.Keys)
                {
                    row[key] = _columnMapper2[key].data(transaction, _userEntity);
                }
                dt4.Rows.Add(row);
            }
            ws = wb.Worksheets.Add(dt4, "系统扣款");
            SetFormat(ws);

            var dt5 = new DataTable();
            foreach (var key in _columnMapper3.Keys)
            {
                dt5.Columns.Add(key, _columnMapper3[key].dataType);
            }

            foreach (var transaction in transactions.Where(t => !filter1(t) && !filter2(t) && !filter3(t) && !filter4(t)))
            {
                var row = dt5.NewRow();

                foreach (var key in _columnMapper3.Keys)
                {
                    row[key] = _columnMapper3[key].data(transaction, _userEntity);
                }
                dt5.Rows.Add(row);
            }

            ws = wb.Worksheets.Add(dt5, "其他");
            SetFormat(ws);

            return wb;
        }

        private void SetFormat(IXLWorksheet ws)
        {
            ws.Column("A").Style.DateFormat.SetFormat("yyyy-MM-dd hh:mm:ss");
            ws.Column("B").Style.NumberFormat.SetFormat("0000");
            ws.Column("C").Style.NumberFormat.SetFormat("0000");
            ws.Column("F").Style.NumberFormat.SetFormat("0.00");
            ws.Column("H").Style.NumberFormat.SetFormat("0.00");
            AutoFit(ws);
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
