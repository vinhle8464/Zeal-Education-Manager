using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Areas.Admin.Service;
using ProjectSemester3.Models;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("feedbackfaculties")]
    [Route("admin/feedbackfaculties")]
    public class FeedbackFacultiesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IFeedbackFacultyService feedbackFacultyService;

        public FeedbackFacultiesController(DatabaseContext context, IFeedbackFacultyService feedbackFacultyService)
        {
            _context = context;
            this.feedbackFacultyService = feedbackFacultyService;
        }



        // GET: Admin/FeedbackFaculties
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.feedbackFacultys = await feedbackFacultyService.FindAll();
                ViewData["FacultyId"] = new SelectList(_context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname");
                ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId");
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
        public async Task<IActionResult> Create([Bind("FeedbackId,FacultyId")] FeedbackFaculty feedbackFaculty)
        {
            if (ModelState.IsValid)
            {
                if (await feedbackFacultyService.Create(feedbackFaculty) == 0)
                {
                    TempData["msg"] = "<script>alert('Feedback Faculty has already existed!');</script>";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                    return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.feedbackFacultys = await feedbackFacultyService.FindAll();
            ViewData["FacultyId"] = new SelectList(_context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname"); ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId");

            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/FeedbackFaculties/Edit/5
        [Route("edit")]
        public async Task<IActionResult> Edit(int feedbackid, string facultyid)
        {
            var feedback = await feedbackFacultyService.Find(feedbackid, facultyid);

            ViewData["FacultyId"] = new SelectList(_context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname"); ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId");
            return View("edit", feedback);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit([Bind("FeedbackId,FacultyId")] FeedbackFaculty feedbackFaculty)
        {
            if (ModelState.IsValid)
            {
                await feedbackFacultyService.Update(feedbackFaculty);
                TempData["msg"] = "<script>alert('Successfully!');</script>";

                return RedirectToAction(nameof(Index));
            }
            ViewBag.feedbackFacultys = await feedbackFacultyService.FindAll();
            ViewData["FacultyId"] = new SelectList(_context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname"); ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId");

            return View(feedbackFaculty);
        }

        // GET: Admin/FeedbackFaculties/Delete/5
        [Route("delete")]
        public async Task<IActionResult> Delete(int feedbackid, string facultyid)
        {
            await feedbackFacultyService.Delete(feedbackid, facultyid);

            ViewBag.feedbackFacultys = await feedbackFacultyService.FindAll();
            ViewData["FacultyId"] = new SelectList(_context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname");
            ViewData["FeedbackId"] = new SelectList(_context.Feedbacks, "FeedbackId", "FeedbackId");
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            return RedirectToAction(nameof(Index));
        }
    }
}
