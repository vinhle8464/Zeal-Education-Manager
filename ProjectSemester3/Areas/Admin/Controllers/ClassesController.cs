using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using ProjectSemester3.Areas.Admin.Service;
using ProjectSemester3.Services;
using Microsoft.AspNetCore.Http;

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



        // Show page class
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.classes = await classesService.FindAll();
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

           
        }

        // create class
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Class classes)
        {
            if (ModelState.IsValid & classes != null)
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
                        classes.ClassId = "class" + "0" + (num + 1);

                    }
                    else
                    {
                        classes.ClassId = "class" + (num + 1);

                    }
                    classes.Status = true;
                    classes.ClassName = classes.ClassName.Trim();
                    classes.NumberOfStudent = classes.NumberOfStudent;
                    classes.Desc = classes.Desc.Trim();

                    if (await classesService.Create(classes) == 0)
                    {
                        TempData["msg"] = "<script>alert('Class has already existed!');</script>";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));

                    }
                   

                }
                else
                {
                    classes.ClassId = "class" + "01";
                    classes.Status = true;
                    classes.ClassName = classes.ClassName.Trim();
                    classes.NumberOfStudent = classes.NumberOfStudent;
                    classes.Desc = classes.Desc.Trim();
                    await classesService.Create(classes);
                }
                TempData["msg"] = "<script>alert('Successfully!');</script>";

                return RedirectToAction(nameof(Index));
            }
            ViewBag.classes = await classesService.FindAll();
            return RedirectToAction(nameof(Index));

        }

        // open page edit class
        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classes = await classesService.Find(id);
            if (classes == null)
            {
                return NotFound();
            }
            return View("edit", classes);
        }

        // edit class in db
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(Class classes)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await classesService.Update(classes);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!classesService.RoleExists(classes.ClassId))
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
            return View(classes);
        }

        // hide class
        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await classesService.Delete(id);
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            return RedirectToAction(nameof(Index));
        }


    }
}
