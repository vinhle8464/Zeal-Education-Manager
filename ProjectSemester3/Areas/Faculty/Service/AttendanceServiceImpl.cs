using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class AttendanceServiceImpl : IAttendanceService
    {
        private DatabaseContext context;
        public AttendanceServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public List<Attendance> attendances(string subjectid,string classid)
        {
            return context.Attendances.Where(m => m.SubjectId==context.Subjects.FirstOrDefault(m=>m.SubjectId==subjectid).SubjectId&&m.ClassId==classid).ToList();
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

        public List<Attendance> search(string subjectid,string search,string classid)
        {
            string mydate = null;
            DateTime date2;
            bool check = DateTime.TryParse(search, out date2);
            if (check)
            {
                mydate =date2.ToString("MM/dd/yyyy");
            }

            return context.Attendances.Where(m => m.Date.Equals(DateTime.Parse( mydate))&&m.ClassId==classid&& m.SubjectId == context.Subjects.FirstOrDefault(m => m.SubjectId == subjectid).SubjectId).ToList();
        }

        public List<Subject> subjects(string classid)
        {
            return context.CourseSubjects.Where(m => m.CourseId==context.Courses.FirstOrDefault(m=>m.CourseId==context.Batches.FirstOrDefault(m=>m.ClassId==context.Classes.FirstOrDefault(m=>m.ClassId==context.Accounts.FirstOrDefault(m=>m.ClassId==classid).ClassId).ClassId).CourseId).CourseId).Select(m => m.Subject).ToList();
            
        }

        public dynamic update(int attendanceid,string check)
        {
            Attendance attendance = context.Attendances.FirstOrDefault(m => m.AttendanceId == attendanceid);
            if (check == "present")
            {
                attendance.Checked = true;
            }else if (check == "apsent")
            {
                attendance.Checked = false;
            }
            context.Entry(attendance).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           return context.SaveChanges();
        }
    }
}
