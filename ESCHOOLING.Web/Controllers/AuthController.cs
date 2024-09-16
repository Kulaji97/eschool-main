using ECOMSYSTEM.Shared;
using ECOMSYSTEM.Shared.Enum;
using ECOMSYSTEM.Shared.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace ECOMSYSTEM.Web.Controllers
{
    public class AuthController : Controller
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _config;
        /// <summary>
        /// The logger
        /// </summary>
        private readonly ILogger<AuthController> _logger;
        /// <summary>
        /// The application user service
        /// </summary>
        private readonly IApplicationUserService _applicationUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="applicatioUserService">The applicatio user service.</param>
        /// <param name="config">The configuration.</param>
        public AuthController(ILogger<AuthController> logger, IApplicationUserService applicatioUserService, IConfiguration config)
        {
            _logger = logger;
            _applicationUserService = applicatioUserService;
            _config = config;
        }

        /// <summary>
        /// Registers this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers the specified application user.
        /// </summary>
        /// <param name="applicationUser">The application user.</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(ApplicationUser applicationUser)
        {
            applicationUser.CreatedDate = DateTime.Now;

            var user = _applicationUserService.RegisterUser(applicationUser);

            if (user.UserId != 0 && user.Email != null)
            {
                return Json(new
                {
                    success = true
                });
            }

            return Json(new
            {
                success = false,
            });
        }

        /// <summary>
        /// Logins this instance.
        /// </summary>
        /// <returns></returns>
        public IActionResult login()
        {
            return View();
        }

        /// <summary>
        /// User not authenticates and no auth cookie will be redirected to this action
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Forbidden/")]
        public IActionResult forbidden()
        {
            return View("forbidden");
        }

        /// <summary>Logins the specified user.</summary>
        /// <param name="linfo">The linfo.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> Login(ApplicationUser linfo)
        {
            var result = _applicationUserService.LoginUser(linfo);
            if (result.Email == null)
            {
                return Json(new
                {
                    success = false
                });
            }

            ApplicationSession.applicationUserId = result.UserId;
            var role = ((RoleEnums)result.UserType).ToString();


            var claims = new List<Claim>
        {
            new Claim("UserId", result.UserId.ToString()),
            new Claim("ImageUrl", result.ImageUrl),
            new Claim(ClaimTypes.Email, result.Email),
            new Claim(ClaimTypes.Name, result.Username),
            new Claim(ClaimTypes.Role, role),
        };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);


            

            if (role == "Admin")
            {
                return Json(new
                {
                    success = true,
                    newUrl = Url.Action("AdminHome", "Admin")
                });
            }
            else if(role == "Teacher") 
            {
                return Json(new
                {
                    success = true,
                    newUrl = Url.Action("TeacherHome", "Teacher")
                });
            }
            else if(role == "Student")
            {
                return Json(new
                {
                    success = true,
                    newUrl = Url.Action("Index", "Student")
                });
            }
            else if (role == "Doctor")
            {
                return Json(new
                {
                    success = true,
                    newUrl = Url.Action("Index", "Doctor")
                });
            }
            else
            {
                return Json(new
                {
                    success = true,
                    newUrl = Url.Action("Index", "Parent")
                });
            }

        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "Auth");
        }
    }
}
