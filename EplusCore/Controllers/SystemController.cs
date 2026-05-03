using System;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    public class SystemController : Controller
    {
        private readonly ISystemService _systemService;

        public SystemController(ISystemService systemService)
        {
            _systemService = systemService;
        }

        public async Task<IActionResult> Index()
        {
            var settings = await _systemService.GetSettingsAsync();
            var photos = (await _systemService.ListPhotosAsync()).ToList();
            var mobilePhotos = (await _systemService.ListMobilePhotosAsync()).ToList();

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
        public async Task<JsonResult> GetPhotoUploadUrl(int photoId, string platform)
        {
            try
            {
                if (platform == "pc")
                {
                    var savedPhoto = await _systemService.GetPhotoUploadUrl(photoId);
                    return Json(new MethodResult<SystemPhotoEntity>(savedPhoto));
                } else
                {
                    var savedPhoto = await _systemService.GetMobilePhotoUploadUrl(photoId);
                    return Json(new MethodResult<SystemPhotoEntity>(savedPhoto));

                }
            }
            catch (Exception e)
            {
                return Json(new MethodResult<SystemPhotoEntity>(new Error() { Text = e.Message }));
            }
        }

        [HttpPost]
        public async Task<JsonResult> GetSystemImageUploadUrl(string propertyName)
        {
            try
            {
                var imageUploadUrl = await _systemService.GetSystemImageUploadUrl(propertyName);
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

        public async Task<IActionResult> ContactUs()
        {
            var keyValues = await _systemService.GetCompanyKeyValue(SystemContactUsViewModel.GetKeys());
            var result = SystemContactUsViewModel.FromTuples(keyValues);
            return View(result);
        }

        public async Task<IActionResult> TransportRules()
        {
            var keyValues = await _systemService.GetCompanyKeyValue(SystemContactUsViewModel.GetKeys());
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
