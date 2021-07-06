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
    [Route("scholarshipstudents")]
    [Route("admin/scholarshipstudents")]
    public class ScholarshipStudentsController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IScholarshipStudentService scholarshipStudentService;

        public ScholarshipStudentsController(DatabaseContext context, IScholarshipStudentService scholarshipStudentService)
        {
            _context = context;
            this.scholarshipStudentService = scholarshipStudentService;
        }



        // GET: Admin/ScholarshipStudents
        [Route("index")]
        public async Task<IActionResult> Index()
        {

            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.scholarshipstudents = await scholarshipStudentService.FindAll();
                ViewData["AccountId"] = new SelectList(_context.Accounts.Where(a => a.RoleId == "role03"), "AccountId", "Fullname");
                ViewData["ScholarshipId"] = new SelectList(_context.Scholarships, "ScholarshipId", "ScholarshipName");
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

           
        }

        // GET: Admin/ScholarshipStudents/Details/5
        //public async Task<IActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var scholarshipStudent = await _context.ScholarshipStudents
        //        .Include(s => s.Account)
        //        .Include(s => s.Scholarship)
        //        .FirstOrDefaultAsync(m => m.AccountId == id);
        //    if (scholarshipStudent == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(scholarshipStudent);
        //}

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create([Bind("AccountId,ScholarshipId")] ScholarshipStudent scholarshipStudent)
        {
            if (ModelState.IsValid)
            {
                if (await scholarshipStudentService.Create(scholarshipStudent) == 0)
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
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Fullname", scholarshipStudent.AccountId);
            ViewData["ScholarshipId"] = new SelectList(_context.Scholarships, "ScholarshipId", "ScholarshipName", scholarshipStudent.ScholarshipId);
            return View(scholarshipStudent);
        }

        // GET: Admin/ScholarshipStudents/Edit/5
        [Route("edit")]
        public async Task<IActionResult> Edit(string accountid, string scholarshipid)
        {

            var scholarshipStudent = await scholarshipStudentService.Find(accountid, scholarshipid);

            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Fullname");
            ViewData["ScholarshipId"] = new SelectList(_context.Scholarships, "ScholarshipId", "ScholarshipName");
            return View(scholarshipStudent);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit([Bind("AccountId,ScholarshipId")] ScholarshipStudent scholarshipStudent)
        {
            if (ModelState.IsValid)
            {
                _context.Update(scholarshipStudent);
                await _context.SaveChangesAsync();
                TempData["msg"] = "<script>alert('Successfully!');</script>";

            }

            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Fullname", scholarshipStudent.AccountId);
            ViewData["ScholarshipId"] = new SelectList(_context.Scholarships, "ScholarshipId", "ScholarshipName", scholarshipStudent.ScholarshipId);
            return View(scholarshipStudent);
        }

    
    }
}
