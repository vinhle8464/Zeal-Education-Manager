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

        // get data autocomplete
        [HttpGet]
        [Route("searchautocomplete")]
        public async Task<IActionResult> SearchByKeyword([FromQuery(Name = "term")] string term)
        {
            var keyword = await coursesService.GetKeyWordByKeyword(term);
            return new JsonResult(keyword);

        }


        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(string courseid)
        {
            var course = await coursesService.FindAjax(courseid);
            var courseAjax = new Course
            {
                CourseId = course.CourseId,
               CourseName = course.CourseName,
               Fee = course.Fee,
               Term = course.Term,
               Certificate = course.Certificate,
               Desc = course.Desc,
               Status = course.Status
            };
            return new JsonResult(courseAjax);

        }

        // GET: Admin/Courses
        [Route("index")]
        public IActionResult Index(string searchCourse, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var courses = coursesService.Search(searchCourse);
                ViewBag.searchCourse = searchCourse;
              
                LoadPagination(courses, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }


        // load pagination
        public void LoadPagination(List<Course> course, int? page, int? pageSize)
        {
            var courseViewModel = new CourseViewModel();

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
            var onePageOfProducts = course.ToPagedList(pageNumber, pagesize);

            courseViewModel.PagedList = (PagedList<Course>)onePageOfProducts;

            ViewBag.courses = courseViewModel;
        }


        // create course
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(Course course, string searchCourse, int? pageSize)
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
                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "courses", action = "index", searchCourse = searchCourse, pageSize = pageSize });
                    }
                    else
                    {
                        TempData["success"] = "success";

                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "courses", action = "index", searchCourse = searchCourse, pageSize = pageSize });
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
                    TempData["success"] = "success";

                }
            }
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "courses", action = "index", searchCourse = searchCourse, pageSize = pageSize });
        }

     // edit course
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(CourseViewModel courseViewModel, string searchCourse, int? pageSize)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await coursesService.Update(courseViewModel.Course);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!coursesService.RoleExists(courseViewModel.Course.CourseId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "courses", action = "index", searchCourse = searchCourse, pageSize = pageSize });
            }
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "courses", action = "index", searchCourse = searchCourse, pageSize = pageSize });
        }

   
        // delete course
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(CourseViewModel courseViewModel, string searchCourse, int? pageSize)
        {

            await coursesService.Delete(courseViewModel.Course.CourseId);
            TempData["success"] = "success";

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "courses", action = "index", searchCourse = searchCourse, pageSize = pageSize });
        }

    }
}
