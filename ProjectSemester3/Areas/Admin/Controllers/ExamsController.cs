using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Areas.Admin.Service;
using ProjectSemester3.Models;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("exams")]
    [Route("admin/exams")]
    public class ExamsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IExamsService examsService;
        private readonly IAccountService accountService;

        public ExamsController(DatabaseContext context, IExamsService examsService, IAccountService accountService)
        {
            _context = context;
            this.examsService = examsService;
            this.accountService = accountService;
        }



        // GET: Admin/Exams
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.exams = await examsService.FindAll();
                ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName");

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(Exam exam)
        {
            if (ModelState.IsValid)
            {
                // devided char and number
                var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
                int num = 0;
                if (examsService.GetNewestId() != null)
                {
                    var match = numAlpha.Match(examsService.GetNewestId());
                    //var alpha = match.Groups["Alpha"].Value;
                    num = Int32.Parse(match.Groups["Numeric"].Value);

                }


                // if have no value with this id in db, we create first 
                // else it already existed in db, we create next
                if (await examsService.CountId() != 0)
                {
                    if (num < 9)
                    {
                        exam.ExamId = "exam" + "0" + (num + 1);

                    }
                    else
                    {
                        exam.ExamId = "exam" + (num + 1);

                    }
                    exam.Title = exam.Title.Trim();
                    exam.Desc = exam.Desc.Trim();
                    exam.Status = true;
                    if (await examsService.Create(exam) == 0)
                    {
                        TempData["msg"] = "<script>alert('Exam has already existed!');</script>";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Successfully!');</script>";

                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    exam.ExamId = "exam" + "01";
                    exam.Title = exam.Title.Trim();
                    exam.Desc = exam.Desc.Trim();
                    exam.Status = true;
                    await examsService.Create(exam);

                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                }
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", exam.SubjectId);
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Exams/Edit/5
        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(string examid, string subjectid)
        {
            if (examid == null || subjectid == null)
            {
                return NotFound();
            }

            var exam = await examsService.Find(examid, subjectid);
            if (exam == null)
            {
                return NotFound();
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", exam.SubjectId);
            return View("edit", exam);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit([Bind("ExamId,SubjectId,Title,Desc")] Exam exam)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await examsService.Update(exam);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!examsService.Exists(exam.ExamId, exam.SubjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["msg"] = "<script>alert('Successfully!');</script>";

                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectName", exam.SubjectId);
            return View(exam);
        }

        // GET: Admin/Exams/Delete/5
        public async Task<IActionResult> Delete(string examid, string subjectid)
        {
            if (examid == null || subjectid == null)
            {
                return NotFound();
            }

            await examsService.Delete(examid, subjectid);
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            return RedirectToAction(nameof(Index));
        }


    }
}
