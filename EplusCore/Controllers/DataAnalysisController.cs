using ClosedXML.Excel;
using ClosedXML.Extensions;
using Common;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Exporters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Models.ApiRequest;
using WebUI.Models.DataTableRequest;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class DataAnalysisController : Controller
    {
        private readonly EplusDbContext _context;
        private readonly IFileExportService _fileExportService;

        private const string ORDER_EXPORT_SQL = @"
SELECT t.Id, DATE_FORMAT(t.DateCreated, '%Y-%m-%d') DateCreated, u.OrderStartNumber, t.OrderNumber, r.Name RouteName, t.ShippingCost, p.name PickUpLocation, CONCAT(a.OrderStartNumber, ' ', ac.Name) Agent, a.OrderStartNumber AgentId, i.Category ItemType, t.LoadDeliveryBatchName, 0.0 PayCommission
FROM transport_order t
JOIN user u ON t.CreatedById=u.Id
JOIN user a ON u.BelongsToId=a.Id JOIN customer ac ON a.CustomerId=ac.Id
JOIN route r ON t.RouteId= r.Id
JOIN pick_up_location p ON t.pick_up_location_id= p.Id
LEFT JOIN vw_order_item i ON t.Id=i.OrderId
";

        public DataAnalysisController(EplusDbContext context, IFileExportService fileExportService)
        {
            _context = context;
            _fileExportService = fileExportService;
        }

        public IActionResult DepositInventory()
        {
            return View();
        }

        public IActionResult OrderExport()
        {
            DataAnalysisOrderExportViewModel model = new DataAnalysisOrderExportViewModel
            {
                Entities = new List<DataAnalysisOrderEntity>(),
                RecipientSummary = new List<DataAnalysisOrderSummary>(),
                AgentSummary = new List<DataAnalysisOrderSummary>(),
                LocationSummary = new List<DataAnalysisOrderSummary>()
            };
            return View(model);
        }

        public async Task<IActionResult> DownloadOrders([FromBody] PreviewOrderExportRequest request)
        {
            try
            {
                (List<DataAnalysisOrderEntity> entities, List<DataAnalysisOrderSummary> recipients, List<DataAnalysisOrderSummary> agents, List<DataAnalysisOrderSummary> locations) = await GetOrderEntityAsync(request, true);

                var exporter = new DataAnalysisOrderExporter(recipients, agents, locations);
                var wb = exporter.Export(entities);
                Response.Headers.Add("Set-Cookie", "fileDownload=true; path=/");
                return wb.Deliver("Orders" + ".xlsx");
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = nameof(DownloadOrders),
                    Text = e.Message
                }));
            }
        }

        [HttpPost]
        public async Task<IActionResult> PreviewOrderExport([FromBody] PreviewOrderExportRequest request)
        {
            try
            {
                (List<DataAnalysisOrderEntity> entities, List<DataAnalysisOrderSummary> recipients, List<DataAnalysisOrderSummary> agents, List<DataAnalysisOrderSummary> locations) = await GetOrderEntityAsync(request, false);
                DataAnalysisOrderExportViewModel model = new DataAnalysisOrderExportViewModel
                {
                    Entities = entities,
                    RecipientSummary = recipients,
                    AgentSummary = agents,
                    LocationSummary = locations
                };
                return new JsonResult(model);
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = nameof(PreviewOrderExport),
                    Text = e.Message
                }));
            }
        }

        public async Task<IActionResult> LoadSelfDeposits(DataTableRequestModel requestModel)
        {
            try
            {
                var entities = new List<DepositSummaryEntity>();
                var total = 0;
                using (var conn = _context.Database.GetDbConnection())
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = $@"
SELECT DATE_FORMAT(Date, '%Y-%m-%d') Date, SUM(Amount) Amount FROM
(SELECT Amount, DATE(Date) Date FROM balance_history WHERE type IN (5,6) AND FromUserId=ToUserId) t
GROUP BY Date
ORDER BY Date DESC
LIMIT {requestModel.Start}, {requestModel.Length}
                        ";
                        var result = await command.ExecuteReaderAsync();
                        
                        while (result.Read())
                        {
                            if (result[0] != DBNull.Value)
                            {
                                entities.Add(new DepositSummaryEntity
                                {
                                    Date = result.GetString(0),
                                    Amount = result.GetDecimal(1)
                                });
                            }
                        }
                        await result.CloseAsync();

                        command.CommandText = $@"
SELECT COUNT(1) FROM
(
SELECT DATE_FORMAT(Date, '%Y-%m-%d') Date, SUM(Amount) Amount FROM
(SELECT Amount, DATE(Date) Date FROM balance_history WHERE type IN (5,6) AND FromUserId=ToUserId) t1
GROUP BY Date
) t2
                        ";
                        total = Convert.ToInt32(await command.ExecuteScalarAsync() as long?);
                    }
                    conn.Close();
                }
                var data = new PagedResult<DepositSummaryEntity>()
                {
                    Total = total,
                    Items = entities
                };

                return Json(new { draw = requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = data.Items });
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = nameof(LoadSelfDeposits),
                    Text = e.Message
                }));
            }
        }

        public async Task<IActionResult> LoadOtherDeposits(DataTableRequestModel requestModel)
        {
            try
            {
                var entities = new List<DepositSummaryEntity>();
                var total = 0;
                using (var conn = _context.Database.GetDbConnection())
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = $@"
SELECT DATE_FORMAT(Date, '%Y-%m-%d') Date, SUM(Amount) Amount FROM
(SELECT DATE(max(Date)) Date, max(Amount) Amount from balance_history where type in (5,6) and FromUserId <> ToUserId and transaction_guid is not null group by transaction_guid) t
GROUP BY Date
ORDER BY DATE DESC
LIMIT {requestModel.Start}, {requestModel.Length}

                        ";
                        var result = await command.ExecuteReaderAsync();

                        while (result.Read())
                        {
                            if (result[0] != DBNull.Value)
                            {
                                entities.Add(new DepositSummaryEntity
                                {
                                    Date = result.GetString(0),
                                    Amount = result.GetDecimal(1)
                                });
                            }
                        }
                        await result.CloseAsync();

                        command.CommandText = $@"
SELECT COUNT(1) FROM
(
SELECT DATE_FORMAT(Date, '%Y-%m-%d') Date, SUM(Amount) Amount FROM
(SELECT DATE(max(Date)) Date, max(Amount) Amount from balance_history where type in (5,6) and FromUserId <> ToUserId and transaction_guid is not null group by transaction_guid) t1
GROUP BY Date
) t2
                        ";
                        total = Convert.ToInt32(await command.ExecuteScalarAsync() as long?);
                    }
                    conn.Close();
                }
                var data = new PagedResult<DepositSummaryEntity>()
                {
                    Total = total,
                    Items = entities
                };

                return Json(new { draw = requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = data.Items });
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = nameof(LoadOtherDeposits),
                    Text = e.Message
                }));
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoadOtherDepositsByDate(string date)
        {
            try
            {
                var entities = new List<OtherDepositDetailEntity>();
                using (var conn = _context.Database.GetDbConnection())
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = $@"
SELECT Date, Amount, u1.OrderStartNumber Sender, u2.OrderStartNumber Recipient, Method, FromUserCurrentBalance - Amount SenderBalance, ToUserCurrentBalance RecipientBalance
FROM balance_history b
JOIN user u1 ON b.FromUserId=u1.Id
JOIN user u2 ON b.ToUserId=u2.Id
WHERE type IN (5,6) AND FromUserId <> ToUserId AND ToUserActualAmount > 0 AND DATE(Date) = @date
ORDER BY Date
                        ";
                        command.Parameters.Add(new MySqlParameter()
                        {
                            ParameterName = "@date",
                            Value = date
                        });
                        var result = await command.ExecuteReaderAsync();

                        while (result.Read())
                        {
                            if (result[0] != DBNull.Value)
                            {
                                entities.Add(new OtherDepositDetailEntity
                                {
                                    Date = result.GetDateTime(0),
                                    Amount = result.GetDecimal(1),
                                    Sender = result.GetString(2),
                                    Recipient = result.GetString(3),
                                    Method = result.GetString(4),
                                    SenderBalance = result.GetDecimal(5),
                                    RecipientBalance = result.GetDecimal(6)
                                });
                            }
                        }
                        await result.CloseAsync();
                    }
                    conn.Close();
                }

                return Json(new { recordsFiltered = entities.Count, recordsTotal = entities.Count, data = entities });
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = nameof(LoadOtherDepositsByDate),
                    Text = e.Message
                }));
            }
        }

        [HttpGet]
        public async Task<IActionResult> LoadSelfDepositsByDate(string date)
        {
            try
            {
                var entities = new List<SelfDepositDetailEntity>();
                using (var conn = _context.Database.GetDbConnection())
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = $@"
SELECT Date, Amount, u1.OrderStartNumber User, Method, ToUserCurrentBalance Balance
FROM balance_history b
JOIN user u1 ON b.FromUserId=u1.Id
WHERE type IN (5,6) AND FromUserId = ToUserId AND DATE(Date) = @date
ORDER BY Date
                        ";
                        command.Parameters.Add(new MySqlParameter()
                        {
                            ParameterName = "@date",
                            Value = date
                        });
                        var result = await command.ExecuteReaderAsync();

                        while (result.Read())
                        {
                            if (result[0] != DBNull.Value)
                            {
                                entities.Add(new SelfDepositDetailEntity
                                {
                                    Date = result.GetDateTime(0),
                                    Amount = result.GetDecimal(1),
                                    User = result.GetString(2),
                                    Method = result.GetString(3),
                                    Balance = result.GetDecimal(4)
                                });
                            }
                        }
                        await result.CloseAsync();
                    }
                    conn.Close();
                }

                return Json(new { recordsFiltered = entities.Count, recordsTotal = entities.Count, data = entities });
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = nameof(LoadSelfDepositsByDate),
                    Text = e.Message
                }));
            }
        }

        private static string AddFilter(DbCommand command, string[] filters, int? pageSize)
        {
            StringBuilder whereClause = new StringBuilder(" WHERE 1=1 ");
            if (filters != null)
            {
                foreach (string filter in filters)
                {
                    var parts = filter.Split(":");
                    switch (parts[0].Trim())
                    {
                        case "创建时间":
                            var values = parts[1].Split(",");
                            if (values.Length == 1)
                            {
                                whereClause.Append("AND DATE(t.DateCreated) = @date ");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = "@date",
                                    Value = values[0]
                                });
                            }
                            else if (values.Length == 2)
                            {
                                whereClause.Append("AND DATE(t.DateCreated) >= @from AND DATE(t.DateCreated) <= @to ");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = "@from",
                                    Value = values[0]
                                });
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = "@to",
                                    Value = values[1]
                                });
                            }
                            else
                            {
                                throw new Exception("创建时间只能有一个或两个值");
                            }
                            break;
                        case "用户编号":
                            whereClause.Append("AND u.OrderStartNumber IN (");
                            var userValues = parts[1].Split(",").Select(v => v.Trim()).ToArray();
                            whereClause.Append("@userId0");
                            command.Parameters.Add(new MySqlParameter()
                            {
                                ParameterName = "@userId0",
                                Value = userValues[0]
                            });
                            for (int i = 1; i < userValues.Length; i++)
                            {
                                whereClause.Append($",@userId{i}");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = $"@userId{i}",
                                    Value = userValues[i]
                                });
                            }
                            whereClause.Append(") ");
                            break;
                        case "线路":
                            whereClause.Append("AND r.Name IN (");
                            var routeValues = parts[1].Split(",").Select(v => v.Trim()).ToArray();
                            whereClause.Append("@routeName0");
                            command.Parameters.Add(new MySqlParameter()
                            {
                                ParameterName = "@routeName0",
                                Value = routeValues[0]
                            });
                            for (int i = 1; i < routeValues.Length; i++)
                            {
                                whereClause.Append($",@routeName{i}");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = $"@routeName{i}",
                                    Value = routeValues[i]
                                });
                            }
                            whereClause.Append(") ");
                            break;
                        case "运费":
                            values = parts[1].Split(",");
                            if (values.Length == 1)
                            {
                                whereClause.Append("AND ShippingCost = @cost ");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = "@cost",
                                    Value = values[0]
                                });
                            }
                            else if (values.Length == 2)
                            {
                                whereClause.Append("AND ShippingCost >= @fromCost AND ShippingCost <= @toCost ");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = "@fromCost",
                                    Value = values[0]
                                });
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = "@toCost",
                                    Value = values[1]
                                });
                            }
                            else
                            {
                                throw new Exception("运费只能有一个或两个值");
                            }
                            break;
                        case "运单取货点":
                            whereClause.Append("AND p.name IN (");
                            var locationValues = parts[1].Split(",").Select(v => v.Trim()).ToArray();
                            whereClause.Append("@location0");
                            command.Parameters.Add(new MySqlParameter()
                            {
                                ParameterName = "@location0",
                                Value = locationValues[0]
                            });
                            for (int i = 1; i < locationValues.Length; i++)
                            {
                                whereClause.Append($",@location{i}");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = $"@location{i}",
                                    Value = locationValues[i]
                                });
                            }
                            whereClause.Append(") ");
                            break;
                        case "客户代理归属":
                            whereClause.Append("AND AgentId IN (");
                            var agentValues = parts[1].Split(",").Select(v => v.Trim()).ToArray();
                            whereClause.Append("@agent0");
                            command.Parameters.Add(new MySqlParameter()
                            {
                                ParameterName = "@agent0",
                                Value = agentValues[0]
                            });
                            for (int i = 1; i < agentValues.Length; i++)
                            {
                                whereClause.Append($",@agent{i}");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = $"@agent{i}",
                                    Value = agentValues[i]
                                });
                            }
                            whereClause.Append(") ");
                            break;
                        case "物品种类":
                            break;
                        case "装车发货批次":
                            whereClause.Append("AND t.LoadDeliveryBatchName IN (");
                            var batchNames = parts[1].Split(",").Select(v => v.Trim()).ToArray();
                            whereClause.Append("@batchName0");
                            command.Parameters.Add(new MySqlParameter()
                            {
                                ParameterName = "@batchName0",
                                Value = batchNames[0]
                            });
                            for (int i = 1; i < batchNames.Length; i++)
                            {
                                whereClause.Append($",@batchName{i}");
                                command.Parameters.Add(new MySqlParameter()
                                {
                                    ParameterName = $"@batchName{i}",
                                    Value = batchNames[i]
                                });
                            }
                            whereClause.Append(") ");
                            break;
                        case "单号":
                        case "返利":
                        case "客户.单数":
                        case "代理.单数":
                        case "取货点.单数":
                            break;
                        default:
                            throw new Exception($"未知字段: {parts[0].Trim()}");
                    }
                }
            }
            if (pageSize.HasValue)
            {
                command.CommandText = $@"{ORDER_EXPORT_SQL}
{whereClause}
ORDER BY t.Id DESC
limit {pageSize}";
            }
            else
            {
                command.CommandText = $@"{ORDER_EXPORT_SQL}
{whereClause}
ORDER BY t.Id DESC";
            }
            
            return $@"{ORDER_EXPORT_SQL}
{whereClause}";
        }

        private async Task<(List<DataAnalysisOrderEntity> orders, List<DataAnalysisOrderSummary> recipientSummary, List<DataAnalysisOrderSummary> agentSummary, List<DataAnalysisOrderSummary> locationSummary)> GetOrderEntityAsync(PreviewOrderExportRequest request, bool returnOrders = false)
        {
            var entities = new List<DataAnalysisOrderEntity>();
            var summaryByRecipient = new List<DataAnalysisOrderSummary>();
            var summaryByAgent = new List<DataAnalysisOrderSummary>();
            var summaryByLocation = new List<DataAnalysisOrderSummary>();
            using (var conn = _context.Database.GetDbConnection())
            {
                conn.Open();
                var listOrderSql = "";

                if (returnOrders)
                {
                    using (var command = conn.CreateCommand())
                    {
                        AddFilter(command, request?.Filters, request?.PageSize ?? 300);
                        var result = await command.ExecuteReaderAsync();
                        while (result.Read())
                        {
                            if (result[0] != DBNull.Value)
                            {
                                entities.Add(new DataAnalysisOrderEntity
                                {
                                    OrderId = result.GetInt32(0),
                                    DateCreated = result.GetString(1),
                                    OrderStartNumber = result.GetString(2),
                                    OrderNumber = result.GetString(3),
                                    Route = result.GetString(4),
                                    ShippingCost = result.IsDBNull(5) ? null : result.GetDecimal(5),
                                    PickUpLocation = result.GetString(6),
                                    Agent = result.GetString(7),
                                    ItemType = result.IsDBNull(9) ? null : result.GetString(9),
                                    LoadDeliveryBatchName = result.IsDBNull(10) ? null : result.GetString(10),
                                    PayCommission = result.GetInt32(11)
                                });
                            }
                        }
                        await result.CloseAsync();
                    }
                }

                using (var command = conn.CreateCommand())
                {
                    listOrderSql = AddFilter(command, request?.Filters, null);
                    var havingSql = new StringBuilder("HAVING 1=1 ");
                    bool listInactiveUsers = false;
                    foreach (var f in request?.Filters ?? Array.Empty<string>())
                    {
                        var parts = f.Split(":");
                        if (parts[0].Trim() == "客户.单数")
                        {
                            var values = parts[1].Split(",");
                            if (values.Length == 1)
                            {
                                var value = int.Parse(values[0]);
                                listInactiveUsers = value == 0;
                                havingSql.Append($"AND c = @recipientOrderCount ");
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@recipientOrderCount",
                                    Value = value
                                });
                            }
                            else if (values.Length == 2)
                            {
                                havingSql.Append($"AND c >= @recipientOrderCountMin AND c <= @recipientOrderCountMax");
                                var min = int.Parse(values[0]);
                                var max = int.Parse(values[1]);
                                listInactiveUsers = (min == 0 && max == 0);
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@recipientOrderCountMin",
                                    Value = min
                                });
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@recipientOrderCountMax",
                                    Value = max
                                });
                            }
                        }
                    }
                    if (listInactiveUsers)
                    {
                        command.CommandText = $"SELECT OrderStartNumber, 0 AS OrderCount, 0.0 AS ShippingCost FROM user WHERE Role=3 AND OrderStartNumber NOT IN (SELECT OrderStartNumber FROM ({listOrderSql}) t) ORDER BY OrderStartNumber";
                    }
                    else
                    {
                        command.CommandText = $"SELECT OrderStartNumber, COUNT(1) c, SUM(ShippingCost) ShippingCost FROM ({listOrderSql}) t GROUP BY OrderStartNumber {havingSql} ORDER BY OrderStartNumber";
                    }
                    var result = await command.ExecuteReaderAsync();

                    while (result.Read())
                    {
                        if (result[0] != DBNull.Value)
                        {
                            summaryByRecipient.Add(new DataAnalysisOrderSummary
                            {
                                GroupId = result.GetString(0),
                                OrderCount = result.GetInt32(1),
                                ShippingCost = result.IsDBNull(2) ? 0 : result.GetDecimal(2),
                                PayCommission = 0
                            });
                        }
                    }
                    await result.CloseAsync();
                }

                using (var command = conn.CreateCommand())
                {
                    listOrderSql = AddFilter(command, request?.Filters, null);
                    var havingSql = new StringBuilder("HAVING 1=1 ");
                    foreach (var f in request?.Filters ?? Array.Empty<string>())
                    {
                        var parts = f.Split(":");
                        if (parts[0].Trim() == "代理.单数")
                        {
                            var values = parts[1].Split(",");
                            if (values.Length == 1)
                            {
                                havingSql.Append($"AND c = @agentOrderCount ");
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@agentOrderCount",
                                    Value = int.Parse(values[0])
                                });
                            }
                            else if (values.Length == 2)
                            {
                                havingSql.Append($"AND c >= @agentOrderCountMin AND c <= @agentOrderCountMax");
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@agentOrderCountMin",
                                    Value = int.Parse(values[0])
                                });
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@agentOrderCountMax",
                                    Value = int.Parse(values[1])
                                });
                            }
                        }
                    }
                    command.CommandText = $"SELECT Agent, COUNT(1) c, SUM(ShippingCost) ShippingCost, SUM(PayCommission) PayCommission FROM ({listOrderSql}) t GROUP BY Agent {havingSql} ORDER BY Agent";
                    var result = await command.ExecuteReaderAsync();

                    while (result.Read())
                    {
                        if (result[0] != DBNull.Value)
                        {
                            summaryByAgent.Add(new DataAnalysisOrderSummary
                            {
                                GroupId = result.GetString(0),
                                OrderCount = result.GetInt32(1),
                                ShippingCost = result.IsDBNull(2) ? 0 : result.GetDecimal(2),
                                PayCommission = result.IsDBNull(3) ? 0 : result.GetDecimal(3)
                            });
                        }
                    }
                    await result.CloseAsync();
                }

                using (var command = conn.CreateCommand())
                {
                    listOrderSql = AddFilter(command, request?.Filters, null);
                    var havingSql = new StringBuilder("HAVING 1=1 ");
                    foreach (var f in request?.Filters ?? Array.Empty<string>())
                    {
                        var parts = f.Split(":");
                        if (parts[0].Trim() == "取货点.单数")
                        {
                            var values = parts[1].Split(",");
                            if (values.Length == 1)
                            {
                                havingSql.Append($"AND c = @locationOrderCount ");
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@locationOrderCount",
                                    Value = int.Parse(values[0])
                                });
                            }
                            else if (values.Length == 2)
                            {
                                havingSql.Append($"AND c >= @locationOrderCountMin AND c <= @locationOrderCountMax");
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@locationOrderCountMin",
                                    Value = int.Parse(values[0])
                                });
                                command.Parameters.Add(new MySqlParameter
                                {
                                    ParameterName = "@locationOrderCountMax",
                                    Value = int.Parse(values[1])
                                });
                            }
                        }
                    }
                    command.CommandText = $"SELECT PickUpLocation, COUNT(1) c, SUM(ShippingCost) ShippingCost, SUM(PayCommission) PayCommission FROM ({listOrderSql}) t GROUP BY PickUpLocation {havingSql} ORDER BY PickUpLocation";
                    var result = await command.ExecuteReaderAsync();

                    while (result.Read())
                    {
                        if (result[0] != DBNull.Value)
                        {
                            summaryByLocation.Add(new DataAnalysisOrderSummary
                            {
                                GroupId = result.GetString(0),
                                OrderCount = result.GetInt32(1),
                                ShippingCost = result.IsDBNull(2) ? 0 : result.GetDecimal(2),
                                PayCommission = result.IsDBNull(3) ? 0 : result.GetDecimal(3)
                            });
                        }
                    }
                    await result.CloseAsync();
                }
                
                conn.Close();
            }
            return (entities, summaryByRecipient, summaryByAgent, summaryByLocation);
        }
    }
}
