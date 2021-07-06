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
    [Route("scholarships")]
    [Route("admin/scholarships")]
    public class ScholarshipsController : Controller
    {
        private readonly IScholarshipService scholarshipService;
        private readonly IAccountService accountService;

        public ScholarshipsController(IScholarshipService scholarshipService, IAccountService accountService)
        {
            this.scholarshipService = scholarshipService;
            this.accountService = accountService;
        }


        // Show page scholarship
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.scholarships = await scholarshipService.FindAll();

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }



        // create scholarship
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create(Scholarship scholarship)
        {
            if (ModelState.IsValid)
            {
                // devided char and number
                var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
                int num = 0;
                if (scholarshipService.GetNewestId() != null)
                {
                    var match = numAlpha.Match(scholarshipService.GetNewestId());
                    //var alpha = match.Groups["Alpha"].Value;
                    num = Int32.Parse(match.Groups["Numeric"].Value);

                }


                // if have no value with this id in db, we create first 
                // else it already existed in db, we create next
                if (await scholarshipService.CountId() != 0)
                {
                    if (num < 9)
                    {
                        scholarship.ScholarshipId = "scholarship" + "0" + (num + 1);

                    }
                    else
                    {
                        scholarship.ScholarshipId = "scholarship" + (num + 1);

                    }
                    scholarship.ScholarshipName = scholarship.ScholarshipName.Trim();
                    scholarship.Discount = scholarship.Discount.Trim();
                    scholarship.Desc = scholarship.Desc.Trim();
                    scholarship.Status = true;  
                    if (await scholarshipService.Create(scholarship) == 0)
                    {
                        TempData["msg"] = "<script>alert('Scholarship has already existed!');</script>";
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
                    scholarship.ScholarshipId = "scholarship" + "01";
                    scholarship.ScholarshipName = scholarship.ScholarshipName.Trim();
                    scholarship.Discount = scholarship.Discount.Trim();
                    scholarship.Desc = scholarship.Desc.Trim();
                    scholarship.Status = true;
                    await scholarshipService.Create(scholarship);
                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                }

            }
            ViewBag.scholarships = await scholarshipService.FindAll();
            return RedirectToAction(nameof(Index));
        }

        // open page edit scholarship
        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scholarship = await scholarshipService.Find(id);
            if (scholarship == null)
            {
                return NotFound();
            }
            return View("edit", scholarship);
        }

        // edit scholarship in db
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit([Bind("ScholarshipId,ScholarshipName,Discount,Desc,Status")] Scholarship scholarship)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await scholarshipService.Update(scholarship);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!scholarshipService.Exists(scholarship.ScholarshipId))
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
            return View(scholarship);
        }

        // hide scholarship
        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await scholarshipService.Delete(id);
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            return RedirectToAction(nameof(Index));
        }

    }
}
