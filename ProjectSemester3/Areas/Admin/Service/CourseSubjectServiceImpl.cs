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

    }
}
