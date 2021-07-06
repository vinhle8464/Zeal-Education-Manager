using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("student")]
    [Route("attendance")]
    [Route("student/attendance")]
    public class AttendanceController : Controller
    {
        private IAccountService accountService;
        private IAttendanceService attendanceService;
        private DatabaseContext context;

        public AttendanceController(IAccountService _accountService, IAttendanceService _attendanceService, DatabaseContext _context)
        {
            accountService = _accountService;
            attendanceService = _attendanceService;
            context = _context;
        }
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                var student = accountService.Find(HttpContext.Session.GetString("username"));

                ViewBag.attendances = await attendanceService.SelectForStudent(student.AccountId);
                ViewBag.subjects = await attendanceService.GetSubject(student.ClassId);

                var subjects = await attendanceService.GetSubject(student.ClassId);
                foreach (var subject in subjects)
                {
                    
                }

                return View();
            }
            return null;
        }

        [Route("details")]
        public async Task<IActionResult> Details(string subjectid)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                var student = accountService.Find(HttpContext.Session.GetString("username"));
                ViewBag.attendances = await attendanceService.GetAttendancesBySubject(subjectid, student.AccountId, student.ClassId);

                if (ModelState.IsValid)
                {
                    ViewBag.timeday = context.Schedules.SingleOrDefault(s => s.SubjectId == subjectid && s.ClassId == student.ClassId).TimeDay;

                    ViewBag.subjectname = context.Subjects.SingleOrDefault(s => s.SubjectId == subjectid).SubjectName;
                }
                
                return View("Details");
            }
            return null;
        }
    }
}
