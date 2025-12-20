using Application.Layer.Services.Interface;
using Application.Layer.Utilities;
using Domain.Layer.Dtos.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Navid_Personal_Website.PresentationExtensions;
using System.Security.Claims;

namespace Navid_Personal_Website.Areas.Administration.Controllers
{
    public class AccountController : AdminBaseController
    {
        #region Constructor

        private readonly IUserServices _userService;


        public static string ReturnUrl { get; set; }

        public AccountController(IUserServices userService)
        {
            _userService = userService;
        }

        #endregion


        #region Register

        [AllowAnonymous]
        [HttpGet("register")]
        public async Task<IActionResult> Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        [AllowAnonymous]
        [HttpPost("register"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterUserDto register)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUser(register);
                switch (result)
                {
                    case RegisterUserResult.MobileExists:
                        TempData[ErrorMessage] = "تلفن همراه وارد شده تکراری می باشد";
                        ModelState.AddModelError("Mobile", "تلفن همراه وارد شده تکراری می باشد ");
                        break;
                    case RegisterUserResult.Success:
                        TempData[InfoMessage] = $" ثبت نام با موفقیت انجام شد.";
                        return RedirectToAction("Index", "Home", new {area = "Administration"});

                }
            }
            return View(register);
        }
        #endregion

        #region Login

        [AllowAnonymous]
        [HttpGet("login")]
        public async Task<IActionResult> Login(string returnUrl)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                return Redirect("/");
            }

            ReturnUrl = returnUrl;

            return View();
        }

        [AllowAnonymous]
        [HttpPost("login"), ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserDto login)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.GetUserForLogin(login);


                switch (result)
                {
                    case LoginUserDto.LoginUserResult.NotFound:
                        TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد";
                        break;
                    case LoginUserDto.LoginUserResult.Success:

                        var user = await _userService.GetUserByMobile(login.Mobile);
                        var claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.MobilePhone, user.Mobile),
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Role, user.RoleId.ToString()),


                        };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        var properties = new AuthenticationProperties
                        {
                            IsPersistent = login.RememberMe,
                            RedirectUri = HttpContext.Request.Query["RedirectUri"]
                        };

                        await HttpContext.SignInAsync(principal, properties);


                        TempData[SuccessMessage] = "شما با موفقیت وارد سایت شدید";

                        if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
                            return Redirect(ReturnUrl);
                        else
                            return Redirect("/");
                }
            }

            return View(login);
        }

        #endregion

        #region User List

        [Authorize("UserManagement", Roles = Roles.Administrator)]
        [HttpGet("user-list")]
        public async Task<IActionResult> UserList(FilterUserDto filter)
        {
            var users = await _userService.FilterUser(filter);

            users.Roles = await _userService.GetRoles();
            ViewBag.roles = users.Roles;

            return View(filter);
        }

        #endregion

        #region Edit User

        [Authorize("UserManagement", Roles = Roles.Administrator)]
        [HttpGet("edit-user/{userId}")]
        public async Task<IActionResult> EditUser(long userId)
        {
            var user = await _userService.GetUserForEdit(userId);
            if (user == null)
            {
                return NotFound();
            }

            user.Roles = await _userService.GetRoles();
            ViewBag.roles = user.Roles;


            return View(user);
        }

        [HttpPost("edit-user/{userId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserDto edit)
        {
            var user = await _userService.GetUserById(User.GetUserId());
            var result = await _userService.EditUser(edit);

            switch (result)
            {
                case EditUserResult.UserNotFound:
                    TempData[ErrorMessage] = "کاربر مورد نظر یافت نشد";
                    break;
                case EditUserResult.Success:
                    TempData[SuccessMessage] = "ویرایش کاربر با موفقیت انجام شد";
                    return RedirectToAction("UserList", "Account", new {area ="Administration"});
            }

            ViewBag.roles = await _userService.GetRoles();
            return View();
        }

        #endregion

        #region Role list
        [Authorize("UserManagement", Roles = Roles.Administrator)]
        [HttpGet("role-list")]
        public async Task<IActionResult> RoleList(FilterRoleDto filter)
        {
            var role = await _userService.FilterRole(filter);
            return View(filter);
        }

        #endregion

        #region Add Role

        [Authorize("UserManagement", Roles = Roles.Administrator)]
        [HttpGet("create-role")]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost("create-role"), ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateRole(CreateRoleDto role)
        {
            var result = await _userService.CreateRole(role);

            switch (result)
            {
                case CreateRoleResult.Error:
                    TempData[ErrorMessage] = "عملیات مورد نظر با خطا مواجه شد";
                    break;
                case CreateRoleResult.Success:
                    TempData[SuccessMessage] = "افزودن نقش با موفقیت انجام شد";
                    return RedirectToAction("RoleList", "Account", new { area = "Administration" });
            }

            return View();

        }

        #endregion

        #region Edit Role

        [Authorize("UserManagement", Roles = Roles.Administrator)]
        [HttpGet("edit-role/{roleId}")]
        public async Task<IActionResult> EditRole(long roleId)
        {
            var role = await _userService.GetRoleForEdit(roleId);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        [HttpPost("edit-role/{roleId}"), ValidateAntiForgeryToken]
        public async Task<IActionResult> EditRole(EditRoleDto edit)
        {
            var user = await _userService.GetUserById(User.GetUserId());
            var result = await _userService.EditRole(edit);

            switch (result)
            {
                case EditRoleResult.Error:
                    TempData[ErrorMessage] = "در ویرایش اطلاعات خطایی رخ داد";
                    break;
                case EditRoleResult.Success:
                    TempData[SuccessMessage] = "ویرایش نقش با موفقیت انجام شد";
                    return RedirectToAction("RoleList", "Account", new { area = "Administration" });
            }

            return View();
        }


        #endregion

     

        #region LogOut


        [AllowAnonymous]
        [HttpGet("log-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            TempData[WarningMessage] = "شما از سایت خارج شدید";
            return Redirect("/");
        }

        #endregion


       
    }
}
