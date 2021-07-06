using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectSemester3.Areas.Faculty.Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Controllers
{
    [Area("faculty")]
    [Route("student")]
    [Route("faculty/student")]
    public class StudentController : Controller
    {
        private readonly IStudentService studentService;
        public StudentController(IStudentService _studentService)
        {
            studentService = _studentService;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index(string facultyid, string searchfullname)
        {
            ViewBag.faculty=ViewBag.account = studentService.getfacultyid(facultyid);
            // ViewBag.students = studentService.students(facultyid, searchfullname);
            ViewBag.students = studentService.allstudents(facultyid);
            return View();
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
