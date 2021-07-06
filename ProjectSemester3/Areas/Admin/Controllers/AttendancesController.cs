using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using ProjectSemester3.Services;

namespace ProjectSemester3.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("attendances")]
    [Route("admin/attendances")]
    public class AttendancesController : Controller
    {
        private readonly DatabaseContext _context;
        private IAccountService accountService;

        public AttendancesController(DatabaseContext context, IAccountService accountService)
        {
            _context = context;
            this.accountService = accountService;
        }



        // GET: Admin/Attendances
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("username") != null && HttpContext.Session.GetString("role") != null)
            {
                 var databaseContext = _context.Attendances.Include(a => a.Faculty).Include(a => a.Student).Include(a => a.Subject);
                return View(await databaseContext.ToListAsync());
            }
            else
            {
                return RedirectToRoute(new { controller = "account", action = "signin" });
            }


           
        }

        [HttpPost]
        [Route("test")]
        public void Test(string Monday, string Tuesday, string Wednesday, string Thursday, string Friday, string Saturday, string Sunday)
        {
           
            //====================
            DateTime start = new DateTime(2021, 01, 01);
            DateTime end = new DateTime(2021, 01, 31);

            ///============
            var list = new List<string>();
            if (Monday != null)
            {
                list.Add("Monday");
            }
            if(Wednesday != null)
            {
                list.Add("Wednesday");
            }
            if (Tuesday != null)
            {
                list.Add("Tuesday");
            }
            if (Thursday != null)
            {
                list.Add("Thursday");
            }
            if (Friday != null)
            {
                list.Add("Friday");
            }
            if (Saturday != null)
            {
                list.Add("Saturday");
            }
            if (Sunday != null)
            {
                list.Add("Sunday");
            }

            
                if (list.Count() == 1)
                {
                while (end >= start)
                {
                    if (start.DayOfWeek.ToString() == list[0])
                    {
                        Debug.WriteLine(start.DayOfWeek + " " + start.Date + "\n");

                    }
                    start = start.AddDays(1);
                } 
                }
                if (list.Count() == 2)
                {
                while (end >= start)
                {
                    if (start.DayOfWeek.ToString() == list[0] || start.DayOfWeek.ToString() == list[1])
                    {
                        Debug.WriteLine(start.DayOfWeek + " " + start.Date + "\n");

                    }
                    start = start.AddDays(1);
                }
                }
                if (list.Count() == 3)
                {
                while (end >= start)
                {
                    if (start.DayOfWeek.ToString() == list[0] || start.DayOfWeek.ToString() == list[1] || start.DayOfWeek.ToString() == list[2])
                    {
                        Debug.WriteLine(start.DayOfWeek + " " + start.Date + "\n");
                    }
                    start = start.AddDays(1);
                }
                }

                if (list.Count() == 4)
                {
                while (end >= start)
                {
                    if (start.DayOfWeek.ToString() == list[0] || start.DayOfWeek.ToString() == list[1] || start.DayOfWeek.ToString() == list[2] || start.DayOfWeek.ToString() == list[3])
                    {
                        Debug.WriteLine(start.DayOfWeek + " " + start.Date + "\n");

                    }
                    start = start.AddDays(1);
                }   
                }
                if (list.Count() == 5)
                {
                while (end >= start)
                {
                    if (start.DayOfWeek.ToString() == list[0] || start.DayOfWeek.ToString() == list[1] || start.DayOfWeek.ToString() == list[2] || start.DayOfWeek.ToString() == list[3] || start.DayOfWeek.ToString() == list[4])
                    {
                        Debug.WriteLine(start.DayOfWeek + " " + start.Date + "\n");

                    }
                    start = start.AddDays(1);
                }
                }
                if (list.Count() == 6)
                {
                while (end >= start)
                {
                    if (start.DayOfWeek.ToString() == list[0] || start.DayOfWeek.ToString() == list[1] || start.DayOfWeek.ToString() == list[2] || start.DayOfWeek.ToString() == list[3] || start.DayOfWeek.ToString() == list[4] || start.DayOfWeek.ToString() == list[5])
                    {
                        Debug.WriteLine(start.DayOfWeek + " " + start.Date + "\n");

                    }
                    start = start.AddDays(1);
                }  
                }
            if (list.Count() == 7)
            {
                while (end >= start)
                {
                    if (start.DayOfWeek.ToString() == list[0] || start.DayOfWeek.ToString() == list[1] || start.DayOfWeek.ToString() == list[2] || start.DayOfWeek.ToString() == list[3] || start.DayOfWeek.ToString() == list[4] || start.DayOfWeek.ToString() == list[5] || start.DayOfWeek.ToString() == list[6])
                    {
                        Debug.WriteLine(start.DayOfWeek + " " + start.Date + "\n");

                    }
                    start = start.AddDays(1);
                }
            }
        }

        // GET: Admin/Attendances/Details/5
        public async Task<IActionResult> Details(int id)
        {
           
            var attendance = await _context.Attendances
                .Include(a => a.Faculty)
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // GET: Admin/Attendances/Create
        public IActionResult Create()
        {
            ViewData["FacultyId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            ViewData["StudentId"] = new SelectList(_context.Accounts, "AccountId", "AccountId");
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId");
            return View();
        }

        // POST: Admin/Attendances/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttendanceId,ClassId,StudentId,FacultyId,SubjectId,Date,Checked")] Attendance attendance)
        {
            if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FacultyId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", attendance.FacultyId);
            ViewData["StudentId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", attendance.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", attendance.SubjectId);
            return View(attendance);
        }

        // GET: Admin/Attendances/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            ViewData["FacultyId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", attendance.FacultyId);
            ViewData["StudentId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", attendance.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", attendance.SubjectId);

            return View(attendance);
        }

        // POST: Admin/Attendances/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AttendanceId,ClassId,StudentId,FacultyId,SubjectId,Date,Checked")] Attendance attendance)
        {
            if (id != attendance.AttendanceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AttendanceId))
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
            ViewData["FacultyId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", attendance.FacultyId);
            ViewData["StudentId"] = new SelectList(_context.Accounts, "AccountId", "AccountId", attendance.StudentId);
            ViewData["SubjectId"] = new SelectList(_context.Subjects, "SubjectId", "SubjectId", attendance.SubjectId);
            return View(attendance);
        }

        // GET: Admin/Attendances/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            

            var attendance = await _context.Attendances
                .Include(a => a.Faculty)
                .Include(a => a.Student)
                .Include(a => a.Subject)
                .FirstOrDefaultAsync(m => m.AttendanceId == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Admin/Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            _context.Attendances.Remove(attendance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttendanceExists(int id)
        {
            return _context.Attendances.Any(e => e.AttendanceId == id);
        }
    }
}
