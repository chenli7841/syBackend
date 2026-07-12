using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using ClosedXML.Excel;
using Domain.Entities;

namespace Infrastructure.Exporters
{
    public class BatchOrderExcelExporter : IExcelExporter<OrderEntity>
    {
        private static string GetBaggageInfos(OrderEntity order)
        {
            return string.Join(", \n", order.Baggages.Select(b => $"{b.Length}*{b.Width}*{b.Height} - {b.WeightKg}"));
        }
        private static string[] ColumnsToMerge = new string[] {"A", "B", "C", "D", "E", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA", "AB", "AC", "AD", "AE", "AF"};
        private readonly Dictionary<string, Func<OrderEntity, OrderItemEntity, bool, decimal, string>> _columnMapper = new()
        {
            {"唛头", (order, item, isFirstItem, accWeight) => ""}, // Col A
            {"客户号", (order, item, isFirstItem, accWeight) => isFirstItem ? order.Creator.Code : ""}, // Col B
            {"运单号", (order, item, isFirstItem, accWeight) => isFirstItem ? order.OrderNumber : ""}, // Col C
            {"国内运单号", (order, item, isFirstItem, accWeight) => isFirstItem ? order.DomesticNumber : ""}, // Col D
            {"hscode", (order, item, isFirstItem, accWeight) => ""}, // Col E
            {"名称", (order, item, isFirstItem, accWeight) => item.Name}, // Col F
            {"材质", (order, item, isFirstItem, accWeight) => item.Material}, // Col G
            {"数量", (order, item, isFirstItem, accWeight) => item.Quantity.ToString()}, // Col H
            {"申报价值", (order, item, isFirstItem, accWeight) => item.ClaimPrice.ToString("0.00")}, // Col I
            {"长*宽*高 - 实重", (order, item, isFirstItem, accWeight) => isFirstItem ? GetBaggageInfos(order) : ""}, // Col J
            {"体积", (order, item, isFirstItem, accWeight) => isFirstItem ? order.TotalVolume?.ToString("0.00") : ""}, // Col K
            {"箱数", (order, item, isFirstItem, accWeight) => isFirstItem ? order.Baggages.Count().ToString() : ""}, // Col L
            {"托盘批次", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col M
            {"航次", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col N
            {"预计到货时间", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col O
            {"取货点", (order, item, isFirstItem, accWeight) => isFirstItem ? order.PickUpLocation.Name : ""}, // Col P
            {"真实姓名", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col Q
            {"联系地址", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col R
            {"城市", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col S
            {"国家", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col T
            {"邮编", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col U
            {"电话", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col V
            {"派送地址", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col W
            {"派送城市", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : "" }, // Col X
            {"派送国家", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col Y
            {"派送邮编", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col Z
            {"派送电话", (order, item, isFirstItem, accWeight) => isFirstItem ? "" : ""}, // Col AA
            {"收费重量", (order, item, isFirstItem, accWeight) => isFirstItem ? order.WeightKg.ToString("0.00") : ""}, // Col AB
            {"运费", (order, item, isFirstItem, accWeight) => isFirstItem ? order.ItemCost.ToString("0.00") : ""}, // Col AC
            {"保险", (order, item, isFirstItem, accWeight) => isFirstItem ? order.InsuranceCost?.ToString("0.00") : ""}, // Col AD
            {"仓储费", (order, item, isFirstItem, accWeight) => isFirstItem ? order.WarehouseCost.ToString("0.00") : ""}, // Col AE
            {"总费用", (order, item, isFirstItem, accWeight) => isFirstItem ? order.ShippingCost.ToString("0.00") : ""}, // Col AF
        };


        public static decimal CalculateVolumeWeight(OrderBaggageEntity baggage)
        {
            if (baggage == null)
            {
                return 0;
            }

            return Math.Round(100 * baggage.Length * baggage.Width * baggage.Height / 5000) / 100;
        }

        public XLWorkbook Export(IEnumerable<OrderEntity> orders)
        {
            Console.WriteLine($"Export: {orders.Count()}");
            orders = orders.OrderBy(o => o.Creator.BelongsToId).ThenBy(o => o.Creator.Code);

            var dt = new DataTable();
            foreach (var key in _columnMapper.Keys)
            {
                dt.Columns.Add(key);
            }

            var lastRecipient = orders.FirstOrDefault()?.Creator.Id;
            decimal accWeight = 0;
            var currentRow = 2;
            var rowsToMerge = new List<(int from, int to)>();
            foreach (var order in orders)
            {
                if (!order.Items.Any())
                {
                    order.Items = new List<OrderItemEntity>() {new OrderItemEntity()};
                }
                if (order.Items.Count() > 1)
                {
                    rowsToMerge.Add((currentRow, currentRow + order.Items.Count() - 1));
                }

                if (order.Creator.Id != lastRecipient)
                {
                    dt.Rows.Add(dt.NewRow());
                    currentRow++;
                    dt.Rows.Add(dt.NewRow());
                    currentRow++;
                    lastRecipient = order.Creator.Id;
                }

                var isFirstItem = true;
                accWeight += order.WeightKg;

                Console.WriteLine($"{order.OrderNumber}, ${order.Items.Count()}");
                foreach (var orderItem in order.Items)
                {
                    var row = dt.NewRow();

                    foreach (var key in _columnMapper.Keys)
                    {
                        row[key] = _columnMapper[key](order, orderItem, isFirstItem, accWeight);
                    }
                    dt.Rows.Add(row);
                    currentRow++;

                    isFirstItem = false;
                }
            }

            var wb = new XLWorkbook();
            //wb.Worksheets.Add(dt, "运单信息");
            var ws = wb.Worksheets.Add("运单信息");
            //ws.Cell(1, 1).InsertData(_columnMapper.Keys);
            for (int c = 0; c < dt.Columns.Count; c++)
            {
                ws.Cell(1, c + 1).Value = dt.Columns[c].ColumnName;
            }
            ws.Cell(2, 1).InsertData(dt.AsEnumerable());
            //ws.Cell(1, 1).InsertTable(dt);
            foreach (var (from, to) in rowsToMerge)
            {
                //ws.Range($"A{from}:A{to}").Merge();
                //ws.Cell($"A{from}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;                    
                //ws.Range($"B{from}:B{to}").Merge();
                //ws.Cell($"B{from}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;                    
                foreach (var c in ColumnsToMerge)
                {
                    ws.Range($"{c}{from}:{c}{to}").Merge();
                    ws.Cell($"{c}{from}").Style.Alignment.Vertical = XLAlignmentVerticalValues.Top;                    
                }
            }
            return wb;
        }
    }
}
