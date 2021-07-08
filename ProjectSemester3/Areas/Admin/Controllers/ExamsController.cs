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
using ProjectSemester3.Areas.Admin.ViewModel;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using X.PagedList;

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

        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(string examid)
        {
            var exam = await examsService.FindAjax(examid);
            var examAjax = new Exam
            {
              ExamId = exam.ExamId,
              SubjectId = exam.SubjectId,
              Title = exam.Title,
              Desc = exam.Desc,
              Status = exam.Status
            };
            return new JsonResult(examAjax);

        }


        // GET: Admin/Exams
        [Route("index")]
        public IActionResult Index(string searchExam, string filterSubject, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var exams = examsService.Search(searchExam, filterSubject);
                ViewBag.searchExam = searchExam;
                ViewBag.filterSubject = filterSubject;

                LoadPagination(exams, page, pageSize);
                ViewData["SubjectId"] = new SelectList(_context.Subjects.Where(s => s.Status == true), "SubjectId", "SubjectName");

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        // load pagination
        public void LoadPagination(List<Exam> exams, int? page, int? pageSize)
        {
            var examViewModel = new ExamViewModel();

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
            var onePageOfProducts = exams.ToPagedList(pageNumber, pagesize);

            examViewModel.PagedList = (PagedList<Exam>)onePageOfProducts;

            ViewBag.exams = examViewModel;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(ExamViewModel examViewModel, string searchExam, string filterSubject, int? pageSize)
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
                        examViewModel.Exam.ExamId = "exam" + "0" + (num + 1);

                    }
                    else
                    {
                        examViewModel.Exam.ExamId = "exam" + (num + 1);

                    }
                    examViewModel.Exam.Title = examViewModel.Exam.Title.Trim();
                    examViewModel.Exam.Desc = examViewModel.Exam.Desc.Trim();
                    examViewModel.Exam.Status = true;
                    if (await examsService.Create(examViewModel.Exam) == 0)
                    {
                        TempData["msg"] = "<script>alert('Exam has already existed!');</script>";
                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "exams", action = "index", searchExam = searchExam, filterSubject = filterSubject, pageSize = pageSize });
                    }
                    else
                    {
                        TempData["success"] = "success";

                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "exams", action = "index", searchExam = searchExam, filterSubject = filterSubject, pageSize = pageSize });
                    }
                }
                else
                {
                    examViewModel.Exam.ExamId = "exam" + "01";
                    examViewModel.Exam.Title = examViewModel.Exam.Title.Trim();
                    examViewModel.Exam.Desc = examViewModel.Exam.Desc.Trim();
                    examViewModel.Exam.Status = true;
                    await examsService.Create(examViewModel.Exam);

                    TempData["success"] = "success";

                }
            }
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "exams", action = "index", searchExam = searchExam, filterSubject = filterSubject, pageSize = pageSize });
        }




        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(ExamViewModel examViewModel, string searchExam, string filterSubject, int? pageSize)
        {

            if (ModelState.IsValid)
            {

                await examsService.Update(examViewModel.Exam);


                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "exams", action = "index", searchExam = searchExam, filterSubject = filterSubject, pageSize = pageSize });
            }
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "exams", action = "index", searchExam = searchExam, filterSubject = filterSubject, pageSize = pageSize });
        }

        // hide exam
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(ExamViewModel examViewModel, string searchExam, string filterSubject, int? pageSize)
        {
            await examsService.Delete(examViewModel.Exam.ExamId, examViewModel.Exam.SubjectId);
            TempData["success"] = "success";

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "exams", action = "index", searchExam = searchExam, filterSubject = filterSubject, pageSize = pageSize });
        }



    }
}
