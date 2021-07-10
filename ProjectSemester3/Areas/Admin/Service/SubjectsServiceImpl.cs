using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class SubjectsServiceImpl : ISubjectsService
    {
        private readonly DatabaseContext context;
        public SubjectsServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<int> CountId()
        {
           return await context.Subjects.Where(p => p.Status == true).CountAsync();
        }

       // public async Task<int> CountIdById(string SubjectId) => await context.Subjects.Where(p => p.SubjectId.Contains(SubjectId) && p.Status == true).CountAsync();

        public async Task<dynamic> Create(Subject Subject)
        {
            if (context.Subjects.Any(p => p.SubjectName == Subject.SubjectName && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.Subjects.Add(Subject);
              return await context.SaveChangesAsync();
            }
            
        }

        public async Task Delete(string SubjectId)
        {
            var Subject = context.Subjects.Find(SubjectId);
            Subject.Status = false;
            context.Entry(Subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

       public async Task<Subject> Find(string SubjectId) => await context.Subjects.FirstOrDefaultAsync(p => p.SubjectId == SubjectId && p.Status == true);

        public async Task<List<Subject>> FindAll() => await context.Subjects.Where(p => p.Status == true).ToListAsync();

        public string GetNewestId()
        {

            return (from Subjects in context.Subjects
                    
                    orderby
                      Subjects.SubjectId descending
                    select Subjects.SubjectId).Take(1).SingleOrDefault();

        }

        public bool Exists(string SubjectId) => context.Subjects.Any(e => e.SubjectId == SubjectId && e.Status == true);

        public async Task<Subject> Update(Subject Subject)
        {
            context.Entry(Subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return Subject;
        }

        public async Task<Subject> FindAjax(string subjectId)
        {
            return await context.Subjects.FirstOrDefaultAsync(s => s.SubjectId == subjectId && s.Status == true);
        }
    }
}
