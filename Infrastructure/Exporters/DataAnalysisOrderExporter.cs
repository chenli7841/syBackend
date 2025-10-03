using ClosedXML.Excel;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Exporters
{
    public class DataAnalysisOrderExporter : IExcelExporter<DataAnalysisOrderEntity>
    {
        private readonly Dictionary<
            string,
            (Func<DataAnalysisOrderEntity, string> data, Type dataType)
        > _columnMapper =
            new()
            {
                { "创建时间", (t => t.DateCreated, typeof(DateTime)) },
                { "用户编号", (t => t.OrderStartNumber, typeof(string)) },
                { "单号", (t => t.OrderNumber, typeof(string)) },
                { "线路", (t => t.Route, typeof(string)) },
                { "运费", (t => t.ShippingCost?.ToString(), typeof(decimal)) },
                { "运单取货点", (t => t.PickUpLocation, typeof(string)) },
                { "客户代理归属", (t => t.Agent, typeof(string)) },
                { "物品种类", (t => t.ItemType, typeof(string)) },
                { "装车发货批次", (t => t.LoadDeliveryBatchName, typeof(string)) },
                { "返利", (t => t.PayCommission.ToString("0.00"), typeof(decimal)) },
            };

        private readonly Dictionary<
            string,
            (Func<DataAnalysisOrderSummary, string> data, Type dataType)
        > _recipientColumnMapper =
            new()
            {
                { "客户编号", (t => t.GroupId, typeof(string)) },
                { "单数", (t => t.OrderCount.ToString(), typeof(int)) },
                { "运费", (t => t.ShippingCost.ToString(), typeof(decimal)) }
            };

        private readonly Dictionary<
            string,
            (Func<DataAnalysisOrderSummary, string> data, Type dataType)
        > _agentColumnMapper =
            new()
            {
                { "代理编号", (t => t.GroupId, typeof(string)) },
                { "单数", (t => t.OrderCount.ToString(), typeof(int)) },
                { "运费", (t => t.ShippingCost.ToString(), typeof(decimal)) },
                { "返利", (t => t.PayCommission.ToString(), typeof(decimal)) }
            };

        private readonly Dictionary<
            string,
            (Func<DataAnalysisOrderSummary, string> data, Type dataType)
        > _locationColumnMapper =
            new()
            {
                { "取货点", (t => t.GroupId, typeof(string)) },
                { "单数", (t => t.OrderCount.ToString(), typeof(int)) },
                { "运费", (t => t.ShippingCost.ToString(), typeof(decimal)) },
                { "返利", (t => t.PayCommission.ToString(), typeof(decimal)) }
            };

        private List<DataAnalysisOrderSummary> _recipients;
        private List<DataAnalysisOrderSummary> _agents;
        private List<DataAnalysisOrderSummary> _locations;
        public DataAnalysisOrderExporter(List<DataAnalysisOrderSummary> recipients, List<DataAnalysisOrderSummary> agents, List<DataAnalysisOrderSummary> locations)
        {
            _recipients = recipients;
            _agents = agents;
            _locations = locations;
        }

        public XLWorkbook Export(IEnumerable<DataAnalysisOrderEntity> orders)
        {
            var wb = new XLWorkbook();

            #region Sheet 1: Order List

            var dt1 = new DataTable();
            foreach (var key in _columnMapper.Keys)
            {
                dt1.Columns.Add(key, _columnMapper[key].dataType);
            }

            foreach (var o in orders)
            {
                var row = dt1.NewRow();

                foreach (var key in _columnMapper.Keys)
                {
                    var value = _columnMapper[key].data(o);
                    if (value != null)
                    {
                        row[key] = value;
                    }
                }
                dt1.Rows.Add(row);
            }

            var ws = wb.Worksheets.Add(dt1, "运单列表");
            ws.Column("A").Style.DateFormat.SetFormat("yyyy-MM-dd");
            ws.Column("B").Style.NumberFormat.SetFormat("0000");
            ws.Column("E").Style.NumberFormat.SetFormat("0.00");
            ws.Column("J").Style.NumberFormat.SetFormat("0.00");
            AutoFit(ws);
            #endregion

            #region Sheet 2: Summary by Recipient

            var dt2 = new DataTable();
            foreach (var key in _recipientColumnMapper.Keys)
            {
                dt2.Columns.Add(key, _recipientColumnMapper[key].dataType);
            }

            foreach (var r in _recipients)
            {
                var row = dt2.NewRow();

                foreach (var key in _recipientColumnMapper.Keys)
                {
                    var value = _recipientColumnMapper[key].data(r);
                    if (value != null)
                    {
                        row[key] = value;
                    }
                }
                dt2.Rows.Add(row);
            }
            ws = wb.Worksheets.Add(dt2, "客户");
            ws.Column("C").Style.NumberFormat.SetFormat("0.00");
            AutoFit(ws);
            #endregion

            #region Sheet 3: Summary by Agent

            var dt3 = new DataTable();
            foreach (var key in _agentColumnMapper.Keys)
            {
                dt3.Columns.Add(key, _agentColumnMapper[key].dataType);
            }

            foreach (var a in _agents)
            {
                var row = dt3.NewRow();

                foreach (var key in _agentColumnMapper.Keys)
                {
                    var value = _agentColumnMapper[key].data(a);
                    if (value != null)
                    {
                        row[key] = value;
                    }
                }
                dt3.Rows.Add(row);
            }
            ws = wb.Worksheets.Add(dt3, "代理");
            ws.Column("C").Style.NumberFormat.SetFormat("0.00");
            ws.Column("D").Style.NumberFormat.SetFormat("0.00");
            AutoFit(ws);
            #endregion

            #region Sheet 4: Summary by Location

            var dt4 = new DataTable();
            foreach (var key in _locationColumnMapper.Keys)
            {
                dt4.Columns.Add(key, _locationColumnMapper[key].dataType);
            }

            foreach (var l in _locations)
            {
                var row = dt4.NewRow();

                foreach (var key in _locationColumnMapper.Keys)
                {
                    var value = _locationColumnMapper[key].data(l);
                    if (value != null)
                    {
                        row[key] = value;
                    }
                }
                dt4.Rows.Add(row);
            }
            ws = wb.Worksheets.Add(dt4, "取货点");
            ws.Column("C").Style.NumberFormat.SetFormat("0.00");
            ws.Column("D").Style.NumberFormat.SetFormat("0.00");
            AutoFit(ws);
            #endregion

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
            ws.Column("J").AdjustToContents();
        }
    }
}
