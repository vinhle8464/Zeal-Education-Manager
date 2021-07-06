using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectSemester3.Models;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("testschedules")]
    [Route("admin/testschedules")]
    public class TestSchedulesController : Controller
    {
        private DatabaseContext context;
        private ITestScheduleService testScheduleService;
        private IAccountService accountService;

        public TestSchedulesController(DatabaseContext context, ITestScheduleService testScheduleService, IAccountService accountService)
        {
            this.context = context;
            this.testScheduleService = testScheduleService;
            this.accountService = accountService;
        }


        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.listClass = await testScheduleService.SelectClasses();
                ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName");
                ViewData["ExamId"] = new SelectList(context.Exams, "ExamId", "Title");

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        [HttpGet]
        [Route("detail")]
        public async Task<IActionResult> Detail(string classid)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.testschedules = await testScheduleService.SelectTestShedule(classid);
                ViewBag.classitem = await testScheduleService.GetClass(classid);
                ViewData["ExamId"] = new SelectList(context.Exams, "ExamId", "Title");

                ViewData["ClassId"] = new SelectList(context.Classes.Where(c => c.ClassId == classid), "ClassId", "ClassName");
                ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.Role.RoleName == "faculty"), "AccountId", "Fullname");


                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(TestSchedule testSchedule, int maxmark, int rate)
        {
            if (ModelState.IsValid)
            {

                testSchedule.Status = true;
                if (await testScheduleService.Add(testSchedule) == 0)
                {
                    TempData["msg"] = "<script>alert('Test-Schedule has already existed!');</script>";
                }
                else
                {

                    await testScheduleService.CreateMarkBySubject(testSchedule.ExamId, maxmark, rate);
                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                }

            }
            ViewBag.testschedules = await testScheduleService.SelectTestShedule(testSchedule.ClassId);
            ViewBag.classitem = await testScheduleService.GetClass(testSchedule.ClassId);
            ViewData["ExamId"] = new SelectList(context.Exams, "ExamId", "Title");

            ViewData["ClassId"] = new SelectList(context.Classes.Where(c => c.ClassId == testSchedule.ClassId), "ClassId", "ClassName");
            ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.Role.RoleName == "faculty"), "AccountId", "Fullname");

            return RedirectToRoute(new { controller = "testschedules", action = "detail", classid = testSchedule.ClassId });
        }
    }
}
