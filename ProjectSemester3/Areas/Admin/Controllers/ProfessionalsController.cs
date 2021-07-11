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
using X.PagedList;

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

        // get data autocomplete
        [HttpGet]
        [Route("searchautocomplete")]
        public async Task<IActionResult> SearchByKeyword([FromQuery(Name = "term")] string term)
        {
            var keyword = await professionalsService.GetKeyWordByKeyword(term);
            return new JsonResult(keyword);

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
            var listSubject = new List<Subject>();

            try
            {
                listSubject = professionalsService.GetListSubject(facultyName.Trim());
            }
            catch (Exception)
            {

                throw;
            }
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
        public async Task<IActionResult> Index(string searchProfessional, string subjectKeyword, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var professionals = professionalsService.Search(searchProfessional, subjectKeyword);
                ViewBag.searchProfessional = searchProfessional;
                ViewBag.subjectKeyword = subjectKeyword;
                //ViewBag.classKeyword = classKeyword;
                //ViewBag.keyword = await batchesService.GetKeyWord();

                // this is a list for form create and edit
                ViewBag.listSubject = await context.Subjects.Where(s => s.Status == true).Select(c => c.SubjectName).ToListAsync();
                // this is a list for form create and edit

             
                LoadPagination(professionals, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }


        // load pagination
        public void LoadPagination(List<Professional> professionals, int? page, int? pageSize)
        {
            var professionalViewModel = new ProfessionalViewModel();

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
            var onePageOfProducts = professionals.ToPagedList(pageNumber, pagesize);

            professionalViewModel.PagedList = (PagedList<Professional>)onePageOfProducts;

            ViewBag.professionals = professionalViewModel;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(Professional professional, string facultyProfess, string subjectProfess, string searchProfessional, string subjectKeyword, int? pageSize)
        {
            if (ModelState.IsValid && subjectProfess != null)
            {

                var facultyId = await context.Accounts.FirstOrDefaultAsync(c => c.Fullname == facultyProfess.Trim());
                
                if (facultyId == null)
                {
                    TempData["msg"] = "<script>alert('Faculty is not Exist!');</script>";

                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "professionals", action = "index", searchProfessional = searchProfessional, subjectKeyword = subjectKeyword, pageSize = pageSize });
                }

                
                professional.FacultyId = facultyId.AccountId;
                professional.SubjectId = subjectProfess;
                professional.Status = true;
                if (await professionalsService.Create(professional) == 0)
                {
                    TempData["msg"] = "<script>alert('Professional has already existed!');</script>";
                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "professionals", action = "index", searchProfessional = searchProfessional, subjectKeyword = subjectKeyword, pageSize = pageSize });
                }
                else
                {
                    TempData["success"] = "success";

                    // Return view index and auto paging
                    return RedirectToRoute(new { controller = "professionals", action = "index", searchProfessional = searchProfessional, subjectKeyword = subjectKeyword, pageSize = pageSize });
                }


            }
            ViewBag.professionals = await professionalsService.FindAll();

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "professionals", action = "index", searchProfessional = searchProfessional, subjectKeyword = subjectKeyword, pageSize = pageSize });
        }


        // delete professional
        [HttpPost("delete")]
        public async Task<IActionResult> Delete(ProfessionalViewModel professionalViewModel, string searchProfessional, string subjectKeyword, int? pageSize)
        {

            professionalViewModel.Professional.Status = false;
            await professionalsService.Update(professionalViewModel.Professional);

            TempData["success"] = "success";

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "professionals", action = "index", searchProfessional = searchProfessional, subjectKeyword = subjectKeyword, pageSize = pageSize });
        }
    }
}
