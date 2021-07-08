using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using ProjectSemester3.Areas.Admin.Service;
using ProjectSemester3.Services;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ProjectSemester3.Areas.Admin.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("classes")]
    [Route("admin/classes")]
    public class ClassesController : Controller
    {

        private readonly IClassesService classesService;
        private IAccountService accountService;

        public ClassesController(IClassesService classesService, IAccountService accountService)
        {
            this.classesService = classesService;
            this.accountService = accountService;
        }

        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(string classid)
        {
            var clas = await classesService.FindAjax(classid);
            var accountAjax = new Class
            {
                ClassId = clas.ClassId,
                ClassName = clas.ClassName,
                NumberOfStudent = clas.NumberOfStudent,
                Desc = clas.Desc,
                Status = clas.Status
            };
            return new JsonResult(accountAjax);

        }

        // Show page class
        [Route("index")]
        public IActionResult Index(string searchClass, string filterNumber, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var classes = classesService.Search(searchClass, Convert.ToInt32(filterNumber));
                ViewBag.searchClass = searchClass;
                ViewBag.filterNumber = filterNumber;

                LoadPagination(classes, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }

        // load pagination
        public void LoadPagination(List<Class> classes, int? page, int? pageSize)
        {
            var classViewModel = new ClassViewModel();

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
            var onePageOfProducts = classes.ToPagedList(pageNumber, pagesize);

            classViewModel.PagedList = (PagedList<Class>)onePageOfProducts;

            ViewBag.classes = classViewModel;
        }



        // create class
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClassViewModel classViewModel, string searchClass, string filterNumber, int? pageSize)
        {
            if (ModelState.IsValid & classViewModel != null)
            {
                // devided char and number
                var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
                int num = 0;
                if (classesService.GetNewestId() != null)
                {
                    var match = numAlpha.Match(classesService.GetNewestId());
                    //var alpha = match.Groups["Alpha"].Value;
                    num = Int32.Parse(match.Groups["Numeric"].Value);

                }

                // if have no value with this id in db, we create first 
                // else it already existed in db, we create next
                if (await classesService.CountId() != 0)
                {
                    if (num < 9)
                    {
                        classViewModel.Class.ClassId = "class" + "0" + (num + 1);

                    }
                    else
                    {
                        classViewModel.Class.ClassId = "class" + (num + 1);

                    }
                    classViewModel.Class.Status = true;
                    classViewModel.Class.ClassName = classViewModel.Class.ClassName.Trim();
                    classViewModel.Class.NumberOfStudent = classViewModel.Class.NumberOfStudent;
                    classViewModel.Class.Desc = classViewModel.Class.Desc.Trim();

                    if (await classesService.Create(classViewModel.Class) == 0)
                    {
                        TempData["msg"] = "<script>alert('Class has already existed!');</script>";
                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "classes", action = "index", searchClass = searchClass, filterNumber = filterNumber, pageSize = pageSize });
                    }
                    else
                    {
                        TempData["success"] = "success";

                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "classes", action = "index", searchClass = searchClass, filterNumber = filterNumber, pageSize = pageSize });

                    }


                }
                else
                {
                    classViewModel.Class.ClassId = "class" + "01";
                    classViewModel.Class.Status = true;
                    classViewModel.Class.ClassName = classViewModel.Class.ClassName.Trim();
                    classViewModel.Class.NumberOfStudent = classViewModel.Class.NumberOfStudent;
                    classViewModel.Class.Desc = classViewModel.Class.Desc.Trim();
                    await classesService.Create(classViewModel.Class);
                }
                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "classes", action = "index", searchClass = searchClass, filterNumber = filterNumber, pageSize = pageSize });
            }
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "classes", action = "index", searchClass = searchClass, filterNumber = filterNumber, pageSize = pageSize });

        }

    
        // edit class in db
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(ClassViewModel classViewModel, string searchClass, string filterNumber, int? pageSize)
        {

            if (ModelState.IsValid)
            {
                    await classesService.Update(classViewModel.Class);
               
                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "classes", action = "index", searchClass = searchClass, filterNumber = filterNumber, pageSize = pageSize });
            }
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "classes", action = "index", searchClass = searchClass, filterNumber = filterNumber, pageSize = pageSize });
        }

        // hide class
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(ClassViewModel classViewModel, string searchClass, string filterNumber, int? pageSize)
        {
            await classesService.Delete(classViewModel.Class.ClassId);
            TempData["success"] = "success";

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "classes", action = "index", searchClass = searchClass, filterNumber = filterNumber, pageSize = pageSize });
        }


    }
}
