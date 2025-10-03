using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using WebUI.Models.DataTableRequest;
using Domain.Models;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class NotificationController : Controller
    {
        private readonly EplusDbContext _context;
        
        public NotificationController(EplusDbContext context)
        {
            _context = context;
        }

        public IActionResult Inventory()
        {
            return View();
        }
        
        public async Task<IActionResult> LoadSmsLogs(DataTableRequestModel requestModel)
        {
            var logs = new List<SmsLogEntity>();
            try
            {
                using (var conn = _context.Database.GetDbConnection())
                {
                    conn.Open();
                    using (var command = conn.CreateCommand())
                    {
                        command.CommandText = @"
    SELECT u.OrderStartNumber, PhoneNumber, Message, Content, Count(1) c
    FROM sms_log l
    JOIN user u on l.UserId=u.Id
    WHERE Timestamp > '2022-05-01'
    GROUP BY PhoneNumber, u.OrderStartNumber, Content 
                        ";
                        var result = await command.ExecuteReaderAsync();
                        while (result.Read())
                        {
                            if (result[0] != DBNull.Value)
                            {
                                logs.Add(new SmsLogEntity
                                {
                                    SenderOrderStartNumber = result.GetString(0),
                                    RecipientPhoneNumber = result.GetString(1),
                                    ErrorSummary = result.GetString(2),
                                    Content = result.GetString(3),
                                    Attempts = 0
                                });
                            }
                        }
                    }
                    conn.Close();
                }
                var data = new PagedResult<SmsLogEntity>()
                {
                    Total = logs.Count,
                    Items = logs
                };

                return Json(new { draw = requestModel.Draw, recordsFiltered = data.Total, recordsTotal = data.Total, data = data.Items });
            }
            catch (Exception e)
            {
                return Json(new MethodResult<object>(new Error
                {
                    Name = nameof(LoadSmsLogs),
                    Text = e.Message
                }));
            }
        }

    }
}