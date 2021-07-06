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
    [Area("Admin")]
    [Route("enquiries")]
    [Route("admin/enquiries")]
    public class EnquiriesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IEnquiryService enquiryService;
        private readonly IAccountService accountService;

        public EnquiriesController(DatabaseContext context, IEnquiryService enquiryService, IAccountService accountService)
        {
            _context = context;
            this.enquiryService = enquiryService;
            this.accountService = accountService;
        }

        [Route("index")]
        // GET: Admin/Enquiries
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.enquiries = enquiryService.FindAll();
                return View();

            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(Enquiry enquiry)
        {
            if (ModelState.IsValid)
            {
                var currentEnquiry = new Enquiry();
                currentEnquiry.Title = enquiry.Title.Trim();
                currentEnquiry.Answer = enquiry.Answer.Trim();
                currentEnquiry.Status = true;
                enquiryService.Create(currentEnquiry);
                ViewBag.msg = "Create Successfull";
                return RedirectToAction("Index");
            }
            ViewBag.msg = "Create fail";
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("details")]
        public IActionResult Detail(int enquiryid)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.enquiry = enquiryService.Find(enquiryid);
                return View("detail");

            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
            
        }
    }
}
