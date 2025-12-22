using Application.Layer.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Navid_Personal_Website.ViewComponents
{
    #region My Information

    public class MyInformationViewComponent : ViewComponent
    {
        private readonly IWebsiteServices _websiteServices;

        public MyInformationViewComponent(IWebsiteServices websiteServices)
        {
            _websiteServices = websiteServices;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var myInformation = await _websiteServices.GetAllMyInformation();
            return View("MyInformation",myInformation);
        }
    }

    #endregion

    #region My Service

    public class MyServiceViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("MyService");
        }
    }

    #endregion

    #region My Resume

    public class MyResumeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("MyResume");
        }
    }

    #endregion

    #region My Skill

    public class MySkillViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("MySkill");
        }
    }

    #endregion

    #region My Project

    public class MyProjectViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("MyProject");
        }
    }

    #endregion

    #region My Blog

    public class MyBlogViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("MyBlog");
        }
    }


    #endregion

    #region My Contact Me

    public class ContactMeViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("ContactMe");
        }
    }


    #endregion
}
