using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using ProjectSemester3.Areas.Admin.Service;
using ProjectSemester3.Areas.Admin.ViewModel;
using ProjectSemester3.Models;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("batches")]
    [Route("admin/batches")]
    public class BatchesController : Controller
    {
        //fields
        private readonly IBatchesService batchesService;
        private readonly IAccountService accountService;
        private readonly DatabaseContext context;


        // constructors
        public BatchesController(IBatchesService batchesService, IAccountService accountService, DatabaseContext context)
        {
            this.batchesService = batchesService;
            this.accountService = accountService;
            this.context = context;
        }

        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(string courseid, string classid)
        {
            var batch = await batchesService.Find(classid, courseid);
            var batchAjax = new Batch
            {
                CourseId = batch.CourseId,
                ClassId = batch.ClassId,
                Graduate = batch.Graduate,
                StartDate = batch.StartDate,
                EndDate = batch.EndDate
            };
            return new JsonResult(batchAjax);

        }

        // get data autocomplete
        [HttpGet]
        [Route("searchautocomplete")]
        public async Task<IActionResult> SearchByKeyword([FromQuery(Name = "term")] string term)
        {
            var keyword = await batchesService.GetKeyWordByKeyword(term);
            return new JsonResult(keyword);

        }

        //// get listCurse autocomplete
        //[HttpGet]
        //[Route("listCourse")]
        //public async Task<IActionResult> ListCourse([FromQuery(Name = "term")] string term)
        //{
        //    var listCourse = await batchesService.ListCourse(term);
        //    return new JsonResult(listCourse);

        //}

        // get listClass do not have course autocomplete
        [HttpGet]
        [Route("listClass")]
        public async Task<IActionResult> ListClass([FromQuery(Name = "term")] string term)
        {
            var listClass = await batchesService.ListClass(term);
            return new JsonResult(listClass);

        }

        ////Find Course In Class
        //[HttpGet]
        //[Route("listCourse")]
        //public IActionResult ListCourse(string className)
        //{
        //    var listSubject = batchesService.ListCourse(className.Trim());
        //    if (listSubject == null)
        //    {
        //        return NotFound();
        //    }
        //    var result = new List<ListSubjectViewModel>();
        //    listSubject.ForEach(s => result.Add(new ListSubjectViewModel
        //    {
        //        Id = s.SubjectId,
        //        Name = s.SubjectName
        //    }));
        //    return new JsonResult(result);
        //}

        //index page
        [Route("index")]
        public async Task<IActionResult> Index(string searchKeyword, string courseKeyword, string classKeyword, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var batches = batchesService.Search(searchKeyword, courseKeyword, classKeyword);
                ViewBag.searchKeyword = searchKeyword;
                ViewBag.courseKeyword = courseKeyword;
                ViewBag.classKeyword = classKeyword;
                ViewBag.keyword = await batchesService.GetKeyWord();

                // this is a list for form create and edit
                ViewBag.listClass = await context.Classes.Where(c => c.Status == true).Select(c => c.ClassName).ToListAsync();
                ViewBag.listCourse = await context.Courses.Where(c => c.Status == true).Select(c => c.CourseName).ToListAsync();
                // this is a list for form create and edit

                ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName");
                ViewData["CourseId"] = new SelectList(context.Courses, "CourseId", "CourseName");

                LoadPagination(batches, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }

        // load pagination
        public void LoadPagination(List<Batch> batches, int? page, int? pageSize)
        {
            var batchViewModel = new BatchViewModel();

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
            var onePageOfProducts = batches.ToPagedList(pageNumber, pagesize);

            batchViewModel.PagedList = (PagedList<Batch>)onePageOfProducts;

            ViewBag.batches = batchViewModel;
        }


        // create new batch
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(BatchViewModel batchViewModel, string listClass, string searchKeyword, string courseKeyword, string classKeyword, int? pageSize)
        {
            if (ModelState.IsValid)
            {
                
                var classs = await context.Classes.FirstOrDefaultAsync(c => c.ClassName == listClass.Trim());
                if (classs == null)
                {
                    TempData["msg"] = "<script>alert('Class is not Exist!');</script>";

                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
                }


                if (await batchesService.Create(new Batch
                {
                    CourseId = batchViewModel.Batch.CourseId,
                    ClassId = classs.ClassId,
                    Graduate = batchViewModel.Batch.Graduate,
                    StartDate = batchViewModel.Batch.StartDate,
                    EndDate = batchViewModel.Batch.EndDate,
                    Status = true
                }) == 0)
                {
                    TempData["msg"] = "<script>alert('Batch has already existed!');</script>";

                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
                }

                await batchesService.CreateClassAssign(batchViewModel.Batch.CourseId, classs.ClassId);
                await batchesService.CreateClassSchedule(batchViewModel.Batch.CourseId, classs.ClassId);
                await batchesService.CreateTestSchedule(batchViewModel.Batch.CourseId, classs.ClassId);

                TempData["success"] = "success";

            }


            // Return view index and auto paging
            return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
        }


        // edit batch
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(BatchViewModel batchViewModel, string searchKeyword, string courseKeyword, string classKeyword, int? pageSize)
        {
            if (ModelState.IsValid)
            {
                await batchesService.Update(new Batch{ 
                    CourseId = batchViewModel.Batch.CourseId,
                    ClassId = batchViewModel.Batch.ClassId,
                    Graduate = batchViewModel.Batch.Graduate,
                    StartDate = batchViewModel.Batch.StartDate,
                    EndDate = batchViewModel.Batch.EndDate,
                    Status = true
                });

                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
            }

   

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
        }

        
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(BatchViewModel batchViewModel, string searchKeyword, string courseKeyword, string classKeyword, int? pageSize)
        {
            
            await batchesService.Delete(batchViewModel.Batch.CourseId, batchViewModel.Batch.ClassId);

            TempData["success"] = "success";

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "professionals", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
        }
    }
}
