
using Application.Layer.Services.Implementation;
using Application.Layer.Services.Interface;
using Domain.Layer.Dtos.Account;
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

        [HttpGet("my-information-list")]
        public async Task<IActionResult> MyInformationList()
        {
            var myInfo = await _websiteService.GetAllMyInformation();
            return View(myInfo);
        }

        #endregion
    }
}
