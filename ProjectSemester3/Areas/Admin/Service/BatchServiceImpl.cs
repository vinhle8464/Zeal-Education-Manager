using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class BatchServiceImpl : IBatchService
    {
        private readonly DatabaseContext context;
        public BatchServiceImpl(DatabaseContext _context)
        {
            context = _context;
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

        public Account getfaculty(string facultyid)
        {
            return context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }

        public Course course(string courseid)
        {
            return context.Courses.FirstOrDefault(m => m.CourseId == courseid);
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
        public List<Account> scholarship(Course course)
        {
            List<Account> accounts = new List<Account>();
            foreach (var cls in context.Batches.Where(m => m.CourseId == course.CourseId).Select(m => m.Class).ToList())
            {
                foreach (var student in cls.Accounts.ToList())
                {
                    accounts = accounts.Union(context.ScholarshipStudents.Where(m => m.AccountId == student.AccountId).Select(m => m.Account).ToList()).ToList();
                }
            }
            return accounts;
        }

        public List<Schedule> schedules(Course course)
        {
            List<Schedule> schedules = new List<Schedule>();
            foreach (var subjects in context.CourseSubjects.Where(m => m.CourseId == course.CourseId).Select(m => m.Subject).ToList())
            {
                schedules = schedules.Union(context.Schedules.Where(m => m.SubjectId == subjects.SubjectId).ToList()).ToList();
            }
            return schedules;
        }
    }
}
