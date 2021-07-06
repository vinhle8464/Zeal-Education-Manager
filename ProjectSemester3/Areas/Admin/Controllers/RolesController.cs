using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using ProjectSemester3.Areas.Admin.Service;
using Microsoft.AspNetCore.Http;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("roles")]
    [Route("admin/roles")]
    public class RolesController : Controller
    {
        private readonly IRolesService roleService;
        private IAccountService accountService;

        public RolesController(IRolesService roleService, IAccountService accountService)
        {
            this.roleService = roleService;
            this.accountService = accountService;
        }



        // Show page role
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.roles = await roleService.FindAll();

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }

        // create role
        [HttpPost]
        [Route("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleName,Desc")] Role role)
        {
            if (ModelState.IsValid)
            {
                // devided char and number
                var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9]*)");
                int num = 0;
                if (roleService.GetNewestId() != null)
                {
                    var match = numAlpha.Match(roleService.GetNewestId());
                    //var alpha = match.Groups["Alpha"].Value;
                    num = Int32.Parse(match.Groups["Numeric"].Value);

                }


                // if have no value with this id in db, we create first 
                // else it already existed in db, we create next
                if (await roleService.CountId() != 0)
                {
                    if (num < 9)
                    {
                        role.RoleId = "role" + "0" + (num + 1);

                    }
                    else
                    {
                        role.RoleId = "role" + (num + 1);

                    }
                    role.RoleName = role.RoleName.Trim();
                    role.Desc = role.Desc.Trim();
                    role.Status = true;
                    if (await roleService.Create(role) == 0)
                    {
                        TempData["msg"] = "<script>alert('Role has already existed!');</script>";
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

                    role.RoleId = "role" + "01";
                    role.RoleName = role.RoleName.Trim();
                    role.Desc = role.Desc.Trim();
                    role.Status = true;
                    await roleService.Create(role);
                    TempData["msg"] = "<script>alert('Successfully!');</script>";

                }

            }
            ViewBag.roles = await roleService.FindAll();
            return RedirectToAction(nameof(Index));

        }

        // open page edit role
        [HttpGet]
        [Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
           

            var role = await roleService.Find(id);
           
            return View("edit", role);
        }

        // edit role in db
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(Role role)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await roleService.Update(role);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!roleService.Exists(role.RoleId))
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
            return View(role);
        }

        // hide role
        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await roleService.Delete(id);
            TempData["msg"] = "<script>alert('Successfully!');</script>";

            return RedirectToAction(nameof(Index));
        }


    }
}
