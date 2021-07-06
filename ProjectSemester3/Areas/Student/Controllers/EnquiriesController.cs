using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("enquiries")]
    [Route("student/enquiries")]
    public class EnquiriesController : Controller
    {
        private readonly IEnquiryService enquiryService;
        private readonly IAccountService accountService;

        public EnquiriesController(IEnquiryService enquiryService, IAccountService accountService)
        {
            this.enquiryService = enquiryService;
            this.accountService = accountService;
        }
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
    }
}
