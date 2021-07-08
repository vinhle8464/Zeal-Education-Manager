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
using ProjectSemester3.Services;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("coursesubjects")]
    [Route("admin/coursesubjects")]
    public class CourseSubjectsController : Controller
    {
        private readonly ICourseSubjectService courseSubjectService;
        private readonly IAccountService accountService;
        private readonly DatabaseContext context;

        public CourseSubjectsController(ICourseSubjectService _courseSubjectService, IAccountService _accountService, DatabaseContext _context)
        {
            this.courseSubjectService = _courseSubjectService;
            this.accountService = _accountService;
            this.context = _context;
        }

        // get list Faculty autocomplete
        [HttpGet]
        [Route("listCourse")]
        public async Task<IActionResult> ListCourse([FromQuery(Name = "term")] string term)
        {
            var listCourse = await courseSubjectService.GetAllCourse(term);

            return new JsonResult(listCourse);

        }

        //Find Subject In Class
        [HttpGet]
        [Route("findSubject")]
        public IActionResult FindSubject(string courseName)
        {
            var listSubject = courseSubjectService.GetListSubject(courseName.Trim());
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

        // GET: Admin/CourseSubjects
        [Route("index")]
        public IActionResult Index(string searchCourseSubject, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var courseSubjectes = courseSubjectService.Search(searchCourseSubject);
                ViewBag.searchCourseSubject = searchCourseSubject;

                LoadPagination(courseSubjectes, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }


        // load pagination
        public void LoadPagination(List<CourseSubject> courseSubjectes, int? page, int? pageSize)
        {
            var courseSubjectViewModel = new CourseSubjectViewModel();

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
            var onePageOfProducts = courseSubjectes.ToPagedList(pageNumber, pagesize);

            courseSubjectViewModel.PagedList = (PagedList<CourseSubject>)onePageOfProducts;

            ViewBag.courseSubjectes = courseSubjectViewModel;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(CourseSubjectViewModel courseSubjectViewModel, string courselist, string subjectlist, string searchCourseSubject, int? pageSize)
        {
            if (ModelState.IsValid)
            {

                var courseid = await context.Courses.FirstOrDefaultAsync(c => c.CourseName == courselist.Trim());

                if (courseid == null)
                {
                    TempData["msg"] = "<script>alert('Course is not Exist!');</script>";
                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "coursesubjects", action = "index", searchCourseSubject = searchCourseSubject, pageSize = pageSize });
                }
                var courseSubject = new CourseSubject
                {
                    CourseId = courseid.CourseId,
                    SubjectId = subjectlist,
                    Status = true
                };
                if (await courseSubjectService.Create(courseSubject) == 0)
                {
                    TempData["msg"] = "<script>alert('Course-Subject has already existed!');</script>";
                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "coursesubjects", action = "index", searchCourseSubject = searchCourseSubject, pageSize = pageSize });
                }
                else
                {
                    TempData["success"] = "success";

                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "coursesubjects", action = "index", searchCourseSubject = searchCourseSubject, pageSize = pageSize });
                }
            }



            // Return view index and auto paging
            return RedirectToRoute(new { controller = "coursesubjects", action = "index", searchCourseSubject = searchCourseSubject, pageSize = pageSize });
        }

        // delete professional
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(CourseSubjectViewModel courseSubjectViewModel, string searchCourseSubject, int? pageSize)
        {

            courseSubjectViewModel.CourseSubject.Status = false;
            await courseSubjectService.Update(courseSubjectViewModel.CourseSubject);

            TempData["success"] = "success";

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "coursesubjects", action = "index", searchCourseSubject = searchCourseSubject, pageSize = pageSize });
        }

    }
}
