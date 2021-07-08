using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("dashboard")]
    [Route("admin/dashboard")]
    public class DashBoardController : Controller
    {
        private IProfileService profileService;
        private IAccountService accountService;
        private IWebHostEnvironment webHostEnvironment;
        private DatabaseContext context;

        public DashBoardController(IProfileService profileService, IAccountService accountService, IWebHostEnvironment webHostEnvironment, DatabaseContext _context)
        {
            this.profileService = profileService;
            this.accountService = accountService;
            this.webHostEnvironment = webHostEnvironment;
            context = _context;
        }

        [Route("index")]
        [Route("")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.faculty = accountService.Find(HttpContext.Session.GetString("username"));
                ViewBag.totalstudent = context.Accounts.Where(m=>m.RoleId=="role03").ToList().Count();
                ViewBag.totalfaculty= context.Accounts.Where(m => m.RoleId == "role02").ToList().Count();
                ViewBag.totalmoney= context.Pays.Where(m => m.PayStatus == true).Sum(m=>m.Total);
                ViewBag.totalmail= context.Mail.ToList().Count();
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }

        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
            return View("Profile");
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("role");
            HttpContext.Session.Remove("username");
            Response.Cookies.Delete("username");
            Response.Cookies.Delete("password");
            return RedirectToRoute(new { controller = "account", action = "signin" });
        }

        [HttpPost]
        [Route("changeavatar")]
        public IActionResult ChangeAvatar(IFormFile file)
        {
            var account = accountService.Find(HttpContext.Session.GetString("username"));
            var currentAccount = accountService.Find(account.Username);
            if (file != null)
            {
                string filename = Guid.NewGuid().ToString();
                var ext = file.ContentType.Split(new char[] { '/' })[1];
                var path = Path.Combine(webHostEnvironment.WebRootPath, "images", filename + "." + ext);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                currentAccount.Avatar = filename + "." + ext;
            }
            currentAccount.Username = account.Username;
            currentAccount.Fullname = account.Fullname;
            currentAccount.Password = account.Password;
            currentAccount.RoleId = account.RoleId;
            currentAccount.ClassId = account.ClassId;
            currentAccount.Email = account.Email;
            currentAccount.Dob = account.Dob;
            currentAccount.Address = account.Address;
            currentAccount.Gender = account.Gender;
            currentAccount.Phone = account.Phone;
            currentAccount.Status = account.Status;

            profileService.ChangeAvatar(currentAccount);
            return RedirectToAction("profile");
        }

        [Route("editfullname")]
        public IActionResult EditFullname(string editfullname)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                var account = accountService.Find(HttpContext.Session.GetString("username"));

                var currentAccount = accountService.Find(account.Username);
                currentAccount.Username = account.Username;
                currentAccount.Fullname = editfullname;
                currentAccount.Password = account.Password;
                currentAccount.RoleId = account.RoleId;
                //currentAccount.ClassId = account.ClassId;
                currentAccount.Email = account.Email;
                currentAccount.Dob = account.Dob;
                currentAccount.Address = account.Address;
                currentAccount.Gender = account.Gender;
                currentAccount.Phone = account.Phone;
                currentAccount.Avatar = account.Avatar;
                currentAccount.Status = account.Status;
                profileService.Edit(currentAccount);

                return RedirectToAction("profile");
            }
            return null;
        }

        [Route("editdob")]
        public IActionResult EditDob(DateTime editdob)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                var account = accountService.Find(HttpContext.Session.GetString("username"));

                var currentAccount = accountService.Find(account.Username);
                currentAccount.Username = account.Username;
                currentAccount.Fullname = account.Fullname;
                currentAccount.Password = account.Password;
                currentAccount.RoleId = account.RoleId;
                currentAccount.ClassId = account.ClassId;
                currentAccount.Email = account.Email;
                currentAccount.Dob = editdob;
                currentAccount.Address = account.Address;
                currentAccount.Gender = account.Gender;
                currentAccount.Phone = account.Phone;
                currentAccount.Avatar = account.Avatar;
                currentAccount.Status = account.Status;
                profileService.Edit(currentAccount);

                return RedirectToAction("profile");
            }
            return null;
        }

        [Route("editemail")]
        public IActionResult EditEmail(string editemail)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                var account = accountService.Find(HttpContext.Session.GetString("username"));

                var currentAccount = accountService.Find(account.Username);
                currentAccount.Username = account.Username;
                currentAccount.Fullname = account.Fullname;
                currentAccount.Password = account.Password;
                currentAccount.RoleId = account.RoleId;
                currentAccount.ClassId = account.ClassId;
                currentAccount.Email = editemail;
                currentAccount.Dob = account.Dob;
                currentAccount.Address = account.Address;
                currentAccount.Gender = account.Gender;
                currentAccount.Phone = account.Phone;
                currentAccount.Avatar = account.Avatar;
                currentAccount.Status = account.Status;
                profileService.Edit(currentAccount);

                return RedirectToAction("profile");
            }
            return null;
        }

        [Route("editphone")]
        public IActionResult EditPhone(string editphone)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                var account = accountService.Find(HttpContext.Session.GetString("username"));

                var currentAccount = accountService.Find(account.Username);
                currentAccount.Username = account.Username;
                currentAccount.Fullname = account.Fullname;
                currentAccount.Password = account.Password;
                currentAccount.RoleId = account.RoleId;
                currentAccount.ClassId = account.ClassId;
                currentAccount.Email = account.Email;
                currentAccount.Dob = account.Dob;
                currentAccount.Address = account.Address;
                currentAccount.Gender = account.Gender;
                currentAccount.Phone = editphone;
                currentAccount.Avatar = account.Avatar;
                currentAccount.Status = account.Status;
                profileService.Edit(currentAccount);

                return RedirectToAction("profile");
            }
            return null;
        }
    }
}
