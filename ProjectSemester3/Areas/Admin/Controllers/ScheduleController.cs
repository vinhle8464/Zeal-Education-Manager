using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("schedule")]
    [Route("admin/schedule")]
    public class ScheduleController : Controller
    {
        private DatabaseContext context;
        private IScheduleService scheduleService;

        public ScheduleController(DatabaseContext _context, IScheduleService _scheduleService)
        {
            context = _context;
            scheduleService = _scheduleService;
        }

        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.listClass = await scheduleService.SelectClasses();
                //ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName");
                //ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName");

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        [HttpGet]
        [Route("details")]
        public async Task<IActionResult> Detail(string classid)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {

                ViewBag.schedule = await scheduleService.SelectShedule(classid);
                ViewBag.classitem = await scheduleService.GetClass(classid);

                ViewBag.listSubject = await scheduleService.GetListSubject(classid);
                ViewData["ClassId"] = new SelectList(context.Classes.Where(c => c.ClassId == classid), "ClassId", "ClassName");
                ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName");
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
        public async Task<IActionResult> Add(Schedule schedule, int hour, int minute)
        {
            if (ModelState.IsValid)
            {
                var scheduleCurrenr = new Schedule();
                scheduleCurrenr.ClassId = schedule.ClassId;
                scheduleCurrenr.SubjectId = schedule.SubjectId;
                scheduleCurrenr.FacultyId = schedule.FacultyId;
                scheduleCurrenr.TimeDay = schedule.TimeDay;
                scheduleCurrenr.StartDate = schedule.StartDate;
                scheduleCurrenr.EndDate = schedule.EndDate;
                scheduleCurrenr.StudyDay = schedule.StudyDay;
                scheduleCurrenr.Status = true;
                //await scheduleService.Add(scheduleCurrenr);


                if (await scheduleService.Add(scheduleCurrenr) == 0)
                {
                    TempData["msg"] = "<script>alert('Schedule has already existed!');</script>";
                }
                else
                {

                    await scheduleService.CreateAttendance(schedule);
                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                }

            }
            ViewBag.schedule = await scheduleService.SelectShedule(schedule.ClassId);
            ViewBag.classitem = await scheduleService.GetClass(schedule.ClassId);
            ViewData["ClassId"] = new SelectList(context.Classes.Where(c => c.ClassId == schedule.ClassId), "ClassId", "ClassName");
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName");
            ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.Role.RoleName == "faculty"), "AccountId", "Fullname");
            return RedirectToRoute(new { controller = "schedule", action = "details", classid = schedule.ClassId});
        }
    }
}


