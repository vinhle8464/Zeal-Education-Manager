using System;
using System.Collections.Generic;
using System.Linq;
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
    [Area("Admin")]
    [Route("enquiries")]
    [Route("admin/enquiries")]
    public class EnquiriesController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IEnquiryService enquiryService;
        private readonly IAccountService accountService;

        public EnquiriesController(DatabaseContext context, IEnquiryService enquiryService, IAccountService accountService)
        {
            _context = context;
            this.enquiryService = enquiryService;
            this.accountService = accountService;
        }


        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(int enquiryid)
        {
            var enquiry = await enquiryService.FindAjax(enquiryid);
            var enquiryAjax = new Enquiry
            {
                Id = enquiry.Id,
                Title = enquiry.Title,
                Answer = enquiry.Answer,
                Status = enquiry.Status
            };
            return new JsonResult(enquiryAjax);

        }

        [Route("index")]
        // GET: Admin/Enquiries
        public IActionResult Index(string searchEnquiry, int? page, int? pageSize)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var enquiries = enquiryService.Search(searchEnquiry);
                ViewBag.searchEnquiry = searchEnquiry;

                LoadPagination(enquiries, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }


        // load pagination
        public void LoadPagination(List<Enquiry> enquiries, int? page, int? pageSize)
        {
            var enquiryViewModel = new EnquiryViewModel();

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
            var onePageOfProducts = enquiries.ToPagedList(pageNumber, pagesize);

            enquiryViewModel.PagedList = (PagedList<Enquiry>)onePageOfProducts;

            ViewBag.enquiries = enquiryViewModel;
        }

        [HttpPost]
        [Route("create")]
        public IActionResult Create(EnquiryViewModel enquiryViewModel, string searchEnquiry, int? pageSize)
        {
            if (ModelState.IsValid)
            {
                var currentEnquiry = new Enquiry();
                currentEnquiry.Title = enquiryViewModel.Enquiry.Title.Trim();
                currentEnquiry.Answer = enquiryViewModel.Enquiry.Answer.Trim();
                currentEnquiry.Status = true;
                enquiryService.Create(currentEnquiry);
                TempData["success"] = "success";
                // Return view index and auto paging
                return RedirectToRoute(new { controller = "enquiries", action = "index", searchEnquiry = searchEnquiry, pageSize = pageSize });
            }
            ViewBag.msg = "Create fail";
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "enquiries", action = "index", searchEnquiry = searchEnquiry, pageSize = pageSize });
        }


        // edit course
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit(EnquiryViewModel enquiryViewModel, string searchEnquiry, int? pageSize)
        {

            if (ModelState.IsValid)
            {
                await enquiryService.Update(enquiryViewModel.Enquiry);

                TempData["success"] = "success";

                // Return view index and auto paging
                return RedirectToRoute(new { controller = "enquiries", action = "index", searchEnquiry = searchEnquiry, pageSize = pageSize });
            }
            // Return view index and auto paging
            return RedirectToRoute(new { controller = "enquiries", action = "index", searchEnquiry = searchEnquiry, pageSize = pageSize });
        }


        // delete course
        [HttpPost]
        [Route("delete")]
        public async Task<IActionResult> Delete(EnquiryViewModel enquiryViewModel, string searchEnquiry, int? pageSize)
        {

            enquiryViewModel.Enquiry.Status = false;
            await enquiryService.Update(enquiryViewModel.Enquiry);
            TempData["success"] = "success";

            // Return view index and auto paging
            return RedirectToRoute(new { controller = "enquiries", action = "index", searchEnquiry = searchEnquiry, pageSize = pageSize });
        }

        [Route("detail")]
        public IActionResult Detail(int enquiryid)
        {
            ViewBag.enquiry = enquiryService.Find(enquiryid);
            return View("detail");

        }

    }
}
