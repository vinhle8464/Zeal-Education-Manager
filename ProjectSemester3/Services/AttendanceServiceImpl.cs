using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class AttendanceServiceImpl : IAttendanceService
    {
        private readonly DatabaseContext context;
        public AttendanceServiceImpl(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<List<Attendance>> GetAttendancesBySubject(string subjectid, string studentid, string classid)
        {
            return await context.Attendances.Where(a => a.SubjectId == subjectid && a.StudentId == studentid && a.ClassId == classid).ToListAsync();
        }

        public async Task<List<CourseSubject>> GetSubject(string classid)
        {
            var courseid = context.Batches.SingleOrDefault(c => c.ClassId == classid).CourseId;
            var courses = await context.CourseSubjects.Where(s => s.CourseId == courseid).ToListAsync();
            return courses;
        }

        public string GetTimeDay(string studentid, string subjectid)
        {
            var subjectidattendance = context.Attendances.SingleOrDefault(a => a.StudentId == studentid && a.SubjectId == subjectid).SubjectId;
            var subjectidsubject = context.Subjects.SingleOrDefault(s => s.SubjectId == subjectidattendance).SubjectId;
            var timeday = context.Schedules.SingleOrDefault(s => s.SubjectId == subjectidsubject).TimeDay;

            return timeday.ToString();
        }

        public async Task<List<Attendance>> SelectForStudent(string studentid)
        {
            return await context.Attendances.Where(a => a.StudentId == studentid).ToListAsync();
        }
    }
}
