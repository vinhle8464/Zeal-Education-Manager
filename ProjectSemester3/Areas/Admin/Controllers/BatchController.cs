using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Areas.Faculty.Service;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("faculty")]
    [Route("batch")]
    [Route("faculty/batch")]
    public class BatchController : Controller
    {
        private readonly IBatchService batchService;
        private readonly DatabaseContext context;
        public BatchController(IBatchService _batchService,DatabaseContext _context)
        {
            batchService = _batchService;
            context = _context;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index(string facultyid)
        {
            ViewBag.faculty = ViewBag.account = batchService.getfaculty(facultyid);
            ViewBag.courses = batchService.courses();

            return View();
        }
        [Route("endingbatch")]
        public IActionResult Endingbatch(string facultyid,string courseid)
        {
            int passexam=0, failexam=0, numbergraduate=0, numberfail=0;

            var course = batchService.course(courseid);
            ViewBag.coursename = course.CourseName;

            ViewBag.totalstudent = batchService.students(course).Count();
            foreach(var cls in context.Batches.Where(m => m.CourseId == course.CourseId).Select(m => m.Class).ToList())
            {
                foreach(var student in cls.Accounts.ToList())
                {
                    foreach(var mark in student.Marks.ToList())
                    {
                        if (((decimal)mark.Mark1 / mark.MaxMark) >= (decimal)0.4)
                        {
                            passexam++;
                        }
                        else
                        {
                            failexam++;
                        }
                    }
                    if ((double)(passexam / (passexam + failexam)) >= 0.4)
                    {
                        numbergraduate++;
                        passexam = 0;
                        failexam = 0;
                    }
                    else
                    {
                        numberfail++;
                        passexam = 0;
                        failexam = 0;

                    }
                }
            }
            ViewBag.numbergraduate = numbergraduate;
            ViewBag.numberfail = numberfail;
            ViewBag.totalmoney = (batchService.students(course).Count()) * course.Fee;
            ViewBag.faculty = ViewBag.account = batchService.getfaculty(facultyid);
            ViewBag.courses = batchService.courses();
            ViewBag.scholarshipstudent = batchService.scholarship(course);
            ViewBag.students = batchService.students(course);
            return View("endingbatch");
        }
        [Route("startingbatch")]
        public IActionResult Startingbatch(string facultyid, string courseid)
        { decimal? scholarshipfee=0;
            var course = batchService.course(courseid);
            ViewBag.coursename = course.CourseName;
            ViewBag.totalstudent = batchService.students(course).Count();
            ViewBag.scholarshipstudentcount = batchService.scholarship(course).Count();
            foreach(var student in batchService.scholarship(course))
            {
                foreach(var scholarship in student.ScholarshipStudents)
                {
                    scholarshipfee +=(decimal)(Convert.ToInt32(scholarship.Scholarship.Discount))/(decimal)100 * course.Fee;
                    break;
                }
            }
            ViewBag.scholarshipfee = scholarshipfee;
            ViewBag.coursefee = course.Fee;
            ViewBag.schedules = batchService.schedules(course);
            ViewBag.faculty = ViewBag.account = batchService.getfaculty(facultyid);
            ViewBag.courses = batchService.courses();
            ViewBag.students = batchService.students(course);
            return View("startingbatch");
        }
    }
}
