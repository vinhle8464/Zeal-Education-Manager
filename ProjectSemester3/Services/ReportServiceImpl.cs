using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class ReportServiceImpl : IReportService
    {
        private DatabaseContext context;

        public ReportServiceImpl(DatabaseContext context)
        {
            this.context = context;
        }

        public List<Batch> GetCourse(string studentid)
        {
            var classid = context.Accounts.SingleOrDefault(c => c.AccountId == studentid).ClassId;
            var batch = context.Batches.Where(b => b.ClassId == classid).ToList();
            return batch;
        }

        public List<ScholarshipStudent> GetScholarships(string studentid)
        {
            return context.ScholarshipStudents.Where(s => s.AccountId == studentid).ToList();
        }

        public List<Attendance> GetDiligence(string studentid)
        {
            return context.Attendances.Where(a => a.StudentId == studentid).ToList();
        }

        public List<CourseSubject> GetSubject(string id)
        {
            var courseid = context.Batches.SingleOrDefault(c => c.ClassId == id).CourseId;
            var courses = context.CourseSubjects.Where(s => s.CourseId == courseid).ToList();
            return courses;
        }

        public List<Attendance> GetAttendances(string subjectid, string studentid)
        {
            return context.Attendances.Where(a => a.SubjectId == subjectid && a.StudentId == studentid).ToList();
        }

        public int GetDateOff(string subjectid, string studentid)
        {
            return context.Attendances.Count(a => a.Checked == false && a.SubjectId == subjectid && a.StudentId == studentid);
        }

        public List<Attendance> GetAttendances(string studentid)
        {
            return context.Attendances.Where(a => a.StudentId == studentid).ToList();
        }

        public async Task<string> GetStatusSubject(string classid, string studentid, string subjectid)
        {
            var db = await context.Attendances.CountAsync(a => a.ClassId == classid && a.StudentId == studentid && a.SubjectId == subjectid);
            var checktrue = await context.Attendances.CountAsync(a => a.ClassId == classid && a.StudentId == studentid && a.SubjectId == subjectid && a.Checked == true);
            if (checktrue >= 0 && checktrue < db)
            {
                return "Studying";
            }
            else if (checktrue > 0 && checktrue == db)
            {
                return "Completed";
            }
            else if (checktrue == 0)
            {
                return "Scheduled";
            }
            return null;
        }

        public async Task<List<CourseSubject>> GetCourseSubject(string coursename)
        {
            return await context.CourseSubjects.Where(c => c.Course.CourseName == coursename).ToListAsync();
        }

        public async Task<Batch> GetDateAttendance(string classid)
        {
            return await context.Batches.SingleOrDefaultAsync(b => b.ClassId == classid);
        }

        public async Task<List<Pay>> GetPayByStudent(string studentid)
        {
            return await context.Pays.Where(p => p.AccountId == studentid).ToListAsync();
        }
    }
}
