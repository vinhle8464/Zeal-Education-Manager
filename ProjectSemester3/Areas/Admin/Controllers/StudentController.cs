using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectSemester3.Areas.Faculty.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("student")]
    [Route("admin/student")]
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;
        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }
        [Route("class")]
        [Route("")]
        public IActionResult Class(string facultyid)
        {
            ViewBag.faculty = ViewBag.account = studentService.getfacultyid(facultyid);
            ViewBag.classes = studentService.classes(facultyid);
            return View("class");
        }
        [Route("index")]
        
        public IActionResult Index(string facultyid, string classid)
        {
            ViewBag.faculty=ViewBag.account = studentService.getfacultyid(facultyid);
            ViewBag.students = studentService.allstudents(classid);
            return View("index");
        }
        [Route("studentanalysis")]
        public IActionResult StudentAnalysis(string facultyid,string classid, string studentid)
        {
            ViewBag.faculty=ViewBag.account = studentService.getfacultyid(studentid);

            ViewBag.subjectslist = studentService.subjects(classid).Select(m => m.SubjectName).ToArray();
            ViewBag.datasubjectslist = studentService.datasubjectlist(studentid);
            ViewBag.students = studentService.mark(studentid);
            ViewBag.fulllesson = studentService.datafulllesson(studentid);
            ViewBag.absentlessson = studentService.dataabsentlesson(studentid);
            return View("studentanalysis");
        }

    }
}
