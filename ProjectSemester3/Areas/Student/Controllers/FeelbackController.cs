using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Services;
using ProjectSemester3.TagHelpers.MailHelper;
using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectSemester3.Models;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("student")]
    [Route("feelback")]
    [Route("student/feelback")]
    public class FeelbackController : Controller
    {
        private IConfiguration configuration;
        private IAccountService accountService;
        private IMailService mailService;
        private IContactService contactService;

        public FeelbackController(IConfiguration configuration, IAccountService accountService, IMailService mailService, IContactService contactService)
        {
            this.configuration = configuration;
            this.accountService = accountService;
            this.mailService = mailService;
            this.contactService = contactService;
        }

        [Route("index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                return View();
            }
            return null;
        }

        [HttpPost]
        [Route("send")]
        public IActionResult Send(string fname, string lname, string email, string subject, string message, string phone, string classstudent)
        {
            var s = "Fullname: " + fname + " " + lname + "<br>" + "Class: " + classstudent + "<br>" + "Email: " + email + "<br>" + subject + "<br>" + "Phone number: " + phone + "<br>" + message;
            var mailHelper = new MailHelper(configuration);
            if (mailHelper.Send(email, configuration["Gmail:Username"], subject, s))
            {
                if (ModelState.IsValid)
                {
                    // devided char and number


                    var mail = new Mail();
                    mail.Title = subject + ", Class: " + classstudent;
                    mail.EmailUser = email;
                    mail.Fullname = fname + " " + lname;
                    mail.PhoneNumber = phone;
                    mail.Content = message;
                    mail.SendDate = DateTime.Now;
                    mail.ReplyDate = null;
                    mail.Check = false;
                    mail.Status = true;
                    contactService.Create(mail);
                }
                ViewBag.succ = "You send successful, thank you your idea";

                return View("Index");
            }
            else
            {
                ViewBag.msg = "Send Fail";
                return View("Index");
            }
        }
    }
}
