using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using Persistence.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class SystemController : Controller
    {
        private readonly ISystemService _systemService;
        private readonly EplusDbContext _context;
        private readonly IMapper _mapper;

        public SystemController(ISystemService systemService, EplusDbContext context, IMapper mapper)
        {
            _systemService = systemService;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            // Unfiltered on purpose: this page hosts the "锁定公司" control itself, so the
            // dropdown must always be able to see every company regardless of the current lock.
            var companies = await _context.Companies.ToListAsync();
            ViewBag.Companies = companies.Select(c => _mapper.Map<CompanyEntity>(c));
            var settings = await _systemService.GetSettingsAsync();

            var result = new SystemViewModel()
            {
                Settings = settings,
            };

            return View(result);
        }

        public async Task<IActionResult> ImageManagement(int? companyIds)
        {
            ViewBag.Companies = await _systemService.GetSelectableCompaniesAsync();
            var photos = (await _systemService.ListPhotosAsync(companyIds)).ToList();
            var mobilePhotos = (await _systemService.ListMobilePhotosAsync(companyIds)).ToList();

            while (photos.Count < 4)
            {
                photos.Add(new SystemPhotoEntity());
            }
            while (mobilePhotos.Count < 4)
            {
                mobilePhotos.Add(new SystemPhotoEntity());
            }

            var result = new SystemViewModel()
            {
                Photos = photos,
                MobilePhotos = mobilePhotos,
            };

            return View(result);
        }

        [HttpPost]
        public async Task<JsonResult> UploadPhoto(int photoId, string photoData)
        {
            try
            {
                var savedPhoto = await _systemService.AddPhotoSync(photoId, photoData);
                return Json(new MethodResult<SystemPhotoEntity>(savedPhoto));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<SystemPhotoEntity>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetPhotoUploadUrl(int photoId, string platform, int companyId)
        {
            try
            {
                if (platform == "pc")
                {
                    var savedPhoto = await _systemService.GetPhotoUploadUrl(photoId, companyId);
                    return Json(new MethodResult<SystemPhotoEntity>(savedPhoto));
                } else
                {
                    var savedPhoto = await _systemService.GetMobilePhotoUploadUrl(photoId, companyId);
                    return Json(new MethodResult<SystemPhotoEntity>(savedPhoto));

                }
            }
            catch (Exception e)
            {
                return Json(new MethodResult<SystemPhotoEntity>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetSystemImageUploadUrl(string propertyName, int? companyId)
        {
            try
            {
                if (!companyId.HasValue)
                {
                    throw new Exception("Need company Id");
                }
                var imageUploadUrl = await _systemService.GetSystemImageUploadUrl(propertyName, companyId.Value);
                return Json(new MethodResult<string>(imageUploadUrl));
            }
            catch (Exception e)
            {
                return Json(new MethodResult<SystemPhotoEntity>(new Error() { Text = e.Message }));
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(SystemSettingsEntity model)
        {
            await _systemService.UpdateSettingsAsync(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ContactUs(int? companyIds)
        {
            ViewBag.Companies = await _systemService.GetSelectableCompaniesAsync();
            var lockedCompanyId = (await _systemService.GetSettingsAsync()).LockedCompanyId;
            if (lockedCompanyId.HasValue)
            {
                companyIds = lockedCompanyId;
            }
            if (!companyIds.HasValue)
            {
                return View(new SystemContactUsViewModel());
            }
            var keyValues = await _systemService.GetCompanyKeyValue(SystemContactUsViewModel.GetKeys(), companyIds.Value);
            var result = SystemContactUsViewModel.FromTuples(keyValues);
            return View(result);
        }

        public async Task<IActionResult> TransportRules(int? companyIds)
        {
            ViewBag.Companies = await _systemService.GetSelectableCompaniesAsync();
            var lockedCompanyId = (await _systemService.GetSettingsAsync()).LockedCompanyId;
            if (lockedCompanyId.HasValue)
            {
                companyIds = lockedCompanyId;
            }
            if (!companyIds.HasValue)
            {
                return View(new SystemContactUsViewModel());
            }
            var keyValues = await _systemService.GetCompanyKeyValue(SystemContactUsViewModel.GetKeys(), companyIds.Value);
            var result = SystemContactUsViewModel.FromTuples(keyValues);
            return View(result);
        }

        public async Task<IActionResult> SaveContactUs(SystemContactUsViewModel model)
        {
            await _systemService.UpdateCompanyKeyValue(model.ToKeyValuePairs());
            return RedirectToAction(nameof(ContactUs));
        }
    }
}
