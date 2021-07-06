using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Areas.Faculty.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Controllers
{
    [Area("faculty")]
    [Route("schedule")]
    [Route("faculty/schedule")]
    public class ScheduleController : Controller
    { private readonly IScheduleService scheduleService;
        public ScheduleController(IScheduleService _scheduleService)
        {
            scheduleService = _scheduleService;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index(string facultyid)
        {
            ViewBag.faculty = scheduleService.getfacultyid(facultyid);
            ViewBag.subjects = scheduleService.subjects(facultyid);
            return View();
        }
        [Route("facultyschedule")]
        public IActionResult facultyschedule(string facultyid,string subjectid)
        {
            ViewBag.faculty = scheduleService.getfacultyid(facultyid);
            ViewBag.schedules = scheduleService.schedules(subjectid);
            return View("facultyschedule");
        }

    }
}
