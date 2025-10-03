using AutoMapper;
using Domain.Entities;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class LogService : ILogService
    {
        private readonly EplusDbContext _context;
        private IMapper _mapper;
        public LogService(EplusDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task SaveSMSLog(int? batchId, int userId, string message, string phoneNumber, string content)
        {
            await _context.SMSLogs.AddAsync(new SMSLog
            {
                BatchId = batchId,
                UserId = userId,
                Message = message,
                Content = content,
                PhoneNumber = phoneNumber,
                Timestamp = System.DateTime.Now
            });
            await _context.SaveChangesAsync();
        }

        public async Task<SMSLogEntity[]> GetSMSLogsAsync(int start, int size)
        {
            var logs = await _context.SMSLogs.OrderByDescending(l => l.Id).Skip(start).Take(size).ToListAsync();
            return logs.Select(l => _mapper.Map<SMSLogEntity>(l)).ToArray();
        }
    }
}