using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using ProjectSemester3.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IMailService mailService;
        private IWebHostEnvironment webHostEnvironment;
        private DatabaseContext context;

        public AccountController(IAccountService accountService, IMailService mailService, IWebHostEnvironment webHostEnvironment, DatabaseContext context)
        {
            this.accountService = accountService;
            this.mailService = mailService;
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }



        // GET: Signin
        [HttpGet]
        [Route("signin")]
        [Route("~/")]
        public IActionResult Signin()
        {
            if (HttpContext.Request.Cookies["username"] != null && HttpContext.Request.Cookies["password"] != null)
            {
                ViewBag.username = HttpContext.Request.Cookies["username"].ToString();
                ViewBag.password = HttpContext.Request.Cookies["password"].ToString();
            }
            if (HttpContext.Session.GetString("role") == null)
            {
                return View();
            }
            else
            {
                if (HttpContext.Session.GetString("role") == "student")
                {
                    return RedirectToRoute(new { area = "student", controller = "dashboard", action = "index" });
                }
                else if (HttpContext.Session.GetString("role") == "admin")
                {
                    return RedirectToRoute(new { area = "admin", controller = "dashboard", action = "index" });
                }
                else if (HttpContext.Session.GetString("role") == "faculty")
                {
                    return RedirectToRoute(new { area = "faculty", controller = "dashboard", action = "index" });
                }
            }

            return View("Signin");
        }
        // POST: Signin
        [HttpPost]
        [Route("signin")]
        public async Task<dynamic> Signin(bool rememberme, string username, string password)
        {
            if (ModelState.IsValid && rememberme && username != null && password != null)
            {
                HttpContext.Response.Cookies.Append("username", username, new CookieOptions { Expires = DateTime.Now.AddDays(30) });
                HttpContext.Response.Cookies.Append("password", password, new CookieOptions { Expires = DateTime.Now.AddDays(30) });
                var result = await accountService.Signin(username, password);
                if (result == "success")
                {
                    HttpContext.Session.SetString("username", username);
                    HttpContext.Session.SetString("role", accountService.GetRole(username));
                    if (accountService.GetRole(username) == "student" && HttpContext.Session.GetString("username") != null)
                    {
                        return RedirectToRoute(new { area = "student", controller = "dashboard", action = "index" });
                    }
                    else if (accountService.GetRole(username) == "admin" && HttpContext.Session.GetString("username") != null)
                    {
                        return RedirectToRoute(new { area = "admin", controller = "dashboard", action = "index" });
                    }
                    else if (accountService.GetRole(username) == "faculty" && HttpContext.Session.GetString("username") != null)
                    {
                        return RedirectToRoute(new { area = "faculty", controller = "dashboard", action = "index" });
                    }
                    return View("Signin");
                }
                else if (result == "fail")
                {
                    TempData["msg"] = "<script>alert('Your username or password is not valid!');</script>";

                    return View("Signin");
                }
                else
                {
                    HttpContext.Session.SetString("username", username);

                    return View("ChangePassFirstTime");
                }
            }
            else if (ModelState.IsValid && rememberme == false && username != null && password != null)
            {
                HttpContext.Response.Cookies.Append("username", username, new CookieOptions { Path = "/Cookies", Expires = DateTime.Now.AddDays(-1) });
                HttpContext.Response.Cookies.Append("password", password, new CookieOptions { Path = "/Cookies", Expires = DateTime.Now.AddDays(-1) });
                var result = await accountService.Signin(username, password);
                if (result == "success")
                {
                    HttpContext.Session.SetString("username", username);
                    HttpContext.Session.SetString("role", accountService.GetRole(username));
                    if (accountService.GetRole(username) == "student" && HttpContext.Session.GetString("username") != null)
                    {
                        return RedirectToRoute(new { area = "student", controller = "dashboard", action = "index" });
                    }
                    else if (accountService.GetRole(username) == "admin" && HttpContext.Session.GetString("username") != null)
                    {
                        return RedirectToRoute(new { area = "admin", controller = "dashboard", action = "index" });
                    }
                    else if (accountService.GetRole(username) == "faculty" && HttpContext.Session.GetString("username") != null)
                    {
                        return RedirectToRoute(new { area = "faculty", controller = "dashboard", action = "index" });
                    }
                    return View("Signin");
                }
                else if (result == "fail")
                {
                    TempData["msg"] = "<script>alert('Your username or password is not valid!');</script>";

                    return View("Signin");
                }
                else
                {
                    HttpContext.Session.SetString("username", username);

                    return View("ChangePassFirstTime");
                }
            }
            return View("Signin");
        }


        // GET: Forgot password
        [HttpGet]
        [Route("forgotpassword")]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
        // POST: Forgot password
        [HttpPost]
        [Route("forgotpassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (await accountService.FindEmail(email) == null)
            {
                ViewBag.msg1 = "Email is not Exist!";
                return View("ForgotPassword");

            }
            else
            {
                HttpContext.Response.Cookies.Append("code", mailService.SendCodeForgotPass(email), new CookieOptions { Expires = DateTime.Now.AddMinutes(1) });
                ViewBag.email = email;
                return View("ForgotPassword");
            }



        }

        // POST: CheckCode
        [HttpPost]
        [Route("checkcode")]
        public IActionResult CheckCode(string codeconfirm, string email)
        {
            var code = HttpContext.Request.Cookies["code"];
            var changepassbyemail = new ChangePasswordByEmail();
            changepassbyemail.Email = email;
            if (codeconfirm == code)
            {
                ViewBag.email = email;
                return View("ChangePassword", changepassbyemail);
            }
            else
            {
                ViewBag.code = code;
                ViewBag.email = email;
                ViewBag.msg = "Your Code is Incorrect!";
                return View("ForgotPassword");
            }

        }

        // GET: Change Password By Email 
        [HttpGet]
        [Route("changepasswordbyemail")]
        public IActionResult ChangePasswordByEmail()
        {
            return View("ChangePassword");
        }


        // POST: ChangePasswordByEmail
        [HttpPost]
        [Route("changepasswordbyemail")]
        public async Task<dynamic> ChangePassWordByEmail(ChangePasswordByEmail changePassword)
        {
            if (ModelState.IsValid)
            {
                await accountService.ChangePasswordByEmail(changePassword.Password, changePassword.Email);
                ViewData["success"] = "Password updated successfully!";

                return View("ChangePassword");
            }
            else
            {
                ViewBag.email = changePassword.Email;

                return View("ChangePassword");
            }


        }

        // GET: Change Password By Username 
        [HttpGet]
        [Route("changepasswordbyusername")]
        public IActionResult ChangePasswordByUsername()
        {
            return View("ChangePassword");
        }

        // POST: Change Password By Username
        [HttpPost]
        [Route("changepasswordbyusername")]
        public async Task<dynamic> ChangePassWordByUsername(ChangePasswordByUsername changePassword)
        {
            if (ModelState.IsValid)
            {
                await accountService.ChangePasswordByUsername(changePassword.Password, HttpContext.Session.GetString("username"));
                TempData["msg"] = "<script>alert('Successfully!');</script>";
                return View("Signin");
            }
            else
            {
                return View("ChangePassFirstTime");
            }
        }


        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            var account = accountService.Find(HttpContext.Session.GetString("username"));
            return View("Profile", account);
        }



        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(string id)
        {
            return View("EditProfile", accountService.FindID(id));
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(Account account, IFormFile file)
        {
            var currentAccount = accountService.FindID(account.AccountId);
            if (!string.IsNullOrEmpty(account.Password))
            {
                currentAccount.Password = BCrypt.Net.BCrypt.HashString(account.Password);
            }
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
            currentAccount.Email = account.Email;
            currentAccount.Dob = account.Dob;
            currentAccount.Phone = account.Phone;
            currentAccount.Status = account.Status;
            // currentAccount.Role = account.Role;
            accountService.Update(currentAccount);
            return RedirectToAction("Index");
        }
    }
}
