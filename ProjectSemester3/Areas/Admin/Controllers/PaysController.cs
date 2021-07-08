using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Areas.Admin.ViewModel;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using X.PagedList;

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
        public IActionResult Index(string searchPay, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
                ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");

                var pays = paysService.Search(searchPay);
                ViewBag.searchPay = searchPay;
                LoadPagination(pays, page, pageSize);
                
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        // load pagination
        public void LoadPagination(List<Pay> pays, int? page, int? pageSize)
        {
            var payViewModel = new PayViewModel();

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
            };
            int pagesize = (pageSize ?? 5);
            ViewBag.psize = pagesize;

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = pays.ToPagedList(pageNumber, pagesize);

            payViewModel.PagedList = (PagedList<Pay>)onePageOfProducts;

            ViewBag.pays = payViewModel;
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
