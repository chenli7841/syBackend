using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Models.DataTableRequest;

namespace WebUI.Controllers
{
    [Route("DataAnalysis")]
    public class OrderExportController : ControllerBase
    {
        private readonly EplusDbContext _context;

        public OrderExportController(EplusDbContext context)
        {
            _context = context;
        }

        [HttpPost("OrderExport/list")]
        public async Task<IActionResult> List(DataTableRequestModel requestModel)
        {
            List<DataAnalysisOrderEntity> entities = new List<DataAnalysisOrderEntity>();
            int total = 0;
            try
            {
                using (var conn = _context.Database.GetDbConnection())
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = $@"
select t.Id, DATE_FORMAT(t.DateCreated, '%Y-%m-%d') DateCreated, u.OrderStartNumber, t.OrderNumber, r.Name RouteName, t.ShippingCost, p.name PickUpLocation, '' Agent, '' ItemType, t.LoadDeliveryBatchName, 0 PayCommission
from transport_order t
join user u on t.CreatedById=u.Id
join route r on t.RouteId=r.Id
join pick_up_location p on t.pick_up_location_id=p.Id
order by t.Id desc
limit 50
                        ";
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
                                    ItemType = result.GetString(8),
                                    LoadDeliveryBatchName = result.IsDBNull(9) ? null : result.GetString(9),
                                    PayCommission = result.GetInt32(10)
                                });
                            }
                        }
                        await result.CloseAsync();
                        total = entities.Count;
                    }
                    conn.Close();
                    return new JsonResult(new { draw = requestModel.Draw, recordsFiltered = total, recordsTotal = total, data = entities });
                }
            }
            catch (Exception e)
            {
                return new JsonResult(new MethodResult<object>(new Error
                {
                    Name = nameof(List),
                    Text = e.Message
                }));
            }
        }
    }
}
