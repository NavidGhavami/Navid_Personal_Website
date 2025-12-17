using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Navid_Personal_Website.Areas.Admin.Controllers
{
    //[Authorize("AdminArea")]
    [Area("Administration")]
    [Route("administration")]
    public class AdminBaseController : Controller
    {
            protected string SuccessMessage = "SuccessMessage";
            protected string WarningMessage = "WarningMessage";
            protected string InfoMessage = "InfoMessage";
            protected string ErrorMessage = "ErrorMessage";
        }
    }

