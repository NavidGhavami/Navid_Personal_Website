using Application.Layer.Services.Interface;
using Domain.Layer.Dtos.MyInformation;
using Microsoft.AspNetCore.Mvc;

namespace Navid_Personal_Website.Areas.Administration.Controllers
{
    public class WebsiteController : AdminBaseController
    {
        #region Constructor

        private readonly IWebsiteServices _websiteService;

        public WebsiteController(IWebsiteServices websiteService)
        {
            _websiteService = websiteService;
        }

        #endregion

        #region My Information

        #region List

        [HttpGet("my-information-list")]
        public async Task<IActionResult> MyInformationList()
        {
            var myInfo = await _websiteService.GetAllMyInformation();
            return View(myInfo);
        }

        #endregion

        #region Create

        [HttpGet("create-my-information")]
        public IActionResult CreateMyInformation()
        {
            return View();
        }

        [HttpPost("create-my-information"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMyInformation(CreateMyInformationDto myInfo, IFormFile profileImage)
        {
            var result = await _websiteService.CreateMyInformation(myInfo, profileImage);

            switch (result)
            {
                case CreateMyInformationResult.Error:
                    TempData[ErrorMessage] = "عملیات مورد نظر با خطا مواجه شد";
                    break;
                case CreateMyInformationResult.Success:
                    TempData[SuccessMessage] = "افزودن اطلاعات من با موفقیت انجام شد";
                    return RedirectToAction("MyInformationList", "Website", new { area = "Administration" });
                default:
                    return NotFound();
            }

            return View();

        }

        #endregion

        #region Edit

        [HttpGet("edit-my-information/{myInfoId}")]
        public async Task<IActionResult> EditMyInformation(long myInfoId)
        {
            var myInfo = await _websiteService.GetMyInformationForEdit(myInfoId);
            return View(myInfo);
        }

        [HttpPost("edit-my-information/{myInfoId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMyInformation(EditMyInformationDto myInfo, IFormFile profileImage)
        {
            var result = await _websiteService.EditMyInformation(myInfo, profileImage);

            switch (result)
            {
                case EditMyInformationResult.Error:
                    TempData[ErrorMessage] = "عملیات مورد نظر با خطا مواجه شد";
                    break;
                case EditMyInformationResult.Success:
                    TempData[SuccessMessage] = "ویرایش اطلاعات من با موفقیت انجام شد";
                    return RedirectToAction("MyInformationList", "Website", new { area = "Administration" });
                default:
                    return NotFound();
            }

            return View();

        }

        #endregion

        #endregion

    }
}
