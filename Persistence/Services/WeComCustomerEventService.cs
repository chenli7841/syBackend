using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Common.WeCom;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Persistence.Services
{
    public class WeComCustomerEventService : IWeComCustomerEventService
    {
        private readonly EplusDbContext _context;
        public WeComCustomerEventService(EplusDbContext context) { _context = context; }

        public async Task<bool> ProcessDecryptedEventAsync(string xml, int companyId, CancellationToken cancellationToken = default)
        {
            var document = XDocument.Parse(xml);
            string Value(string name) => document.Descendants(name).FirstOrDefault()?.Value;
            if (Value("MsgType") != "event" || Value("Event") != "change_external_chat") return false;
            var chatId = Value("ChatId");
            if (Value("ChangeType") == "dismiss" && !string.IsNullOrWhiteSpace(chatId))
            {
                var binding = await _context.WeComCustomerGroupBindings.FirstOrDefaultAsync(x => x.CompanyId == companyId && x.ChatId == chatId && x.IsActive, cancellationToken);
                if (binding != null)
                {
                    binding.IsActive = false;
                    binding.UpdatedAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync(cancellationToken);
                }
            }
            return true;
        }
    }
}
