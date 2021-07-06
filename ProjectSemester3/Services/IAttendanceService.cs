using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IAttendanceService
    {
        public Task<List<Attendance>> SelectForStudent(string studentid);
        public Task<List<CourseSubject>> GetSubject(string classid);
        public Task<List<Attendance>> GetAttendancesBySubject(string subjectid, string studentid, string classid);
        public string GetTimeDay(string studentid, string subjectid);
    }
}
