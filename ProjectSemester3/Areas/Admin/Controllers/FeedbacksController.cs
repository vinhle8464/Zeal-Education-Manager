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
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.feedbacks = await feedBackService.FindAll();
                ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName");
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
        public async Task<IActionResult> Create(Feedback feedback, string feedbackFaculty, string subjectFeedback)
        {
            if (ModelState.IsValid && subjectFeedback != null)
            {
                var facultyId = await context.Accounts.FirstOrDefaultAsync(c => c.Fullname == feedbackFaculty.Trim());

                if (facultyId == null)
                {
                    TempData["msg"] = "<script>alert('Faculty is not Exist!');</script>";
                    return RedirectToAction(nameof(Index));

                    // Return view index and auto paging
                    //return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
                }


                feedback.SubjectId = subjectFeedback;
                feedback.Note = feedback.Note.Trim();
                feedback.Status = true;
                var result = await feedBackService.Create(feedback);
                if(result != null)
                {
                    await feedBackService.CreateFeedbackFaculty(new FeedbackFaculty {
                        FeedbackId = result.FeedbackId,
                        FacultyId = facultyId.AccountId,
                        Status = true
                    });
                }
                TempData["msg"] = "<script>alert('Successfully');</script>";

                return RedirectToAction(nameof(Index));


            }
            ViewBag.feedbacks = await feedBackService.FindAll();
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", feedback.SubjectId);

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Feedbacks/Edit/5
        [Route("edit")]
        public async Task<IActionResult> Edit(int id)
        {
            var feedback = await feedBackService.Find(id);

            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", feedback.SubjectId);
            return View("edit", feedback);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit([Bind("FeedbackId,SubjectId,Teaching,Exercises,TeacherEthics,Specialize,Assiduous,Note")] Feedback feedback)
        {
            if (ModelState.IsValid)
            {
                await feedBackService.Update(feedback);
                TempData["msg"] = "<script>alert('Successfully!');</script>";

                return RedirectToAction(nameof(Index));
            }
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", feedback.SubjectId);
            return View(feedback);
        }

        // GET: Admin/Feedbacks/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            await feedBackService.Delete(id);
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            ViewBag.feedbacks = await feedBackService.FindAll();
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName");

            return RedirectToAction(nameof(Index));
        }

    }
}
