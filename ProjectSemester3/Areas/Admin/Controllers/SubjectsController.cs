using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Areas.Admin.Service;
using ProjectSemester3.Models;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("subjects")]
    [Route("admin/subjects")]
    public class SubjectsController : Controller
    {
        private readonly ISubjectsService subjectsService;
        private IAccountService accountService;

        public SubjectsController(ISubjectsService subjectsService, IAccountService accountService)
        {
            this.subjectsService = subjectsService;
            this.accountService = accountService;
        }


        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(string subjectid)
        {
            var subject = await subjectsService.FindAjax(subjectid);
            var subjectAjax = new Subject
            {
                SubjectId = subject.SubjectId,
               SubjectName = subject.SubjectName,
               Desc = subject.Desc,
               Status = subject.Status
                
            };
            return new JsonResult(subjectAjax);

        }

        // Show page subject
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.subjects = await subjectsService.FindAll();

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }



        // create subject
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (ModelState.IsValid)
            {
                // devided char and number
                var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
                int num = 0;
                if (subjectsService.GetNewestId() != null)
                {
                    var match = numAlpha.Match(subjectsService.GetNewestId());
                    //var alpha = match.Groups["Alpha"].Value;
                    num = Int32.Parse(match.Groups["Numeric"].Value);

                }


                // if have no value with this id in db, we create first 
                // else it already existed in db, we create next
                if (await subjectsService.CountId() != 0)
                {
                    if (num < 9)
                    {
                        subject.SubjectId = "subject" + "0" + (num + 1);

                    }
                    else
                    {
                        subject.SubjectId = "subject" + (num + 1);

                    }
                    subject.SubjectName = subject.SubjectName.Trim();
                    subject.Desc = subject.Desc.Trim();
                    subject.Status = true;
                    if (await subjectsService.Create(subject) == 0)
                    {
                        TempData["msg"] = "<script>alert('Subject has already existed!');</script>";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["success"] = "success";

                        return RedirectToAction(nameof(Index));

                    }

                }
                else
                {

                    subject.SubjectId = "subject" + "01";
                    subject.SubjectName = subject.SubjectName.Trim();
                    subject.Desc = subject.Desc.Trim();
                    subject.Status = true;
                    await subjectsService.Create(subject);
                    TempData["success"] = "success";


                }

            }
            ViewBag.subjects = await subjectsService.FindAll();
            return RedirectToAction(nameof(Index));
        }


     
        // edit subject in db
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> Edit(Subject subject, string namesubject)
        {
            if (ModelState.IsValid)
            {
              
                    await subjectsService.Update(subject);
            
                
                TempData["success"] = "success";

                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // hide subject
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            await subjectsService.Delete(id);
            TempData["success"] = "success";

            return RedirectToAction(nameof(Index));
        }

    }
}
