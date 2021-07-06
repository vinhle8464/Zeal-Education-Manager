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
    [Route("professionals")]
    [Route("admin/professionals")]
    public class ProfessionalsController : Controller
    {
        private readonly DatabaseContext context;
        private readonly IProfessionalsService professionalsService;

        public ProfessionalsController(DatabaseContext _context, IProfessionalsService professionalsService)
        {
            context = _context;
            this.professionalsService = professionalsService;
        }

        // get list Faculty autocomplete
        [HttpGet]
        [Route("listFaculty")]
        public async Task<IActionResult> ListFaculty([FromQuery(Name = "term")] string term)
        {
            var listFaculty = await professionalsService.GetAllFaculty(term);

            return new JsonResult(listFaculty);

        }

        //Find Subject In Class
        [HttpGet]
        [Route("findSubject")]
        public IActionResult FindSubject(string facultyName)
        {
            var listSubject = professionalsService.GetListSubject(facultyName.Trim());
            if(listSubject == null)
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

        // GET: Admin/Professionals
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.professionals = await professionalsService.FindAll();
              
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
        public async Task<IActionResult> Create(Professional professional, string facultyProfess, string subjectProfess)
        {
            if (ModelState.IsValid && subjectProfess != null)
            {

                var facultyId = await context.Accounts.FirstOrDefaultAsync(c => c.Fullname == facultyProfess.Trim());
                
                if (facultyId == null)
                {
                    TempData["msg"] = "<script>alert('Faculty is not Exist!');</script>";
                    return RedirectToAction(nameof(Index));

                    // Return view index and auto paging
                    //return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
                }

                

                //if (subjectId == null)
                //{
                //    TempData["msg"] = "<script>alert('Subject is not Exist!');</script>";

                //    // Return view index and auto paging
                //    //return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
                //}

                professional.FacultyId = facultyId.AccountId;
                professional.SubjectId = subjectProfess;
                professional.Status = true;
                if (await professionalsService.Create(professional) == 0)
                {
                    TempData["msg"] = "<script>alert('Professional has already existed!');</script>";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                    return RedirectToAction(nameof(Index));
                }


            }
            ViewBag.professionals = await professionalsService.FindAll();

            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Professionals/Edit/5
        [Route("edit")]
        public async Task<IActionResult> Edit(string facultyid, string subjectid)
        {
            var professional = await professionalsService.Find(facultyid, subjectid);

            ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname", professional.FacultyId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", professional.SubjectId);
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            return View(professional);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(string id, [Bind("FacultyId,SubjectId")] Professional professional)
        {
            if (ModelState.IsValid)
            {
                context.Update(professional);
                await context.SaveChangesAsync();
                TempData["msg"] = "<script>alert('Successfully!');</script>";

                return RedirectToAction(nameof(Index));
            }

            ViewData["FacultyId"] = new SelectList(context.Accounts.Where(a => a.RoleId == "role02"), "AccountId", "Fullname", professional.FacultyId);
            ViewData["SubjectId"] = new SelectList(context.Subjects, "SubjectId", "SubjectName", professional.SubjectId);

            return View(professional);
        }


    }
}
