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
    [Route("courses")]
    [Route("admin/courses")]
    public class CoursesController : Controller
    {
        private readonly ICoursesService coursesService;
        private IAccountService accountService;

        public CoursesController(ICoursesService coursesService, IAccountService accountService)
        {
            this.coursesService = coursesService;
            this.accountService = accountService;
        }


        // GET: Admin/Courses
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.courses = await coursesService.FindAll();
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }


        }

        //// GET: Admin/Courses/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses
        //        .FirstOrDefaultAsync(m => m.CourseId == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(course);
        //}

        // GET: Admin/Courses/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Admin/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create([Bind("CourseName,Fee,Term,Certificate,Desc")] Course course)
        {
            if (ModelState.IsValid)
            {
                var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
                int num = 0;
                if (coursesService.GetNewestId() != null)
                {
                    var match = numAlpha.Match(coursesService.GetNewestId());
                    //var alpha = match.Groups["Alpha"].Value;
                    num = Int32.Parse(match.Groups["Numeric"].Value);

                }


                // if have no value with this id in db, we create first 
                // else it already existed in db, we create next
                if (await coursesService.CountId() != 0)
                {
                    if (num < 9)
                    {
                        course.CourseId = "course" + "0" + (num + 1);

                    }
                    else
                    {
                        course.CourseId = "course" + (num + 1);

                    }
                    course.CourseName = course.CourseName.Trim();
                    course.Term = course.Term.Trim();
                    course.Certificate = course.Certificate.Trim();
                    course.Desc = course.Desc.Trim();
                    course.Status = true;
                    if (await coursesService.Create(course) == 0)
                    {
                        TempData["msg"] = "<script>alert('Course has already existed!');</script>";
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

                    course.CourseId = "course" + "01";
                    course.CourseName = course.CourseName.Trim();
                    course.Term = course.Term.Trim();
                    course.Certificate = course.Certificate.Trim();
                    course.Desc = course.Desc.Trim();
                    course.Status = true;
                    await coursesService.Create(course);
                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                }
            }
            ViewBag.courses = await coursesService.FindAll();
            return RedirectToAction(nameof(Index));

        }

        // GET: Admin/Courses/Edit/5
        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await coursesService.Find(id);
            if (course == null)
            {
                return NotFound();
            }
            return View("edit", course);
        }

        // POST: Admin/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(Course course)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await coursesService.Update(course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!coursesService.RoleExists(course.CourseId))
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
            return View(course);
        }

        //// GET: Admin/Courses/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var course = await _context.Courses
        //        .FirstOrDefaultAsync(m => m.CourseId == id);
        //    if (course == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(course);
        //}

        // POST: Admin/Courses/Delete/5
        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await coursesService.Delete(id);
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            return RedirectToAction(nameof(Index));
        }


    }
}
