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

        public async Task<IActionResult> Index(int? companyIds)
        {
            var companies = await _context.Companies.ToListAsync();
            ViewBag.Companies = companies.Select(c => _mapper.Map<CompanyEntity>(c));
            var settings = await _systemService.GetSettingsAsync();
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
                Settings = settings,
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
            var companies = await _context.Companies.ToListAsync();
            ViewBag.Companies = companies.Select(c => _mapper.Map<CompanyEntity>(c));
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
            var companies = await _context.Companies.ToListAsync();
            ViewBag.Companies = companies.Select(c => _mapper.Map<CompanyEntity>(c));
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
