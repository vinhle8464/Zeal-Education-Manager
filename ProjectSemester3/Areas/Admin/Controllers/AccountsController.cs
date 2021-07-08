using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Areas.Admin.ViewModel;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("accounts")]
    [Route("admin/accounts")]
    public class AccountsController : Controller
    {
        private readonly DatabaseContext context;
        private readonly IAccountService accountService;

        public AccountsController(DatabaseContext _context, IAccountService accountService)
        {
            context = _context;
            this.accountService = accountService;
        }


        // get listClass autocomplete
        [HttpGet]
        [Route("listClass")]
        public async Task<IActionResult> ListClass([FromQuery(Name = "term")] string term)
        {
            var listClass = await accountService.GetAllClass(term);
            return new JsonResult(listClass);

        }

        // get Scholarship autocomplete
        [HttpGet]
        [Route("listScholarship")]
        public async Task<IActionResult> ListScholarship([FromQuery(Name = "term")] string term)
        {
            var listScholarship = await accountService.GetAllScholarship(term);
            return new JsonResult(listScholarship);

        }

        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(string accountid)
        {
            var account = await accountService.FindAjax(accountid);
            var accountAjax = new Account
            {
                RoleId = account.RoleId,
                ClassId = account.ClassId,
                Username = account.Username,
                Fullname = account.Fullname,
                Email = account.Email,
                Dob = account.Dob,
                Address = account.Address,
                Gender = account.Gender,
                Phone = account.Phone,
                Active = account.Active,
                Avatar = account.Avatar
            };
            return new JsonResult(accountAjax);

        }

        // get data autocomplete
        [HttpGet]
        [Route("searchautocomplete")]
        public async Task<IActionResult> SearchByKeyword([FromQuery(Name = "term")] string term)
        {
            var keyword = await accountService.GetKeyWordByKeyword(term);
            return new JsonResult(keyword);

        }


        // GET: Admin/Accounts
        [Route("index")]
        public async Task<IActionResult> Index(string searchKeyword, string roleKeyword, string genderKeyword, string statusKeyword, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var acounts = accountService.Search(searchKeyword, roleKeyword, genderKeyword, statusKeyword);
                ViewBag.searchKeyword = searchKeyword;
                ViewBag.roleKeyword = roleKeyword;
                ViewBag.genderKeyword = genderKeyword;
                ViewBag.statusKeyword = statusKeyword;

               // ViewBag.keyword = await accountService.GetKeyWord();

                // this is a list for form create and edit
                ViewBag.listRole = await context.Roles.Where(r => r.RoleId != "role01").Select(c => c.RoleName).ToListAsync();
                //ViewBag.listCourse = await context.Courses.Select(c => c.CourseName).ToListAsync();
                // this is a list for form create and edit

                 ViewData["RoleId"] = new SelectList(context.Roles.Where(a => a.RoleId != "role01"), "RoleId", "RoleName");

                LoadPagination(acounts, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }

        }

        // load pagination
        public void LoadPagination(List<Account> accounts, int? page, int? pageSize)
        {
            var accountViewModel = new AccountViewModel();

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
            var onePageOfProducts = accounts.ToPagedList(pageNumber, pagesize);

            accountViewModel.PagedList = (PagedList<Account>)onePageOfProducts;

            ViewBag.accounts = accountViewModel;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<dynamic> Create(IFormFile photo, AccountViewModel accountViewModel, string listClassName, string listScholarship, string searchKeyword, string roleKeyword, string genderKeyword, string statusKeyword, int? pageSize)
        {   
            if (ModelState.IsValid)
            {
                var numAlpha = new Regex("(?<Alpha>[a-zA-Z]*)(?<Numeric>[0-9][0-9]*)");
                int numId = 0;
                Class classid = null;

                Scholarship scholarship = null;

                Course course = null;

                if (accountViewModel.Account.RoleId == "role03")
                {


                 
                    if (listClassName != null)
                    {
                        classid = await context.Classes.FirstOrDefaultAsync(c => c.ClassName == listClassName.Trim());
                        if (classid == null)
                        {
                            TempData["msg"] = "<script>alert('Class is not Exist!');</script>";

                            // Return view index and auto paging
                            return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
                        }
                    }
                    var batch = await accountService.GetBatch(classid.ClassId);
             
                    if (batch == null && accountViewModel.Account.RoleId == "role03")
                    {
                        TempData["msg"] = "<script>alert('This Class do not have Batch! Please create a Batch and try again.');</script>";
                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
                    }
                    else
                    {
                        course = await accountService.GetCourse(batch.CourseId);
                    }


                    if (course != null)
                    {
                        // batch = await accountService.GetBatch(classid.ClassId);
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('This Batch has some error with Course! Please try it later.');</script>";
                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });

                    }

                    if (listScholarship != null)
                    {
                        scholarship = await context.Scholarships.FirstOrDefaultAsync(c => c.ScholarshipName == listScholarship.Trim());
                        if (scholarship == null)
                        {
                            TempData["msg"] = "<script>alert('Scholarship is not Exist!');</script>";
                            // Return view index and auto paging
                            return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });

                            // Return view index and auto paging
                            //return RedirectToRoute(new { controller = "batches", action = "index", searchKeyword = searchKeyword, courseKeyword = courseKeyword, classKeyword = classKeyword, pageSize = pageSize });
                        }
                    }
                }


                // devided char and number

                if (accountService.GetNewestId() != null)
                {
                    var match1 = numAlpha.Match(accountService.GetNewestId());
                    //var alpha = match.Groups["Alpha"].Value;

                    numId = Int32.Parse(match1.Groups["Numeric"].Value);

                }


                // if have no value with this id in db, we create first 
                // else it already existed in db, we create next
                if (await accountService.CountId() != 0)
                {
                    if (numId < 9)
                    {
                        accountViewModel.Account.AccountId = "acc" + "0" + (numId + 1);
                    }
                    else
                    {
                        accountViewModel.Account.AccountId = "acc" + (numId + 1);
                    }
                    if (classid != null)
                    {
                        accountViewModel.Account.ClassId = classid.ClassId;

                    }
                    accountViewModel.Account.Username = "account" + accountService.RandomCode(4) + accountService.RandomCode(4);
                    accountViewModel.Account.Fullname = accountViewModel.Account.Fullname.Trim();
                    accountViewModel.Account.Email = accountViewModel.Account.Email.Trim();
                    accountViewModel.Account.Address = accountViewModel.Account.Address.Trim();
                    accountViewModel.Account.Phone = accountViewModel.Account.Phone.Trim();
                    if (accountViewModel.Account.RoleId != "role03")
                    {
                        accountViewModel.Account.ClassId = null;
                    }
                    accountViewModel.Account.Active = false;
                    accountViewModel.Account.Status = false;
                    if (await accountService.Create(photo, accountViewModel.Account) != 0)
                    {
                        if (accountViewModel.Account.RoleId == "role03")
                        {
                            if (scholarship.ScholarshipId != null)
                            {
                                await accountService.CreateScholarshipStudent(accountViewModel.Account.AccountId, scholarship.ScholarshipId);
                            }

                            if (await accountService.CreatePay(accountViewModel.Account.AccountId, course, accountViewModel.Account.Email) == null)
                            {
                                TempData["msg"] = "<script>alert('There are some error in your Batch or Course!');</script>";
                                // Return view index and auto paging
                                return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
                            }

                        }
                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
                    }
                    else
                    {
                        TempData["msg"] = "<script>alert('Account has already existed!');</script>";
                        // Return view index and auto paging
                        return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
                    }
                }
                else
                {
                    accountViewModel.Account.AccountId = "acc" + "01";
                    accountViewModel.Account.ClassId = classid.ClassId;
                    accountViewModel.Account.Username = "account" + accountService.RandomCode(4) + accountService.RandomCode(4);
                    accountViewModel.Account.Fullname = accountViewModel.Account.Fullname.Trim();
                    accountViewModel.Account.Email = accountViewModel.Account.Email.Trim();
                    accountViewModel.Account.Address = accountViewModel.Account.Address.Trim();
                    accountViewModel.Account.Phone = accountViewModel.Account.Phone.Trim();
                    if (accountViewModel.Account.RoleId != "role03")
                    {
                        accountViewModel.Account.ClassId = null;
                    }
                    accountViewModel.Account.Active = false;
                    accountViewModel.Account.Status = false;
                    await accountService.Create(photo, accountViewModel.Account);
                    if (accountViewModel.Account.RoleId == "role03")
                    {
                        if (scholarship.ScholarshipId != null)
                        {
                            await accountService.CreateScholarshipStudent(accountViewModel.Account.AccountId, scholarship.ScholarshipId);
                        }


                        if (await accountService.CreatePay(accountViewModel.Account.AccountId, course, accountViewModel.Account.Email) == null)
                        {
                            TempData["msg"] = "<script>alert('There are some error in your Batch or Course!');</script>";
                            // Return view index and auto paging
                            return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
                        }

                    }
                }
                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
            }
            ViewData["RoleId"] = new SelectList(context.Roles, "RoleId", "RoleName");
            //ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName", account.RoleId);


            //ViewBag.accounts = accountService.Findall();
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(AccountViewModel accountViewModel, string searchKeyword, string roleKeyword, string genderKeyword, string statusKeyword, int? pageSize)
        {


            if (ModelState.IsValid)
            {

                context.Update(accountViewModel.Account);
                await context.SaveChangesAsync();

                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
            }
            ViewData["RoleId"] = new SelectList(context.Roles, "RoleId", "RoleName");
            //ViewData["ClassId"] = new SelectList(context.Classes, "ClassId", "ClassName", account.RoleId);

            return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("delete")]
        public  IActionResult Delete(AccountViewModel accountViewModel, string searchKeyword, string roleKeyword, string genderKeyword, string statusKeyword, int? pageSize)
        {
            var account = context.Accounts.Find(accountViewModel.Account.AccountId);
            account.Status = false;
            accountService.Update(account);
            TempData["success"] = "success";

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "accounts", action = "index", searchKeyword = searchKeyword, roleKeyword = roleKeyword, genderKeyword = genderKeyword, statusKeyword = statusKeyword, pageSize = pageSize });
        }


    }
}
