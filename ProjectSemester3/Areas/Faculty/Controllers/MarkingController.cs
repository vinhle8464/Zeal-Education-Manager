using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Models;
using ProjectSemester3.Areas.Faculty.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Controllers
{
    [Area("faculty")]
    [Route("marking")]
    [Route("faculty/marking")]
    public class MarkingController : Controller
    {
        private IMarkingService markingService;
        public MarkingController(IMarkingService _markingService)
        {
            markingService = _markingService;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        { 
            ViewBag.faculty = markingService.getaccount(HttpContext.Session.GetString("username"));
            return View();
        }
        [Route("class")]
        public IActionResult Class(string facultyid)
        {
            ViewBag.faculty  = markingService.getfaculty(facultyid);
            ViewBag.classes= markingService.classes(facultyid);
            return View("Class");
        }
        [Route("student")]
        public IActionResult Student( string facultyid,string examid,string classid)
        {
           
            ViewBag.classes = markingService.getclass(classid);
            ViewBag.faculty  = markingService.getfaculty(facultyid);
            ViewBag.exams = markingService.getexam(examid);
            List<Mark> marks = markingService.students(examid);
            return View("student",marks);
        }
        [Route("subject")]
        public IActionResult Subject(string facultyid,string examid,string classid)
        {
            ViewBag.classid = classid;
            ViewBag.faculty  = markingService.getfaculty(facultyid);
            ViewBag.subjects = markingService.subjects(classid);
            return View("subject");
        }
        [Route("exam")]
        public IActionResult Exam(string facultyid,string subjectid,string classid)
        {
            
            ViewBag.classes = markingService.getclass(classid);
            ViewBag.faculty = markingService.getfaculty(facultyid);
            ViewBag.exams = markingService.exams(subjectid,classid);
            return View("exam");
        }
        
        
        [Route("edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(List<Mark> marks,string classid,string facultyid,string examid)
        {
           await markingService.update(marks);
            
            return RedirectToRoute(new {area="faculty", controller = "marking", action = "student", classid = classid,facultyid=facultyid,examid=examid });
        }
    }
}
