using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProjectSemester3.Models;
using ProjectSemester3.Paypal;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("student")]
    [Route("pays")]
    [Route("student/pays")]
    public class PaysController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IAccountService accountService;
        private readonly IPaysService paysService;
        private readonly IConfiguration configuration;

        public PaysController(DatabaseContext context, IAccountService accountService, IPaysService paysService, IConfiguration configuration)
        {
            _context = context;
            this.accountService = accountService;
            this.paysService = paysService;
            this.configuration = configuration;
        }





        // GET: Student/Pays
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleName");
                ViewData["ClassId"] = new SelectList(_context.Classes, "ClassId", "ClassName");


                ViewBag.pays = await paysService.FindAllPayByAccountId(accountService.Find(HttpContext.Session.GetString("username")).AccountId);
                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        // GET: Student/Pays/Details/5
        [Route("details")]
        public async Task<IActionResult> Details(int id)
        {
        
            var pay = await paysService.FindById(id);

            if (pay == null)
            {
                return NotFound();
            }
            ViewBag.pays = await paysService.FindById(id);
            ViewBag.postUrl = configuration["Paypal:PostUrl"];
            ViewBag.business = configuration["Paypal:Business"];
            ViewBag.returnUrl = configuration["Paypal:ReturnUrl"];

            var account = accountService.Find(HttpContext.Session.GetString("username"));
           

            var pay1 = paysService.GetFee(account.AccountId, pay.Title);
            pay1.Total = Convert.ToInt32(pay1.Total);
            if (pay1 == null)
            {
                return NotFound();
            }
            ViewBag.pay = pay1;
           
            HttpContext.Session.SetString("payment", pay1.PayId.ToString());

            return View();
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit([Bind("PayId,AccountId,Payment,Title,Fee,Paid,Extant,Status,DateRequest,DatePaid,NumberDateLate,Total")] Pay pay)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    await paysService.Update(pay);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!paysService.Exists(pay.PayId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", pay.AccountId);
            return View("index", pay);
        }

        

        [Route("success")]
        public async Task<IActionResult> Success([FromQuery(Name = "tx")] string tx)
        {
            if(tx != null && HttpContext.Session.GetString("payment") != null)
            {
                var payId = Int32.Parse(HttpContext.Session.GetString("payment"));
                await paysService.PayFee(payId);
            }
            ViewBag.pays = await paysService.FindAllPayByAccountId(accountService.Find(HttpContext.Session.GetString("username")).AccountId);
            return View("index");
        }
    }
}
