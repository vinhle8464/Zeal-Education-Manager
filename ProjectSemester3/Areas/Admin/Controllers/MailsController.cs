using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Areas.Admin.ViewModel;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("mails")]
    [Route("admin/mails")]
    public class MailsController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMailService mailService;

        public MailsController(IAccountService accountService, IMailService mailService)
        {
            this.accountService = accountService;
            this.mailService = mailService;
        }



        // GET: Admin/Mails
        [Route("index")]
        public IActionResult Index(string searchMail, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var mails = mailService.Search(searchMail);
                ViewBag.searchMail = searchMail;

                LoadPagination(mails, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }

        // load pagination
        public void LoadPagination(List<Mail> mails, int? page, int? pageSize)
        {
            var mailViewModel = new MailViewModel();

            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
            };
            int pagesize = (pageSize ?? 5);
            ViewBag.psize = pagesize;

            var pageNumber = page ?? 1; // if no page was specified in the querystring, default to the first page (1)
            var onePageOfProducts = mails.ToPagedList(pageNumber, pagesize);

            mailViewModel.PagedList = (PagedList<Mail>)onePageOfProducts;

            ViewBag.mails = mailViewModel;
        }

        // GET: Admin/Mails/Details/5
        [HttpGet]
        [Route("details")]
        public IActionResult Details(int id)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.mail = mailService.Find(id);
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
            var mail = mailService.Find(id);
            return View(mail);
        }

        [HttpPost]
        [Route("reply/{MailId}")]
        public IActionResult Reply(Mail mail)
        {
            if (ModelState.IsValid)
            {
                string title = "Reply contact!";
                var currentMail = mailService.Find(mail.MailId);
                currentMail.Title = mail.Title;
                currentMail.EmailUser = mail.EmailUser;
                currentMail.Fullname = mail.Fullname;
                currentMail.PhoneNumber = mail.PhoneNumber;
                currentMail.Content = mail.Content;
                currentMail.ReplyContent = mail.ReplyContent;
                currentMail.SendDate = mail.SendDate;
                currentMail.ReplyDate = DateTime.Now;
                currentMail.Check = true;
                currentMail.Status = mail.Status;
                mailService.InsertIntoDb(currentMail);
                mailService.Reply(mail.EmailUser, title, mail.ReplyContent);
            }
            return RedirectToAction("Index");
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit([Bind("MailId,Title,EmailUser,Fullname,PhoneNumber,Content,ReplyContent,SendDate,ReplyDate,Status")] Mail mail)
        {
           
            if (ModelState.IsValid)
            {
                try
                {
                    mail.ReplyDate = DateTime.Now;
                    mail.Status = true;
                    await mailService.Update(mail);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!mailService.Exists(mail.MailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(mail);
        }

        // GET: Admin/Mails/Delete/5
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mailService.Delete(id);
            return RedirectToAction(nameof(Index));
        }


    }
}
