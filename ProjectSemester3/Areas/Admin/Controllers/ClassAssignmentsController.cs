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
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.classassignments = await classAssignmentService.FindAll();
                //ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName");
                //ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname");
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }


        }


        // Create classAssignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(ClassAssignment classAssignment, string cbbClass, string cbbSubject)
        {
            if (ModelState.IsValid)
            {
                var classs = await context.Classes.FirstOrDefaultAsync(c => c.ClassName == cbbClass.Trim());
                if (classs == null)
                {
                    TempData["msg"] = "<script>alert('Class is not Exist!');</script>";

                    // Return view index and auto paging
                    //return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
                }
                await classAssignmentService.Create(classAssignment.FacultyId, classs.ClassId, cbbSubject.Trim());
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            }
            ViewBag.classassignments = await classAssignmentService.FindAll();

            //ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName", classAssignment.ClassId);
            //ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname", classAssignment.FacultyId);
            return View("index");
        }

        // go to page edit classAssignment
        [Route("edit")]
        public async Task<IActionResult> Edit(string facultyid, string classid)
        {

            var classAssignment = await classAssignmentService.Find(facultyid, classid);

            ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName", classAssignment.ClassId);
            ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname", classAssignment.FacultyId);
            return View(classAssignment);
        }

        // Edit classAssignment
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(string id, [Bind("FacultyId,ClassId")] ClassAssignment classAssignment)
        {


            if (ModelState.IsValid)
            {

                context.Update(classAssignment);
                await context.SaveChangesAsync();

                TempData["msg"] = "<script>alert('Successfully!');</script>";

                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName", classAssignment.ClassId);
            ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname", classAssignment.FacultyId);

            return View(classAssignment);
        }


    }
}
