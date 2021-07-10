using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProjectSemester3.Areas.Student.ViewModels;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("student")]
    [Route("reports")]
    [Route("student/reports")]
    public class ReportsController : Controller
    {
        private IAccountService accountService;
        private IReportService reportService;
        private DatabaseContext context;
        private IProfileService profileService;

        public ReportsController(IAccountService accountService, IReportService reportService, DatabaseContext context, IProfileService profileService)
        {
            this.accountService = accountService;
            this.reportService = reportService;
            this.context = context;
            this.profileService = profileService;
        }

        [Route("index")]
        public IActionResult Index()
        {

            return View();
        }

        [Route("course")]
        public IActionResult Course()
        {
            var studentid = accountService.Find(HttpContext.Session.GetString("username")).AccountId;
            ViewBag.course = reportService.GetCourse(studentid);

            return View("Course");
        }

        [Route("batch")]
        public async Task<IActionResult> Batch()
        {
            var studentid = accountService.Find(HttpContext.Session.GetString("username")).AccountId;
            ViewBag.batch = reportService.GetCourse(studentid);

            var classid = accountService.Find(HttpContext.Session.GetString("username")).ClassId;
            var course = profileService.SelectCourse(studentid);

            var listSubject = await reportService.GetCourseSubject(course);
            var listStudying = new List<string>();
            var listCompleted = new List<string>();
            var listScheduled = new List<string>();
            foreach (var subject in listSubject)
            {
                var item = await reportService.GetStatusSubject(classid, studentid, subject.SubjectId);
                if (item.Contains("Studying"))
                {
                    listStudying.Add(subject.Subject.SubjectName);
                }
                else if (item.Contains("Complete"))
                {
                    listCompleted.Add(subject.Subject.SubjectName);
                }
                else
                {
                    listScheduled.Add(subject.Subject.SubjectName);
                }
            }
            ViewBag.listStudying  = listStudying;
            ViewBag.listCompleted = listCompleted;
            ViewBag.listScheduled = listScheduled;
            return View("Batch");
        }

        [Route("discount")]
        public IActionResult Discount()
        {
            var studentid = accountService.Find(HttpContext.Session.GetString("username")).AccountId;
            ViewBag.discount = reportService.GetScholarships(studentid);

            return View("Discount");
        }


        [Route("diligence")]
        public async Task<IActionResult> Diligence()
        {
            var student = accountService.Find(HttpContext.Session.GetString("username"));
            ViewBag.subjects = reportService.GetSubject(student.ClassId);
            Batch batch = await reportService.GetDateAttendance(student.ClassId);

            var from = batch.StartDate.Year;
            var to = batch.EndDate.Year;
            var list = new List<int>();
            while (to >= from)
            {
                list.Add(from);
                from++;
            }
            ViewBag.year = list;

            return View("Diligence");
        }

        [Route("detailsdiligence")]
        public IActionResult DetailsDiligence(string subjectid)
        {
            var student = accountService.Find(HttpContext.Session.GetString("username"));
            ViewBag.attendances = reportService.GetAttendances(subjectid, student.AccountId);

            ViewBag.dateoff = reportService.GetDateOff(subjectid, student.AccountId);

            ViewBag.subjectname = context.Subjects.SingleOrDefault(s => s.SubjectId == subjectid).SubjectName;

            return View("DetailsDiligence");
        }

        [Route("getdata")]
        public IActionResult GetData()
        {
            var student = accountService.Find(HttpContext.Session.GetString("username"));
            var listAttendance = reportService.GetAttendances(student.AccountId);
            List<GetAttendance> getattendance = new List<GetAttendance>();

            listAttendance.ForEach(p =>
            {
                getattendance.Add(new GetAttendance
                {
                    SubjectId = p.SubjectId,
                    SubjectName = p.Subject.SubjectName,
                    Desc = p.Subject.Desc,
                    Status = p.Subject.Status,
                    AttendanceId = p.AttendanceId,
                    ClassId = p.ClassId,
                    StudentId = p.StudentId,
                    FacultyId = p.FacultyId,
                    FacultyName = p.Faculty.Fullname,
                    Date = (DateTime)p.Date,
                    Checked = p.Checked
                });
            });

            return new JsonResult(getattendance);
        }

        [HttpGet]
        [Route("search")]
        public IActionResult Search(int year, int month)
        {
            var student = accountService.Find(HttpContext.Session.GetString("username"));
            ViewBag.subjects = reportService.GetSubject(student.ClassId);
            var from = 2010;
            var to = 2100;
            var list = new List<int>();
            while (to >= from)
            {
                list.Add(from);
                from++;
            }
            ViewBag.year = list;
            /*--------------------------------*/
            var listAttendance = reportService.GetAttendances(student.AccountId);
            List<GetAttendance> getattendance = new List<GetAttendance>();

            listAttendance.ForEach(p =>
            {
                getattendance.Add(new GetAttendance
                {
                    SubjectId = p.SubjectId,
                    SubjectName = p.Subject.SubjectName,
                    Desc = p.Subject.Desc,
                    Status = p.Subject.Status,
                    AttendanceId = p.AttendanceId,
                    ClassId = p.ClassId,
                    StudentId = p.StudentId,
                    FacultyId = p.FacultyId,
                    FacultyName = p.Faculty.Fullname,
                    Date = (DateTime)p.Date,
                    Checked = p.Checked
                });
            });

            var searchbydate = new Object();
            if (month == 0)
            {
                searchbydate = getattendance.Where(a => a.Date.Year == year).ToList();
            } else if (year == 0)
            {
                searchbydate = getattendance.Where(a => a.Date.Month == month).ToList();
            }
            else if(month != 0 || year != 0)
            {
                searchbydate = getattendance.Where(a => a.Date.Month == month && a.Date.Year == year).ToList();
            }
            return new JsonResult(searchbydate);
        }

        [Route("pay")]
        public async Task<IActionResult> Pay()
        {
            var student = accountService.Find(HttpContext.Session.GetString("username"));
            ViewBag.pays = await reportService.GetPayByStudent(student.AccountId);
            

            return View("Pay");
        }

    }
}
