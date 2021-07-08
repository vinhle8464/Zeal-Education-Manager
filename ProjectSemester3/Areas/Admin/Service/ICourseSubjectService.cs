using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface ICourseSubjectService
    {
        public Task<CourseSubject> Find(string courseId, string subjectId);
        public Task<List<CourseSubject>> FindAll();
        public Task<dynamic> Create(CourseSubject curseSubject);
        public Task<dynamic> Update(CourseSubject courseSubject);

        public Task<List<string>> GetAllCourse(string keyword);
        public List<Subject> GetListSubject(string courseName);
        public List<CourseSubject> Search(string searchCourseSubject);

    }
}
