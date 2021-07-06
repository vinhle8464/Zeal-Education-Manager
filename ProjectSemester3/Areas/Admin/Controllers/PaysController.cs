using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("pays")]
    [Route("admin/pays")]
    public class PaysController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IAccountService accountService;
        private readonly IPaysService paysService;

        public PaysController(DatabaseContext context, IAccountService accountService, IPaysService paysService)
        {
            _context = context;
            this.accountService = accountService;
            this.paysService = paysService;
        }


        // GET: Admin/Pays
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
                ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");


                ViewBag.pays = await paysService.FindAll();
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
          
        }

        // GET: Admin/Pays/Details/5
        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pay = await paysService.FindById(id);
            if (pay == null)
            {
                return NotFound();
            }

            return View(pay);
        }

    }
}
