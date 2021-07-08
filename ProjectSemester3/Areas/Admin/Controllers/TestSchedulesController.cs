using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectSemester3.Areas.Admin.ViewModel;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using X.PagedList;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("testschedules")]
    [Route("admin/testschedules")]
    public class TestSchedulesController : Controller
    {
        private DatabaseContext context;
        private ITestScheduleService testScheduleService;
        private IAccountService accountService;

        public TestSchedulesController(DatabaseContext context, ITestScheduleService testScheduleService, IAccountService accountService)
        {
            this.context = context;
            this.testScheduleService = testScheduleService;
            this.accountService = accountService;
        }


        // get data to modal edit
        [Route("findajax")]
        public async Task<IActionResult> FindAjax(int testscheduleid)
        {
            var testschedule = await testScheduleService.FindAjax(testscheduleid);
            var testscheduleAjax = new TestSchedule
            {
                TestScheduleId = testschedule.TestScheduleId,
                ClassId = testschedule.ClassId,
                ExamId = testschedule.ExamId,
                Status = testschedule.Status

            };
            return new JsonResult(testscheduleAjax);

        }

        //Find Faculty In Class
        [HttpGet]
        [Route("findFaculty")]
        public IActionResult FindSubject(string examid)
        {
            var listFaculty = testScheduleService.GetListFaculty(examid.Trim());
            if (listFaculty == null)
            {
                return NotFound();
            }
            var result = new List<ListSubjectViewModel>();
            listFaculty.ForEach(s => result.Add(new ListSubjectViewModel
            {
                Id = s.AccountId,
                Name = s.Fullname
            }));
            return new JsonResult(result);
        }


        [Route("index")]
        public IActionResult Index(string searchClassSchedule, int? page, int? pageSize)
        {

            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                var classes =  testScheduleService.Search(searchClassSchedule);
                ViewBag.searchClassSchedule = searchClassSchedule;

                LoadPagination(classes, page, pageSize);

                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        // load pagination
        public void LoadPagination(List<Class> classes, int? page, int? pageSize)
        {
            var testscheduleViewModel = new TestScheduleViewModel();

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
            var onePageOfProducts = classes.ToPagedList(pageNumber, pagesize);

            testscheduleViewModel.PagedList = (PagedList<Class>)onePageOfProducts;

            ViewBag.listClass = testscheduleViewModel;
        }



        [HttpGet]
        [Route("detail")]
        public async Task<IActionResult> Detail(string classid)
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                ViewBag.testschedules = await testScheduleService.SelectTestShedule(classid);
                ViewBag.classitem = await testScheduleService.GetClass(classid);
               


                return View();
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(TestSchedule testSchedule, int maxmark, int rate)
        {
            if (ModelState.IsValid)
            {

                testSchedule.Status = true;
                await testScheduleService.Add(testSchedule);
                await testScheduleService.CreateMarkBySubject(testSchedule.ExamId, maxmark, rate);
                TempData["success"] = "success";



            }
            ViewBag.testschedules = await testScheduleService.SelectTestShedule(testSchedule.ClassId);
            ViewBag.classitem = await testScheduleService.GetClass(testSchedule.ClassId);
          

            return RedirectToRoute(new
            {
                controller = "testschedules",
                action = "detail",
                classid = testSchedule.ClassId
            });
        }



        [HttpGet]
        [Route("delete")]
        public async Task<IActionResult> Delete(int id, string idclass)
        {
            await testScheduleService.Delete(id);
            return RedirectToRoute(new { controller = "testschedules", action = "detail", classid = idclass });


        }
    }
}
