using Microsoft.AspNetCore.Mvc;

namespace Navid_Personal_Website.Areas.Administration.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
