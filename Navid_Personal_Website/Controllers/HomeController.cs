using Microsoft.AspNetCore.Mvc;

namespace Navid_Personal_Website.Controllers
{
    public class HomeController : SiteBaseController
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
