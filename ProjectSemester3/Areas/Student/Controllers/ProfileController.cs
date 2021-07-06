using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Areas.Student.ViewModels;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using ProjectSemester3.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("student")]
    [Route("profile")]
    [Route("student/profile")]
    public class ProfileController : Controller
    {
        private IProfileService profileService;
        private IAccountService accountService;
        private IWebHostEnvironment webHostEnvironment;

        public ProfileController(IProfileService _profileService, IAccountService _accountService, IWebHostEnvironment _webHostEnvironment)
        {
            profileService = _profileService;
            accountService = _accountService;
            webHostEnvironment = _webHostEnvironment;
        }

        [Route("index")]
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                var account = accountService.Find(HttpContext.Session.GetString("username"));
                var classname = profileService.SelectClass(account.AccountId);
                var coursename = profileService.SelectCourse(account.AccountId);
                ViewBag.classname = classname;
                ViewBag.coursename = coursename;
                return View();
            }
            return null;
        }

        // POST: Change Password
        [HttpPost]
        [Route("changepassword")]
        public async Task<dynamic> ChangePassWord(ChangePassword changePassword)
        {
            if (ModelState.IsValid)
            {
                var OldPassword = await profileService.GetPassword(HttpContext.Session.GetString("username"), changePassword.CurrentPassword);
                if (OldPassword)
                {
                    await profileService.ChangePassword(changePassword.NewPassword, HttpContext.Session.GetString("username"));
                    ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                    //ViewData["success"] = "Password updated successfully!";
                    TempData["msg"] = "<script>alert('Change password succesfully');</script>";
                    return RedirectToAction("Index");

                }
                else
                {
                    ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                    //ViewData["failed"] = "Current Password is Incorrect!";
                    TempData["msg"] = "<script>alert('Current Password is Incorrect!');</script>";
                    return RedirectToAction("Index");

                }
            }
            ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
            return View("index");
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
            return RedirectToAction("Index");
        }

        [Route("editfullname")]
        public IActionResult EditFullname(string editfullname)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                var account = accountService.Find(HttpContext.Session.GetString("username"));
                var classname = profileService.SelectClass(account.AccountId);
                var coursename = profileService.SelectCourse(account.AccountId);
                ViewBag.classname = classname;
                ViewBag.coursename = coursename;

                var currentAccount = accountService.Find(account.Username);
                currentAccount.Username = account.Username;
                currentAccount.Fullname = editfullname;
                currentAccount.Password = account.Password;
                currentAccount.RoleId = account.RoleId;
                currentAccount.ClassId = account.ClassId;
                currentAccount.Email = account.Email;
                currentAccount.Dob = account.Dob;
                currentAccount.Address = account.Address;
                currentAccount.Gender = account.Gender;
                currentAccount.Phone = account.Phone;
                currentAccount.Avatar = account.Avatar;
                currentAccount.Status = account.Status;
                profileService.Edit(currentAccount);

                return RedirectToAction("Index");
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
                var classname = profileService.SelectClass(account.AccountId);
                var coursename = profileService.SelectCourse(account.AccountId);
                ViewBag.classname = classname;
                ViewBag.coursename = coursename;

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

                return RedirectToAction("Index");
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
                var classname = profileService.SelectClass(account.AccountId);
                var coursename = profileService.SelectCourse(account.AccountId);
                ViewBag.classname = classname;
                ViewBag.coursename = coursename;

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

                return RedirectToAction("Index");
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
                var classname = profileService.SelectClass(account.AccountId);
                var coursename = profileService.SelectCourse(account.AccountId);
                ViewBag.classname = classname;
                ViewBag.coursename = coursename;

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

                return RedirectToAction("Index");
            }
            return null;
        }
    }
}
