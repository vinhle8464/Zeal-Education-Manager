using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Areas.Faculty.Service;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Controllers
{
    [Area("faculty")]
    [Route("attendance")]
    [Route("faculty/attendance")]
    public class AttendanceController : Controller
    {
        private IAttendanceService attendanceService;
        private Services.IAccountService accountService;
        public AttendanceController(IAttendanceService _attendanceService, Services.IAccountService _accountService)
        {
            attendanceService = _attendanceService;
            accountService = _accountService;
        }
        [Route("subject")]
        [Route("")]
        public IActionResult Subject(string facultyid, string classid)
        {
            ViewBag.classid = classid;
            ViewBag.faculty = ViewBag.account = accountService.FindID(facultyid);
            ViewBag.subjects = attendanceService.subjects(classid);
            return View();
        }
        [Route("class")]
        public IActionResult Class(string facultyid)
        {
            ViewBag.faculty = ViewBag.account = accountService.FindID(facultyid);
            ViewBag.classes = attendanceService.classes(facultyid);
            return View("class");
        }
        [Route("attendances")]
        public IActionResult attendances(string facultyid, string subjectid, string search,string classid)
        {
            if (search == null)
            {
                ViewBag.classid = classid;
                ViewBag.subjectid = subjectid;
                ViewBag.faculty = ViewBag.account = accountService.FindID(facultyid);
                ViewBag.students = attendanceService.attendances(subjectid,classid);
                return View("attendances");
            }
            else
            {
                ViewBag.classid = classid;
                ViewBag.subjectid = subjectid;
                ViewBag.faculty = ViewBag.account = accountService.FindID(facultyid);
                ViewBag.students = attendanceService.search(subjectid,search, classid);
                ViewBag.search = search;
                return View("attendances");
            }

        }
        [Route("edit")]
        public IActionResult Edit(int attendanceid, string check, string facultyid, string subjectid,string search,string classid)
        {
            ViewBag.classid = classid;
            attendanceService.update(attendanceid, check);
            return RedirectToRoute(new { controller = "attendance", action = "attendances", facultyid = facultyid, search = search,subjectid=subjectid,classid=classid });
        }
        [Route("search")]
        public IActionResult Search(string facultyid,string search,string classid,string subjectid)
        {
           
            return RedirectToRoute(new { controller = "attendance", action = "attendances",facultyid=facultyid ,search = search,classid=classid,subjectid=subjectid });
        }

    }
}
