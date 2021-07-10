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
    [Route("classassignments")]
    [Route("admin/classassignments")]
    public class ClassAssignmentsController : Controller
    {

        private readonly DatabaseContext context;
        private readonly IClassAssignmentService classAssignmentService;
        // contractors

        public ClassAssignmentsController(DatabaseContext _context, IClassAssignmentService _classAssignmentService)
        {
            context = _context;
            classAssignmentService = _classAssignmentService;
        }

        // get listClass autocomplete
        [HttpGet]
        [Route("listClass")]
        public async Task<IActionResult> ListClass([FromQuery(Name = "term")] string term)
        {
            var listClass = await classAssignmentService.GetAllClass(term);

            return new JsonResult(listClass);

        }

        //Find Subject In Class
        [HttpGet]
        [Route("findSubject")]
        public IActionResult FindSubject(string className)
        {
            var listSubject = classAssignmentService.GetListSubject(className.Trim());
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

        //// Find faculty by subject
        [HttpGet]
        [Route("findFaculty")]
        public IActionResult FindFaculty(string subjectName)
        {
            var listFaculty = classAssignmentService.GetListFaculty(subjectName);
            if (listFaculty == null)
            {
                return NotFound();
            }
            var result = new List<ListSubjectViewModel>();
            listFaculty.ForEach(s => result.Add(new ListSubjectViewModel
            {
                Id = s.AccountId,
                Name = s.Fullname
            }));
            return new JsonResult(result);
        }

        // page index of ClassAssignment
        [Route("index")]
        public IActionResult Index(string searchClassAssignment, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var classAssignments = classAssignmentService.Search(searchClassAssignment);
                ViewBag.searchClassAssignment = searchClassAssignment;

                LoadPagination(classAssignments, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        // load pagination
        public void LoadPagination(List<ClassAssignment> classAssignments, int? page, int? pageSize)
        {
            var classAssignmentViewModel = new ClassAssignmentViewModel();

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
            var onePageOfProducts = classAssignments.ToPagedList(pageNumber, pagesize);

            classAssignmentViewModel.PagedList = (PagedList<ClassAssignment>)onePageOfProducts;

            ViewBag.classAssignments = classAssignmentViewModel;
        }


        // Create classAssignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(ClassAssignment classAssignment, string cbbClass, string cbbSubject, string searchClassAssignment, int? pageSize)
        {
            if (ModelState.IsValid)
            {
                var classs = await context.Classes.FirstOrDefaultAsync(c => c.ClassName == cbbClass.Trim());
                if (classs == null)
                {
                    TempData["msg"] = "<script>alert('Class is not Exist!');</script>";

                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "classassignments", action = "index", searchClassAssignment = searchClassAssignment, pageSize = pageSize });
                }
                await classAssignmentService.Create(classAssignment.FacultyId, classs.ClassId, cbbSubject.Trim());
                TempData["success"] = "success";

            }

            return RedirectToRoute(new { controller = "classassignments", action = "index", searchClassAssignment = searchClassAssignment, pageSize = pageSize });
        }



    }
}
