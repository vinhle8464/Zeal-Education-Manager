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
    [Route("testschedule")]
    [Route("student/testschedule")]
    public class TestScheduleController : Controller
    {
        private IAccountService accountService;
        private ITestScheduleService testScheduleService;

        public TestScheduleController(IAccountService _accountService, ITestScheduleService _testScheduleService)
        {
            accountService = _accountService;
            testScheduleService = _testScheduleService;
        }

        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.testschedules = await testScheduleService.SelectTestShedule(accountService.Find(HttpContext.Session.GetString("username")).ClassId);
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));

                return View();
            }
            return null;
        }

        [Route("details")]
        public async Task<IActionResult> Details(string examid)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                var account = accountService.Find(HttpContext.Session.GetString("username"));
                ViewBag.testSchedule = await testScheduleService.GetDetailTestSchedule(examid, account.ClassId);
               
                return View();
            }
            return null;
        }
    }
}
