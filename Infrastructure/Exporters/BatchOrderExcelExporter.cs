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
        private readonly Dictionary<string, Func<OrderEntity, OrderItemEntity, bool, decimal, string>> _columnMapper = new()
        {
            {"运单号", (order, item, isFirstItem, accWeight) => isFirstItem ? order.OrderNumber : ""},
            {"收件人账户编号", (order, item, isFirstItem, accWeight) => isFirstItem ? order.Creator.Code : ""},
            {"收件人", (order, item, isFirstItem, accWeight) => isFirstItem ? order.Creator.Name : ""},
            {"箱号", (order, item, isFirstItem, accWeight) => isFirstItem ? accWeight.ToString() : ""},
            {"国内单号", (order, item, isFirstItem, accWeight) => isFirstItem ? order.DomesticNumber : ""},
            {"运费", (order, item, isFirstItem, accWeight) => isFirstItem ? order.ShippingCost.ToString("0.00") : ""},
            { "收费重量", (order, item, isFirstItem, accWeight) => isFirstItem ? order.WeightKg.ToString("0.00") : "" },
            { "重量", (order, item, isFirstItem, accWeight) => isFirstItem ? order.Baggages.FirstOrDefault()?.WeightKg.ToString("0.00") : ""},
            {"体积重", (order, item, isFirstItem, accWeight) => isFirstItem ? CalculateVolumeWeight(order.Baggages.FirstOrDefault()).ToString("0.00") : ""},
            {"关税", (order, item, isFirstItem, accWeight) => isFirstItem ? (order.Items.Sum(c => c.ClaimPrice) * (decimal) 0.022).ToString("0.00") : ""},
            {"保险", (order, item, isFirstItem, accWeight) => isFirstItem ? (order.Insurance * (decimal)100.0).ToString("0.00") : ""},
            {"总价值", (order, item, isFirstItem, accWeight) => isFirstItem ? order.Items.Sum(c => c.ClaimPrice).ToString("0.00") : ""},
            {"所属群主", (order, item, isFirstItem, accWeight) => isFirstItem ? order.Creator.BelongsTo?.Name : ""},
            {"取货地点", (order, item, isFirstItem, accWeight) => isFirstItem ? order.PickUpLocation?.Name : ""},
            //{"已付款", (order, item, isFirstItem, accWeight) => isFirstItem ? (order.OrderStatus.Any(os => os.Status == Constants.ReceivedByEmployee || os.Status == Constants.Paid) ? "是" : "否") : ""},
            {"名称", (order, item, isFirstItem, accWeight) => item.Name},
            {"品牌", (order, item, isFirstItem, accWeight) => item.Brand},
            {"材质", (order, item, isFirstItem, accWeight) => item.Material},
            {"数量", (order, item, isFirstItem, accWeight) => item.Quantity.ToString()},
            {"申报价值", (order, item, isFirstItem, accWeight) => (item.ClaimPrice * (decimal)0.3).ToString("0.00")},
            {"种类", (order, item, isFirstItem, accWeight) => item.Category},
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
            orders = orders.OrderBy(o => o.Creator.BelongsToId).ThenBy(o => o.Creator.Code);

            var dt = new DataTable();
            foreach (var key in _columnMapper.Keys)
            {
                dt.Columns.Add(key);
            }

            var lastRecipient = orders.FirstOrDefault()?.Creator.Id;
            decimal accWeight = 0;
            foreach (var order in orders)
            {
                if (!order.Items.Any())
                {
                    order.Items = new List<OrderItemEntity>() {new OrderItemEntity()};
                }

                if (order.Creator.Id != lastRecipient)
                {
                    dt.Rows.Add(dt.NewRow());
                    dt.Rows.Add(dt.NewRow());
                    lastRecipient = order.Creator.Id;
                }

                var isFirstItem = true;
                accWeight += order.WeightKg;

                foreach (var orderItem in order.Items)
                {
                    var row = dt.NewRow();

                    foreach (var key in _columnMapper.Keys)
                    {
                        row[key] = _columnMapper[key](order, orderItem, isFirstItem, accWeight);
                    }
                    dt.Rows.Add(row);

                    isFirstItem = false;
                }
            }

            var wb = new XLWorkbook();
            wb.Worksheets.Add(dt, "运单信息");
            return wb;
        }
    }
}
