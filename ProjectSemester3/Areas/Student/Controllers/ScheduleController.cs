using Microsoft.AspNetCore.Http;
using ProjectSemester3.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ProjectSemester3.Areas.Student.Controllers
{
    [Area("student")]
    [Route("schedule")]
    [Route("student/schedule")]
    public class ScheduleController : Controller
    {
        private IAccountService accountService;
        private IScheduleService scheduleService;

        public ScheduleController(IAccountService _accountService, IScheduleService _scheduleService)
        {
            accountService = _accountService;
            scheduleService = _scheduleService;
        }
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.schedule = await scheduleService.SelectShedule(accountService.Find(HttpContext.Session.GetString("username")).ClassId);
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));
                return View();
            }
            return null;
        }

        [Route("detail")]
        public async Task<IActionResult> Detail(string subjectid)
        {
            if (HttpContext.Session.GetString("username") != null)
            {
                ViewBag.schedule = await scheduleService.SelectShedule(accountService.Find(HttpContext.Session.GetString("username")).ClassId);
                ViewBag.account = accountService.Find(HttpContext.Session.GetString("username"));

                var student = accountService.Find(HttpContext.Session.GetString("username"));

                var schedule = await scheduleService.GetBySubjectId(subjectid, student.ClassId);

                var startdate = schedule.StartDate;
                var enddate = schedule.EndDate;

                var days = new List<string>();
                var monday = new List<string>();
                var tuesday = new List<string>();
                var wednesday = new List<string>();
                var thursday = new List<string>();
                var friday = new List<string>();
                var saturday = new List<string>();
                var sunday = new List<string>();


                if (startdate == null || enddate == null || schedule.StudyDay == null)
                {
                    return View("Index");
                }
                else
                {
                    string[] studyday = schedule.StudyDay.Split(',');
                    while (enddate >= startdate)
                    {
                        foreach (var day in studyday)
                        {
                            if (startdate.ToString("dddd") == day)
                            {
                                days.Add(day + " " + startdate.Date.ToString("dd/MM/yyyy"));
                            }
                        }
                        startdate = startdate.AddDays(1);
                    }

                    foreach (var day in days)
                    {
                        if (day.Contains("Monday"))
                        {
                            monday.Add(day);
                        }
                        else if (day.Contains("Tuesday"))
                        {
                            tuesday.Add(day);
                        }
                        else if (day.Contains("Wednesday"))
                        {
                            wednesday.Add(day);
                        }
                        else if (day.Contains("Thursday"))
                        {
                            thursday.Add(day);
                        }
                        else if (day.Contains("Friday"))
                        {
                            friday.Add(day);
                        }
                        else if (day.Contains("Saturday"))
                        {
                            saturday.Add(day);
                        }
                        else if (day.Contains("Sunday"))
                        {
                            sunday.Add(day);
                        }
                    }
                }
                ViewBag.monday = monday;
                ViewBag.tuesday = tuesday;
                ViewBag.wednesday = wednesday;
                ViewBag.thursday = thursday;
                ViewBag.friday = friday;
                ViewBag.saturday = saturday;
                ViewBag.sunday = sunday;
                ViewBag.days = days;

                ViewBag.schedule = await scheduleService.GetBySubjectId(subjectid, student.ClassId);
                return View("detail");
            }
            return null;
        }
    }
}
