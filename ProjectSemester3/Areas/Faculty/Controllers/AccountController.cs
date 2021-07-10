using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Models;
using ProjectSemester3.Areas.Faculty.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Controllers
{
    [Area("faculty")]
    [Route("account")]
    [Route("faculty/account")]
    public class AccountController : Controller
    {

        private readonly IAccountService accountService;
        public AccountController(IAccountService _accountService)
        {
            accountService = _accountService;
        }
        [Route("list")]
        [Route("")]
        public IActionResult List()
        {
            return View(accountService.Findall());
        }
        // get: signup
        [HttpGet]
        [Route("addaccount")]
        public IActionResult Addaccount()
        {
            ViewBag.role = accountService.Selectrole();
            return View("addaccount");
        }
        // post: signup
        [HttpPost]
        [Route("addaccount")]
        public async Task<dynamic> Addaccount(IFormFile photo, Account account)
        {
            if (ModelState.IsValid)
            {
                await accountService.Signup(photo, account);
                return RedirectToAction("signin");
            }
            else
            {
                throw new InvalidCastException("error!");
            }

        }

    }
}
