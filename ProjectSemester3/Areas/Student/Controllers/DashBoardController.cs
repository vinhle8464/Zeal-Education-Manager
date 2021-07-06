using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("student")]
    [Route("dashboard")]
    [Route("student/dashboard")]
    public class DashboardController : Controller
    {
        private IAccountService accountService;

        public DashboardController(IAccountService _accountService)
        {
            accountService = _accountService;
        }

        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("role");
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("password");
            return RedirectToRoute(new { controller = "account", action = "signin" });
        }
    }
}
