using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class CoursesServiceImpl : ICoursesService
    {
        private readonly DatabaseContext context;
        public CoursesServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<int> CountId()
        {
           return await context.Courses.Where(p => p.Status == true).CountAsync();
        }

       // public async Task<int> CountIdById(string RoleId) => await _context.Roles.Where(p => p.RoleId.Contains(RoleId) && p.Status == true).CountAsync();

        public async Task<dynamic> Create(Course course)
        {
            if (context.Courses.Any(p => p.CourseName == course.CourseName && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.Courses.Add(course);
              return await context.SaveChangesAsync();
            }
            
        }

        public async Task Delete(string CourseId)
        {
            var course = context.Courses.Find(CourseId);
            course.Status = false;
            context.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

       public async Task<Course> Find(string CourseId) => await context.Courses.FirstOrDefaultAsync(p => p.CourseId == CourseId && p.Status == true);

        public async Task<List<Course>> FindAll() => await context.Courses.Where(p => p.Status == true).Take(10).ToListAsync();

        public async Task<List<string>> GetKeyWordByKeyword(string keyword)
        {
            var list = new List<string>();

            var listCourse = await context.Courses
                .Where(c => c.CourseName.ToLower().Contains(keyword.ToLower()) && c.Status == true)
                .Select(c => c.CourseName).ToListAsync();
            foreach (var item in listCourse)
            {
                list.Add(item);
            }

            return list;
        }

        public string GetNewestId()
        {

            return (from courses in context.Courses
                   
                    orderby
                      courses.CourseId descending
                    select courses.CourseId).Take(1).SingleOrDefault();

        }

        public List<Course> Search(string searchCourse)
        {
            var courses = context.Courses.AsQueryable();

            if (searchCourse != null) courses = courses.Where(b => b.CourseName.Contains(searchCourse) || b.Certificate.Contains(searchCourse));

            var result = courses.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }

        public bool RoleExists(string CourseId) => context.Courses.Any(e => e.CourseId == CourseId && e.Status == true);

        public async Task<Course> Update(Course course)
        {
            context.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return course;
        }

        public async Task<Course> FindAjax(string courseId)
        {
            return await context.Courses.FirstOrDefaultAsync(c => c.CourseId == courseId && c.Status == true);
        }
    }
}
