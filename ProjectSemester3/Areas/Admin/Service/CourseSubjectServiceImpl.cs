using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class CourseSubjectServiceImpl : ICourseSubjectService
    {
        private readonly DatabaseContext context;

        public CourseSubjectServiceImpl(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<dynamic> Create(CourseSubject CourseSubject)
        {
            if (context.CourseSubjects.Any(p => p.SubjectId.Equals(CourseSubject.SubjectId) && p.CourseId.Equals(CourseSubject.CourseId) && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.CourseSubjects.Add(CourseSubject);
                await context.SaveChangesAsync();
                return 1;
            }
        }


        public async Task<CourseSubject> Find(string subjectId, string courseId) => await context.CourseSubjects.FirstOrDefaultAsync(b => b.SubjectId == subjectId && b.CourseId == courseId);


        public async Task<List<CourseSubject>> FindAll() => await context.CourseSubjects.Include(b => b.Subject).Include(b => b.Course).ToListAsync();


        public async Task<dynamic> Update(CourseSubject CourseSubject)
        {
            context.Entry(CourseSubject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return CourseSubject;
        }


        // get all class do not have faculty
        public async Task<List<string>> GetAllCourse(string keyword)
        {
            var result = new List<string>();
            var list = await context.Courses.Where(c => c.CourseName.ToLower().Contains(keyword.ToLower()) && c.Status == true).ToListAsync();

            foreach (var item in list)
            {
                result.Add(item.CourseName);
            }

            return result;
        }

        // Get List Subject
        public List<Subject> GetListSubject(string courseName)
        {
            var result = context.Subjects.Where(c => c.Status == true).ToList();
            var courseId = context.Courses.FirstOrDefault(c => c.CourseName == courseName);


            var listSubjectId = new List<string>();
            try
            {
                listSubjectId = context.CourseSubjects.Where(p => p.CourseId == courseId.CourseId && p.Status == true).Select(p => p.SubjectId).ToList();
            }
            catch (System.Exception)
            {

                return null;
            }
            foreach (var item in listSubjectId)
            {
                var obj = result.FirstOrDefault(s => s.SubjectId == item);
                result.Remove(obj);
            }
            return result;
        }

        public List<CourseSubject> Search(string searchCourseSubject)
        {
            var professionals = context.CourseSubjects.AsQueryable();

            if (searchCourseSubject != null) professionals = professionals.Where(s => s.Subject.SubjectName.Contains(searchCourseSubject));

            if (searchCourseSubject != null) professionals = professionals.Where(b => b.Course.CourseName.Contains(searchCourseSubject) || b.Subject.SubjectName.Contains(searchCourseSubject));

            var result = professionals.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }
    }
}
