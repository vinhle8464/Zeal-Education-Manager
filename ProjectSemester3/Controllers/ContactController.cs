using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using ProjectSemester3.TagHelpers.MailHelper;


namespace ProjectSemester3.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        private IConfiguration configuration;
        private IContactService contactService;

        public ContactController(IConfiguration _configuration, IContactService _contactService)
        {
            configuration = _configuration;
            contactService = _contactService;
        }


        // GET: /<controller>/
        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [Route("send")]
        public IActionResult Send(string fname, string lname, string email, string subject, string message, string phone)
        {
            var s = "Fullname: " + fname + " " + lname + "<br>" + subject + "<br>" + "Email: " + email + "<br>" + "Phone number: " + phone + "<br>" + message;
            var mailHelper = new MailHelper(configuration);
            if (mailHelper.Send(email, configuration["Gmail:Username"], subject, s))
            {
                if (ModelState.IsValid)
                {
                    // devided char and number
                  

                    var mail = new Mail();
                    mail.Title = subject;
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
