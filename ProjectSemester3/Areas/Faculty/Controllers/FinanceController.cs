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
    [Route("finance")]
    [Route("faculty/finance")]
    public class FinanceController : Controller
    {
        private readonly IFinanceService financeService;
        private readonly DatabaseContext context;
        public FinanceController(IFinanceService _financeService, DatabaseContext _context)
        {
            financeService = _financeService;
            context = _context;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index(string facultyid)
        {
            ViewBag.faculty = ViewBag.account = financeService.getfaculty(facultyid);
            ViewBag.courses = financeService.courses();
            return View();
        }
        [Route("financereport")]
        public IActionResult Index(string facultyid,string courseid)
        {
            ViewBag.faculty = ViewBag.account = financeService.getfaculty(facultyid);
            var course = financeService.course(courseid);
            ViewBag.totalmoney = financeService.students(course).Count() * course.Fee;
            return View();
        }
    }
}
