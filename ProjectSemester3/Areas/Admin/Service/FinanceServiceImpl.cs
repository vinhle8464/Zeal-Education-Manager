using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class FinanceServiceImpl:IFinanceService
    {
        private readonly DatabaseContext context;
        public FinanceServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }
        public Account getfaculty(string facultyid)
        {
            return context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }

        public Course course(string courseid)
        {
            return context.Courses.FirstOrDefault(m => m.CourseId == courseid);
        }
        public List<Course> courses()
        {
            List<Course> courses = new List<Course>();
            foreach (var courseid in context.Batches.Select(m => m.CourseId).Distinct().ToList())
            {
                courses = courses.Union(context.Courses.Where(m => m.CourseId == courseid).ToList()).ToList();
            }
            return courses;
        }
        public List<Account> students(Course course)
        {
            List<Account> accounts = new List<Account>();
            foreach (var classid in course.Batches.ToList())
            {
                accounts = accounts.Union(context.Accounts.Where(m => m.ClassId == classid.ClassId).ToList()).ToList();
            }
            return accounts;
        }
    }
}
