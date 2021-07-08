using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectSemester3.Areas.Admin.ViewModel;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

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

        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(string scheduleid)
        {
            var schedule = await scheduleService.FindAjax(scheduleid);
            var scheduleAjax = new Schedule
            {
                RoleId = role.RoleId,
                RoleName = role.RoleName,
                Desc = role.Desc,
            };
            return new JsonResult(scheduleAjax);

        }

        //Find Faculty In Class
        [HttpGet]
        [Route("findFaculty")]
        public IActionResult FindSubject(string subjectid)
        {
            var listFaculty = scheduleService.GetListFaculty(subjectid.Trim());
            if (listFaculty == null)
            {
                return NotFound();
            }
            var result = new List<ListSubjectViewModel>();
            listFaculty.ForEach(s => result.Add(new ListSubjectViewModel
            {
                Id = s.AccountId,
                Name = s.Fullname
            }));
            return new JsonResult(result);
        }

        [Route("index")]
        public async Task<IActionResult> Index(string searchClassSchedule, int? page, int? pageSize)
        {
            
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var classes = await scheduleService.Search(searchClassSchedule);
                ViewBag.searchClassSchedule = searchClassSchedule;
              
            

                LoadPagination(classes, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        // load pagination
        public void LoadPagination(List<Class> classes, int? page, int? pageSize)
        {
            var scheduleViewModel = new ScheduleViewModel();

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
            var onePageOfProducts = classes.ToPagedList(pageNumber, pagesize);

            scheduleViewModel.PagedList = (PagedList<Class>)onePageOfProducts;

            ViewBag.listClass = scheduleViewModel;
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


