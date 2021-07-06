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
        public IActionResult Subject(string facultyid,string classid)
        {
            ViewBag.faculty= ViewBag.account = accountService.FindID(facultyid);
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
        public IActionResult attendances(string facultyid, string subjectid)
        {
            ViewBag.faculty = ViewBag.account = accountService.FindID(facultyid);
            ViewBag.students = attendanceService.attendances(subjectid);
            return View("attendances");
        }
        [Route("edit")] 
        public IActionResult Edit(int attendanceid,string check, string facultyid, string subjectid)
        {
            attendanceService.update(attendanceid,check);
            ViewBag.faculty = ViewBag.account = accountService.FindID(facultyid);
            ViewBag.students = attendanceService.attendances(subjectid);
            return View("attendances");
        }
        

    }
}
