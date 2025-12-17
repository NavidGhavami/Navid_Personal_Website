using Microsoft.AspNetCore.Mvc;
using Navid_Personal_Website.Areas.Admin.Controllers;

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
