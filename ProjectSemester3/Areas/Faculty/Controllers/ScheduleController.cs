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
            ViewBag.faculty=ViewBag.account = scheduleService.getfacultyid(facultyid);
            ViewBag.subjects = scheduleService.subjects(facultyid);
            return View();
        }
        [Route("facultyschedule")]
        public IActionResult facultyschedule(string facultyid,string subjectid)
        {
            ViewBag.faculty = ViewBag.account = scheduleService.getfacultyid(facultyid);
            ViewBag.schedules = scheduleService.schedules(subjectid);
            return View("facultyschedule");
        }
        [Route("class")]
        public IActionResult Class(string facultyid)
        {
            ViewBag.faculty = ViewBag.account = scheduleService.getfacultyid(facultyid);
            ViewBag.classes = scheduleService.classes(facultyid);
            return View("Class");
        }
        [Route("subject")]
        public IActionResult Subject(string facultyid, string classid)
        {
            ViewBag.classid = classid;
            ViewBag.faculty = ViewBag.account = scheduleService.getfacultyid(facultyid);
            ViewBag.subjects = scheduleService.subjectsofclass(classid);
            return View("subject");
        }
        [Route("exam")]
        public IActionResult Exam(string facultyid, string subjectid, string classid)
        {

            ViewBag.classes = scheduleService.getclass(classid);
            ViewBag.faculty = ViewBag.account = scheduleService.getfacultyid(facultyid);
            ViewBag.exams = scheduleService.exams(subjectid);
            return View("exam");
        }
        [Route("testschedule")]
        public IActionResult Testschedule(string facultyid, string examid)
        {
            ViewBag.faculty = ViewBag.account = scheduleService.getfacultyid(facultyid);
            ViewBag.testschedules = scheduleService.testSchedules(examid);
            return View("testschedule");
        }
    }
}
