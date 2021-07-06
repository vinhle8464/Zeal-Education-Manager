using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Areas.Admin.Service;
using ProjectSemester3.Models;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("coursesubjects")]
    [Route("admin/coursesubjects")]
    public class CourseSubjectsController : Controller
    {
        private readonly ICourseSubjectService courseSubjectService;
        private readonly IAccountService accountService;
        private readonly DatabaseContext context;

        public CourseSubjectsController(ICourseSubjectService _courseSubjectService, IAccountService _accountService, DatabaseContext _context)
        {
            this.courseSubjectService = _courseSubjectService;
            this.accountService = _accountService;
            this.context = _context;
        }



        // GET: Admin/CourseSubjects
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.courseSubjectes = await courseSubjectService.FindAll();
                ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseName");
                ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName");

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create([Bind("CourseId,SubjectId")] CourseSubject courseSubject)
        {
            if (ModelState.IsValid)
            {
                if (await courseSubjectService.Create(courseSubject) == 0)
                {
                    TempData["msg"] = "<script>alert('Course-Subject has already existed!');</script>";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                    return RedirectToAction(nameof(Index));
                }
            }

            ViewBag.courseSubjectes = await courseSubjectService.FindAll();

            ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseName", courseSubject.CourseId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectName", "SubjectId", courseSubject.SubjectId);

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/CourseSubjects/Edit/5
        public async Task<IActionResult> Edit(string courseid, string subjectid)
        {
            var courseSubject = await courseSubjectService.Find(courseid, subjectid);
         
            ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseName", courseSubject.CourseId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", courseSubject.SubjectId);

            return View("edit", courseSubject);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(CourseSubject courseSubject)
        {
            if (ModelState.IsValid)
            {
                await courseSubjectService.Update(courseSubject);
                TempData["msg"] = "<script>alert('Successfully!');</script>";

                return RedirectToAction(nameof(Index));
            }

            ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseName", courseSubject.CourseId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", courseSubject.SubjectId);

            return View(courseSubject);
        }

    }
}
