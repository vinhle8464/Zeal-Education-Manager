using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Areas.Faculty.Service;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("finance")]
    [Route("admin/finance")]
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
        public IActionResult Financereport(string facultyid,string courseid)
        {
            List<Account> accounts = new List<Account>();
            List<Account> accounts2 = new List<Account>();
            ViewBag.faculty = ViewBag.account = financeService.getfaculty(facultyid);
            var course = financeService.course(courseid);
            ViewBag.totalmoney = financeService.students(course).Count() * course.Fee;
            ViewBag.realmoney = context.Pays.Where(m=>m.PayStatus==true).Sum(m => m.Total);
            ViewBag.sholarshipmoney = context.Pays.Where(m => m.PayStatus == true).Sum(m => m.Discount);
            ViewBag.notpaidmoney = context.Pays.Where(m => m.PayStatus == false).Sum(m => m.Total);
            ViewBag.finemoney = context.Pays.Where(m => m.Title == "Finefee").Sum(m => m.Total);
            foreach(var studentid in context.Marks.Where(m=>m.StatusMark=="fail").Select(m => m.StudentId).Distinct().ToList())
            {
                accounts = accounts.Union(context.Accounts.Where(m => m.AccountId ==studentid).ToList()).ToList();
            }
            
            foreach (var studentid in context.Accounts.Select(m => m.AccountId).Except(context.Pays.Where(m => m.Title == "Coursefee").Distinct().Select(m => m.AccountId)).ToList())
            {
                accounts2 = accounts2.Union(context.Accounts.Where(m => m.AccountId == studentid&&m.RoleId== "role03").ToList()).ToList();
            }
            ViewBag.studentwithfinefee = accounts;
            ViewBag.studentwithoutlearningfee = accounts2;
            return View("financereport");
        }
    }
}
