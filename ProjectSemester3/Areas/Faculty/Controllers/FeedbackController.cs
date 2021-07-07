using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Areas.Faculty.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Controllers
{
    [Area("faculty")]
    [Route("feedback")]
    [Route("faculty/feedback")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService feedbackService;
        private Services.IAccountService accountService;
        public FeedbackController(IFeedbackService _feedbackService, Services.IAccountService _accountService)
        {
            feedbackService = _feedbackService;
            accountService = _accountService;
        }
        [Route("index")]
        [Route("")]
        public IActionResult Index(string facultyid)
        {
            ViewBag.faculty = accountService.FindID(facultyid);
            ViewBag.subjects = feedbackService.subjects(facultyid);
            return View();
        }
        [Route("evaluate")]
        public IActionResult Evaluate(string facultyid,string subjectid)
        {
            try
            {
                ViewBag.faculty = ViewBag.account = accountService.FindID(facultyid);
                ViewBag.feedback = feedbackService.feedbacks(subjectid, facultyid);
                return View("evaluate");
            }
            catch 
            {
                TempData["msg"] = "<script>alert('feedback not exists!');</script>";
                return RedirectToRoute(new { area = "faculty", controller = "feedback", action = "index", facultyid = facultyid });
            }
           
        }
    }
}
