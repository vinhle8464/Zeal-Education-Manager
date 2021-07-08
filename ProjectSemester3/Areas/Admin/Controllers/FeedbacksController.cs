using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Areas.Admin.Service;
using ProjectSemester3.Areas.Admin.ViewModel;
using ProjectSemester3.Models;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("feedbacks")]
    [Route("admin/feedbacks")]
    public class FeedbacksController : Controller
    {
        private readonly DatabaseContext context;
        private readonly IFeedBackService feedBackService;

        public FeedbacksController(DatabaseContext _context, IFeedBackService feedBackService)
        {
            context = _context;
            this.feedBackService = feedBackService;
        }


        // get listFacluty autocomplete
        [HttpGet]
        [Route("listFaculty")]
        public async Task<IActionResult> ListFaculty([FromQuery(Name = "term")] string term)
        {
            var listFaculty = await feedBackService.GetListFaculty(term);

            return new JsonResult(listFaculty);

        }

        //Find Subject by Faculty
        [HttpGet]
        [Route("findSubject")]
        public IActionResult FindSubject(string facultyName)
        {
            var listSubject = feedBackService.GetListSubjectAsync(facultyName.Trim());
            if (listSubject == null)
            {
                return NotFound();
            }
            var result = new List<ListSubjectViewModel>();
            listSubject.ForEach(s => result.Add(new ListSubjectViewModel
            {
                Id = s.SubjectId,
                Name = s.SubjectName
            }));
            return new JsonResult(result);
        }


        // GET: Admin/Feedbacks
        [Route("index")]
        public async Task<IActionResult> Index(string searchFeedback, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.feedbacks = await feedBackService.FindAll();
                //ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName");

                var feedbacks = feedBackService.Search(searchFeedback);
                ViewBag.searchFeedback = searchFeedback;

                LoadPagination(feedbacks, page, pageSize);
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        // load pagination
        public void LoadPagination(List<Feedback> feedbacks, int? page, int? pageSize)
        {
            var feedbackViewModel = new FeedbackViewModel();

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
            var onePageOfProducts = feedbacks.ToPagedList(pageNumber, pagesize);

            feedbackViewModel.PagedList = (PagedList<Feedback>)onePageOfProducts;

            ViewBag.feedbacks = feedbackViewModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(FeedbackViewModel feedbackViewModel, string feedbackFaculty, string subjectFeedback, string searchFeedback, int? pageSize)
        {
            if (ModelState.IsValid && subjectFeedback != null)
            {
                var facultyId = await context.Accounts.FirstOrDefaultAsync(c => c.Fullname == feedbackFaculty.Trim());

                if (facultyId == null)
                {
                    TempData["msg"] = "<script>alert('Faculty is not Exist!');</script>";
                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "feedbacks", action = "index", searchFeedback = searchFeedback, pageSize = pageSize });

                    // Return view index and auto paging
                    //return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
                }


                feedbackViewModel.Feedback.SubjectId = subjectFeedback;
                feedbackViewModel.Feedback.Note = feedbackViewModel.Feedback.Note.Trim();
                feedbackViewModel.Feedback.Status = true;
                var result = await feedBackService.Create(feedbackViewModel.Feedback);
                if(result != null)
                {
                    await feedBackService.CreateFeedbackFaculty(new FeedbackFaculty {
                        FeedbackId = result.FeedbackId,
                        FacultyId = facultyId.AccountId,
                        Status = true
                    });
                }
                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "feedbacks", action = "index", searchFeedback = searchFeedback, pageSize = pageSize });


            }
            ViewBag.feedbacks = await feedBackService.FindAll();
            //ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", feedback.SubjectId);

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "feedbacks", action = "index", searchFeedback = searchFeedback, pageSize = pageSize });
        }

        //// GET: Admin/Feedbacks/Edit/5
        //[Route("edit")]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    var feedback = await feedBackService.Find(id);

        //    ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", feedback.SubjectId);
        //    return View("edit", feedback);

        //}


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Route("edit")]
        //public async Task<IActionResult> Edit([Bind("FeedbackId,SubjectId,Teaching,Exercises,TeacherEthics,Specialize,Assiduous,Note")] Feedback feedback)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        await feedBackService.Update(feedback);
        //        TempData["msg"] = "<script>alert('Successfully!');</script>";

        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", feedback.SubjectId);
        //    return View(feedback);
        //}

        // GET: Admin/Feedbacks/Delete/5
        public async Task<IActionResult> Delete(FeedbackViewModel feedbackViewModel, string searchFeedback, int? pageSize)
        {
            await feedBackService.Delete(feedbackViewModel.Feedback.FeedbackId);
            TempData["success"] = "success";

            ViewBag.feedbacks = await feedBackService.FindAll();
            // ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName");

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "feedbacks", action = "index", searchFeedback = searchFeedback, pageSize = pageSize });
        }

    }
}
