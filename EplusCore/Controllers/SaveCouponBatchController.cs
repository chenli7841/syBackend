using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Models.ApiRequest;
using System;

namespace WebUI.Controllers
{
    [Route("CouponBatch")]
    public class SaveCouponBatchController : ControllerBase
    {
        private readonly EplusDbContext _context;

        public SaveCouponBatchController(EplusDbContext context)
        {
            _context = context;
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save(SaveCouponBatchRequest request)
        {
            try
            {
                var coupons = await _context.Coupons.Where(i => i.CouponBatchId == request.Id).ToListAsync();
                if (coupons == null)
                {
                    throw new Exception("Coupon batch does not exist.");
                }
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    var batch = await _context.CouponBatches.FirstOrDefaultAsync(b => b.Id == request.Id);
                    batch.Name = request.Name;
                }
                if (request.ValidFrom.HasValue)
                {
                    coupons.ForEach(c => c.ValidFrom = request.ValidFrom.Value);
                }
                if (request.ValidUntil.HasValue)
                {
                    coupons.ForEach(c => c.ValidUntil = request.ValidUntil.Value);
                }
                if (request.MinimumPrice.HasValue)
                {
                    coupons.ForEach(c => c.MinimumPrice = request.MinimumPrice.Value);
                }
                await _context.SaveChangesAsync();
                return new JsonResult(new { success = true });
            }
            catch (Exception e)
            {
                return new JsonResult(new MethodResult<object>(new Error
                {
                    Name = "SaveCouponBatchController.Save",
                    Text = e.Message
                }));
            }
        }
    }
}
