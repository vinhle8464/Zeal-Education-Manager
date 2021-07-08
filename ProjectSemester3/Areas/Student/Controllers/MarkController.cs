using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("student")]
    [Route("mark")]
    [Route("student/mark")]
    public class MarkController : Controller
    {
        private IAccountService accountService;
        private IMarkService markService;

        public MarkController(IAccountService accountService, IMarkService markService)
        {
            this.accountService = accountService;
            this.markService = markService;
        }

        [Route("index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                var account = accountService.Find(HttpContext.Session.GetString("username"));
                ViewBag.marks = markService.GetMarkByStudentId(account.AccountId);
                ViewBag.getMark = markService.CheckStatus(account.AccountId);
                var checks = markService.CheckStatus(account.AccountId);
                var listFailed = new List<string>();
                var listPassed = new List<string>();
                foreach (var item in checks)
                {
                    if (item.Contains("FAILED"))
                    {
                        listFailed.Add(item);
                    }
                    else if(item.Contains("PASSED"))
                    {
                        listPassed.Add(item);
                    }
                }

                ViewBag.retestSubject = markService.GetSubjectFaid(account.AccountId);

                ViewBag.listFailed = markService.GetSubjectFaid(account.AccountId);
                ViewBag.listPassed = markService.GetPass(account.AccountId);
                return View();
            }
            return null;
        }
    }
}
