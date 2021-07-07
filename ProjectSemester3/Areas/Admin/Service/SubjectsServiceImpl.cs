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
        private readonly DatabaseContext _context;
        public SubjectsServiceImpl(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> CountId()
        {
           return await _context.Subjects.Where(p => p.Status == true).CountAsync();
        }

       // public async Task<int> CountIdById(string SubjectId) => await _context.Subjects.Where(p => p.SubjectId.Contains(SubjectId) && p.Status == true).CountAsync();

        public async Task<dynamic> Create(Subject Subject)
        {
            if (_context.Subjects.Any(p => p.SubjectName == Subject.SubjectName && p.Status == true))
            {
                return 0;
            }
            else
            {
                _context.Subjects.Add(Subject);
              return await _context.SaveChangesAsync();
            }
            
        }

        public async Task Delete(string SubjectId)
        {
            var Subject = _context.Subjects.Find(SubjectId);
            Subject.Status = false;
            _context.Entry(Subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
        }

       public async Task<Subject> Find(string SubjectId) => await _context.Subjects.FirstOrDefaultAsync(p => p.SubjectId == SubjectId && p.Status == true);

        public async Task<List<Subject>> FindAll() => await _context.Subjects.Where(p => p.Status == true).Take(10).ToListAsync();

        public string GetNewestId()
        {

            return (from Subjects in _context.Subjects
                    where
                      Subjects.Status == true
                    orderby
                      Subjects.SubjectId descending
                    select Subjects.SubjectId).Take(1).SingleOrDefault();

        }

        public bool Exists(string SubjectId) => _context.Subjects.Any(e => e.SubjectId == SubjectId && e.Status == true);

        public async Task<Subject> Update(Subject Subject)
        {
            _context.Entry(Subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _context.SaveChangesAsync();
            return Subject;
        }


    }
}
