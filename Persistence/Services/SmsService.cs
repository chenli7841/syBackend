using Domain.Services;
using System.Threading.Tasks;
using RingCentral;
using System.Linq;
using System.Collections.Generic;
using Domain.Models;
using System;
using Persistence.Data;
using AutoMapper;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistence.Utils;
using System.Net.Mail;
using System.Net;

namespace Persistence.Services
{
    public class SmsService: ISmsService
    {
        private IServiceProvider _serviceProvider;
        private IMapper _mapper;
        private const string JWT = "eyJraWQiOiI4NzYyZjU5OGQwNTk0NGRiODZiZjVjYTk3ODA0NzYwOCIsInR5cCI6IkpXVCIsImFsZyI6IlJTMjU2In0.eyJhdWQiOiJodHRwczovL3BsYXRmb3JtLnJpbmdjZW50cmFsLmNvbS9yZXN0YXBpL29hdXRoL3Rva2VuIiwic3ViIjoiMTk3OTgxMzAyNyIsImlzcyI6Imh0dHBzOi8vcGxhdGZvcm0ucmluZ2NlbnRyYWwuY29tIiwiZXhwIjozODY2NDE0NTAxLCJpYXQiOjE3MTg5MzA4NTQsImp0aSI6Im05VHlkNk5LVG55WmUyVU1PTGVfa1EifQ.NNU533Dv1pCrWZRG2SXrWZK_MCGahC_94MPqCjwTgHEUJUeb3S6514n8DdErVUDijxvxS5m0lXQbhofytgf24q-odkhvpMDmGsZdzemqR3fZ9SLvhDBzEMcEX12MLyxk9FLacA4kwxqkqt4h8ffvSRkfWvY4lA5g82fGPo7a1Cbtq9JBLsvSV5nC_V_TAKxfd48ca7WtWRHx_rfjghg5NEgLwxi1BTb_jPA66lHrr_7nFtBa3HBJkaVh4-IMO4JcxU5Ogz5MQu9IypNI3iocHC_jWBpscGOdyyztNMuk7M1GmPfBw4Mn7wLJqneyxtbytAo2Gwzfzo9p8UndY-SmNQ";

        public SmsService(IServiceProvider serviceProvider, ICacheService cacheService, IMapper mapper)
        {
            _serviceProvider = serviceProvider;
            _mapper = mapper;
        }

        public async Task<SupportUserEntity> GetSupportUserAsync(int userId)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    EplusDbContext context = scope.ServiceProvider.GetRequiredService<EplusDbContext>();
                    var supportUser = await context.SupportUsers.FirstOrDefaultAsync(u => u.UserId == userId);
                    if (supportUser == null) return null;
                    return _mapper.Map<SupportUserEntity>(supportUser);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message + ". " + e.StackTrace);
                return null;
            }
        }

        public async Task<IEnumerable<SmsUserInfo>> GetSmsUserInfosByBatchIdAsync(int batchId)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    EplusDbContext context = scope.ServiceProvider.GetRequiredService<EplusDbContext>();
                    var query = context.BatchBoxes.Where(b => b.BatchId == batchId)
                    .SelectMany(b => b.BatchBoxOrderMaps)
                    .Include(m => m.Order)
                        .ThenInclude(o => o.CreatedBy)
                        .ThenInclude(u => u.Customer)
                    .Include(m => m.Order)
                        .ThenInclude(o => o.CreatedBy)
                        .ThenInclude(u => u.BelongsToNavigation)
                        .ThenInclude(u => u.Customer)
                    .Select<BatchBoxOrderMap, SmsUserInfo>(m => new SmsUserInfo
                    {
                        MobilePhoneNumber = m.Order.CreatedBy.CanadaPhoneNumber,
                        OrderStartNumber = m.Order.CreatedBy.OrderStartNumber,
                        FullName = m.Order.CreatedBy.Customer != null ? m.Order.CreatedBy.Customer.Name : m.Order.CreatedBy.UserName,
                        Level = m.Order.CreatedBy.Level,
                        BelongsToName = m.Order.CreatedBy.BelongsToNavigation.Customer != null ? m.Order.CreatedBy.BelongsToNavigation.Customer.Name : m.Order.CreatedBy.BelongsToNavigation.UserName,
                        OrderCount = 0,
                        BatchName = m.BatchBox.Batch.Name,
                        ShippingCost = m.Order.ShippingCost ?? 0,
                        Balance = m.Order.CreatedBy.Balance
                    })
                    .GroupBy(m => new { m.OrderStartNumber, m.MobilePhoneNumber, m.BatchName, m.Balance, m.FullName, m.BelongsToName, m.Level })
                    .Select(g => new SmsUserInfo
                    {
                        OrderStartNumber = g.Key.OrderStartNumber,
                        MobilePhoneNumber = g.Key.MobilePhoneNumber,
                        OrderCount = g.Count(),
                        BatchName = g.Key.BatchName,
                        BatchId = batchId,
                        ShippingCost = g.Sum(c => c.ShippingCost),
                        Balance = g.Key.Balance,
                        FullName = g.Key.FullName,
                        BelongsToName = g.Key.BelongsToName,
                        Level = g.Key.Level
                    })
                    .Distinct()
                    .OrderBy(m => m.OrderStartNumber);
                    return await query.ToArrayAsync();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message + ". " + e.StackTrace);
                return new SmsUserInfo[0];
            }
        }

        public async Task<IEnumerable<SmsUserInfo>> GetSmsUserInfoByBelongsToAsync(string belongsToUserOrderStartNumber)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    EplusDbContext context = scope.ServiceProvider.GetRequiredService<EplusDbContext>();
                    var users = await context.Users.Include(u => u.BelongsToNavigation)
                    .Where(u => u.BelongsToNavigation.OrderStartNumber == belongsToUserOrderStartNumber && u.OrderStartNumber != belongsToUserOrderStartNumber)
                    .Include(u => u.Customer)
                    .Include(u => u.BelongsToNavigation).ThenInclude(b => b.Customer)
                    .Select<Persistence.Data.User, SmsUserInfo>(u => new SmsUserInfo
                    {
                        MobilePhoneNumber = u.CanadaPhoneNumber,
                        OrderStartNumber = u.OrderStartNumber,
                        FullName = u.Customer != null ? u.Customer.Name : u.UserName,
                        Level = u.Level,
                        BelongsToName = u.BelongsToNavigation.Customer != null ? u.BelongsToNavigation.Customer.Name : u.BelongsToNavigation.UserName,
                        OrderCount = 0,
                        BatchName = null,
                        ShippingCost = 0,
                        Balance = u.Balance
                    }).ToArrayAsync();
                    return users;
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message + ". " + e.StackTrace);
                return new SmsUserInfo[0];
            }
        }

        public async Task<SmsUserInfo> GetSmsUserInfoByUserIdAsync(int userId)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    EplusDbContext context = scope.ServiceProvider.GetRequiredService<EplusDbContext>();
                    var user = await context.Users.Where(u => u.Id == userId)
                    .Include(u => u.Customer)
                    .Include(u => u.BelongsToNavigation).ThenInclude(b => b.Customer)
                    .Select<Persistence.Data.User, SmsUserInfo>(u => new SmsUserInfo
                    {
                        MobilePhoneNumber = u.CanadaPhoneNumber,
                        OrderStartNumber = u.OrderStartNumber,
                        FullName = u.Customer != null ? u.Customer.Name : u.UserName,
                        Level = u.Level,
                        BelongsToName = u.BelongsToNavigation.Customer != null ? u.BelongsToNavigation.Customer.Name : u.BelongsToNavigation.UserName,
                        OrderCount = 0,
                        BatchName = null,
                        ShippingCost = 0,
                        Balance = u.Balance,
                        Email = u.Mailbox
                    }).ToArrayAsync();
                    if (user.Count() == 0)
                    {
                        return null;
                    }
                    return user[0];
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message + ". " + e.StackTrace);
                return null;
            }
        }
        
        public async Task<SmsUserInfo> GetSmsUserInfoByOrderIdAsync(int orderId)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    EplusDbContext context = scope.ServiceProvider.GetRequiredService<EplusDbContext>();
                    var user = await context.TransportOrders.Where(o => o.Id == orderId)
                    .Include(o => o.CreatedBy)
                    .ThenInclude(u => u.Customer)
                    .Select<TransportOrder, SmsUserInfo>(o => new SmsUserInfo
                    {
                        MobilePhoneNumber = o.CreatedBy.CanadaPhoneNumber,
                        OrderStartNumber = o.CreatedBy.OrderStartNumber,
                        FullName = o.CreatedBy.Customer != null ? o.CreatedBy.Customer.Name : o.CreatedBy.UserName,
                        Level = o.CreatedBy.Level,
                        BelongsToName = o.CreatedBy.BelongsTo,
                        OrderCount = 0,
                        BatchName = null,
                        ShippingCost = o.ShippingCost ?? 0,
                        Balance = o.CreatedBy.Balance
                    }).ToArrayAsync();
                    if (user.Count() == 0)
                    {
                        return null;
                    }
                    return user[0];
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message + ". " + e.StackTrace);
                return null;
            }
        }

        public async Task<bool> SendAsync(IEnumerable<SmsRequest> requests, int userId, int? batchId = null)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                EplusDbContext context = scope.ServiceProvider.GetRequiredService<EplusDbContext>();
                ICacheService cacheService = scope.ServiceProvider.GetRequiredService<ICacheService>();
                ILogService logService = scope.ServiceProvider.GetRequiredService<ILogService>();
                var requestsArray = requests.ToArray();
                var cacheEntity = cacheService.getRingCentralClient(userId);
                if (cacheEntity == null)
                {

                    var credentials = cacheService.getRingCentralCredentials();
                    if (credentials == null)
                    {
                        credentials = context.RingCentralCredentials.ToArray().Select(_mapper.Map<RingCentralCredentialEntity>).ToArray();
                        cacheService.setRingCentralCredentials(credentials);
                    }
                    var cred = credentials.FirstOrDefault(c => c.UserId == userId);
                    if (cred == null) return false;
                    var client = new RestClient(cred.ClientID, cred.ClientSecret);
                    await client.Authorize(JWT);
                    cacheEntity = new RingCentralCacheEntity
                    {
                        Client = client,
                        FromNumber = cred.FromNumber
                    };
                    cacheService.setRingCentralClient(userId, cacheEntity);
                }


                for(int i = 0; i < requestsArray.Length; i++)
                {
                    var u = requestsArray[i];
                    var retry = 5;
                    while (retry > 0)
                    {
                        try
                        {
                            if (string.IsNullOrWhiteSpace(u.MobilePhoneNumber))
                            {
                                Console.WriteLine("User " + i + ": " + u.OrderStartNumber + " has empty number.");
                                continue;
                            }
                            var contactResp = await cacheEntity.Client.Restapi().Account("~").Extension().AddressBook().Contact().List(new ListContactsParameters
                            {
                                startsWith = u.OrderStartNumber
                            });
                            var r = contactResp.records.FirstOrDefault(r => r.firstName == u.OrderStartNumber);
                            if (r == null)
                            {
                                // Insert this user
                                await cacheEntity.Client.Restapi().Account("~").Extension().AddressBook().Contact().Post(new PersonalContactRequest
                                {
                                    firstName = u.OrderStartNumber,
                                    lastName = u.FullName,
                                    company = u.BelongsTo,
                                    jobTitle = u.Level.ToString(),
                                    mobilePhone = u.MobilePhoneNumber
                                });
                                Console.WriteLine("Inserted user. " + i + " OrderStartNumber: " + u.OrderStartNumber + ", " + 
                                    "MobilePhone: " + u.MobilePhoneNumber + ", " + 
                                    "FullName: " + u.FullName + ", " +
                                    "BelongsTo: " + u.BelongsTo + ", " +
                                    "Level: " + u.Level.ToString());
                            }
                            else
                            {
                                if (
                                    !(r.mobilePhone ?? r.businessPhone).Contains(u.MobilePhoneNumber) ||
                                    r.lastName != u.FullName ||
                                    r.company != u.BelongsTo ||
                                    r.jobTitle != u.Level.ToString()
                                )
                                {
                                    // Update the phone number
                                    await cacheEntity.Client.Restapi().Account("~").Extension().AddressBook().Contact(r.id.Value.ToString()).Put(new PersonalContactRequest
                                    {
                                        firstName = u.OrderStartNumber,
                                        mobilePhone = u.MobilePhoneNumber,
                                        company = u.BelongsTo,
                                        jobTitle = u.Level.ToString(),
                                        lastName = u.FullName
                                    });
                                }
                            }
                            await cacheEntity.Client.Restapi().Account().Extension().Sms().Post(new CreateSMSMessage
                            {
                                from = new MessageStoreCallerInfoRequest { phoneNumber = cacheEntity.FromNumber },
                                text = u.Message,
                                to = new MessageStoreCallerInfoRequest[] { new MessageStoreCallerInfoRequest { phoneNumber = u.MobilePhoneNumber }}
                            });
                            Console.WriteLine("Sent: " + u.OrderStartNumber + ", " + u.MobilePhoneNumber);
                            retry = 0;
                            System.Threading.Thread.Sleep(6000);
                        } catch (Exception e)
                        {
                            await logService.SaveSMSLog(batchId, userId, u.MobilePhoneNumber + " | " + e.Message + " | " + e.StackTrace, u.MobilePhoneNumber, u.Message);
                            if (e.Message.Contains("StatusCode: 401"))
                            {
                                var credentials = cacheService.getRingCentralCredentials();
                                if (credentials == null)
                                {
                                    credentials = context.RingCentralCredentials.ToArray().Select(_mapper.Map<RingCentralCredentialEntity>).ToArray();
                                    cacheService.setRingCentralCredentials(credentials);
                                }
                                var cred = credentials.FirstOrDefault(c => c.UserId == userId);
                                if (cred == null) return false;
                                await cacheEntity.Client.Authorize(cred.UserName, cred.Extension, cred.Password);
                            }
                            retry--;
                            System.Threading.Thread.Sleep(90000);
                        }
                    }
                }
            }
            return true;
        }

        public async Task SendSmsAndEmailAsync(int userId, int batchId, string customMessage, string pickUpLocation, string pickUpTime)
        {
            using IServiceScope scope = _serviceProvider.CreateScope();
            EplusDbContext context = scope.ServiceProvider.GetRequiredService<EplusDbContext>();
            var message = string.IsNullOrWhiteSpace(customMessage) ? MessageUtils.BatchNotificationNewMessage : customMessage;
            var batch = await context.Batches
                .Include(b => b.PickUpLocation).ThenInclude(p => p.BelongsTo)
                .FirstOrDefaultAsync(b => b.Id == batchId);
            var messageRequestData = context.EmailDatas
                .Include(e => e.Sender)
                .Include(e => e.Recipient)
                    .ThenInclude(u => u.BelongsToNavigation)
                    .ThenInclude(u => u.Customer)
                .Include(e => e.Recipient)
                    .ThenInclude(u => u.Customer)
                .Include(e => e.Order).ThenInclude(o => o.PickUpLocation)
                .Where(e => e.SenderUserId == userId && e.DateSentSms == null && e.BatchId == batchId)
                .Select(e => new EmailData
                {
                    Id = e.Id,
                    Recipient = e.Recipient,
                    DateSentSms = e.DateSentSms,
                    DateSent = e.DateSent,
                    Order = e.Order
                })
                .ToList();
            var messageRequests = messageRequestData.GroupBy(e => new { e.Recipient }).Select(g => new SmsRequest
            {
                EmailDataIds = g.ToArray().Select(e => e.Id),
                Level = g.Key.Recipient.Level,
                Message = MessageUtils.BatchNotificationNewMessage,
                EmailMessage = MessageUtils.GetBatchNotificationEmailBody(
                    batch.Name,
                    g.Key.Recipient.Balance,
                    pickUpLocation,
                    pickUpTime,
                    batch.PickUpLocation.BelongsTo.CanadaPhoneNumber,
                    g.ToList().Select(o => o.Order).ToList()),
                MobilePhoneNumber = g.Key.Recipient.CanadaPhoneNumber,
                Email = g.Key.Recipient.Mailbox,
                OrderStartNumber = g.Key.Recipient.OrderStartNumber,
                BelongsTo = g.Key.Recipient.BelongsToNavigation.Customer.Name ?? g.Key.Recipient.BelongsToNavigation.UserName,
                FullName = g.Key.Recipient.Customer.Name ?? g.Key.Recipient.UserName,
                DateSentSms = g.ToList().FirstOrDefault(e => e.DateSentSms != null)?.DateSentSms,
                DateSentEmail = g.ToList().FirstOrDefault(e => e.DateSent != null)?.DateSent
            }).ToList();
            foreach (var req in messageRequests)
            {
                if (req.DateSentSms == null)
                {
                    var success = await SendAsync(new SmsRequest[] { req }, userId, batchId);
                    if (success)
                    {
                        await context.Database.ExecuteSqlRawAsync($"UPDATE email_data SET DateSentSms=NOW() WHERE Id IN ({string.Join(",", req.EmailDataIds)})");
                    }
                }
                if (!string.IsNullOrWhiteSpace(req.Email) && req.DateSentEmail == null)
                {
                    try
                    {
                        var smtpClient = new SmtpClient("smtp.gmail.com")
                        {
                            Port = 587,
                            Credentials = new NetworkCredential("notification.eplus@gmail.com", "dybqcagazakncdqb"),
                            EnableSsl = true
                        };

                        var mail = new MailMessage();
                        mail.From = new MailAddress("notification.eplus@gmail.com");
                        mail.To.Add(req.Email);
                        mail.Subject = MessageUtils.BatchNotificationEmailSubject;
                        mail.Body = req.EmailMessage;
                        mail.IsBodyHtml = true;
                        smtpClient.Send(mail);
                        await context.Database.ExecuteSqlRawAsync($"UPDATE email_data SET DateSent=NOW() WHERE Id IN ({string.Join(",", req.EmailDataIds)})");
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}