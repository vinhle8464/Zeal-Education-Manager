using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class CoursesServiceImpl : ICoursesService
    {
        private readonly DatabaseContext _context;
        public CoursesServiceImpl(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> CountId()
        {
           return await _context.Courses.Where(p => p.Status == true).CountAsync();
        }

       // public async Task<int> CountIdById(string RoleId) => await _context.Roles.Where(p => p.RoleId.Contains(RoleId) && p.Status == true).CountAsync();

        public async Task<dynamic> Create(Course course)
        {
            if (_context.Courses.Any(p => p.CourseName == course.CourseName))
            {
                return 0;
            }
            else
            {
                _context.Courses.Add(course);
              return await _context.SaveChangesAsync();
            }
            
        }

        public async Task Delete(string CourseId)
        {
            var course = _context.Courses.Find(CourseId);
            course.Status = false;
            _context.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

       public async Task<Course> Find(string CourseId) => await _context.Courses.FirstOrDefaultAsync(p => p.CourseId == CourseId && p.Status == true);

        public async Task<List<Course>> FindAll() => await _context.Courses.Where(p => p.Status == true).Take(10).ToListAsync();

        public string GetNewestId()
        {

            return (from courses in _context.Courses
                    where
                      courses.Status == true
                    orderby
                      courses.CourseId descending
                    select courses.CourseId).Take(1).SingleOrDefault();

        }

        public bool RoleExists(string CourseId) => _context.Courses.Any(e => e.CourseId == CourseId && e.Status == true);

        public async Task<Course> Update(Course course)
        {
            _context.Entry(course).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return course;
        }


    }
}
