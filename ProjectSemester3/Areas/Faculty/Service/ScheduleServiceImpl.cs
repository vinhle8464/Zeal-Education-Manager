using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class ScheduleServiceImpl:IScheduleService
    {
        private readonly DatabaseContext context;
        public ScheduleServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }
        public List<Subject> subjects(string facultyid)
        {
            return context.Professionals.Where(m=>m.FacultyId==facultyid).Select(m => m.Subject).ToList();
        }
        public Account getfacultyid(string facultyid)
        {
            return context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }

        public List< Schedule> schedules(string subjectid)
        {
            return context.Schedules.Where(m => m.SubjectId == subjectid).ToList();
        }
        public List<Subject> subjectsofclass(string classid)
        {
            return context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == classid).CourseId).CourseId).Select(m => m.Subject).ToList();
        }
        public List<Exam> exams(string subjectid)
        {


            return context.Exams.Where(m => m.SubjectId == subjectid).ToList();
        }

        public Class getclass(string classid)
        {
            return context.Classes.FirstOrDefault(m => m.ClassId == classid);
        }

        public List<TestSchedule> testSchedules(string examid)
        {
            return context.TestSchedules.Where(m => m.ExamId == examid).ToList();
        }

        public List<Class> classes(string facultyid)
        {
            List<Class> classes = new List<Class>();
            foreach (var classid in context.ClassAssignments.Where(m => m.FacultyId == facultyid).Distinct().Select(n => n.Class.ClassId).ToList())
            {
                classes = classes.Union(context.Classes.Where(m => m.ClassId == classid).ToList()).ToList();
            }
            return classes;
        }
    }
}
