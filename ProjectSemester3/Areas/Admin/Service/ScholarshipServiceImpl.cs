using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class ScholarshipServiceImpl : IScholarshipService
    {
        private readonly DatabaseContext context;
        public ScholarshipServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<int> CountId()
        {
            return await context.Scholarships.Where(p => p.Status == true).CountAsync();
        }

        // public async Task<int> CountIdById(string ScholarshipId) => await context.Scholarships.Where(p => p.ScholarshipId.Contains(ScholarshipId) && p.Status == true).CountAsync();

        public async Task<dynamic> Create(Scholarship Scholarship)
        {
            if (context.Scholarships.Any(p => p.ScholarshipName == Scholarship.ScholarshipName && p.Status == true))
            {
                return 0;
            }
            else if (context.Scholarships.Any(p => p.ScholarshipName == Scholarship.ScholarshipName && p.Status == false))
            {
                var newscho = await context.Scholarships.FirstOrDefaultAsync(s => s.ScholarshipName == Scholarship.ScholarshipName);
                newscho.Status = true;
                context.Entry(newscho).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return await context.SaveChangesAsync();

            }
            else
            {
                context.Scholarships.Add(Scholarship);
                return await context.SaveChangesAsync();
            }

        }

        public async Task Delete(string ScholarshipId)
        {
            var Scholarship = context.Scholarships.Find(ScholarshipId);
            Scholarship.Status = false;
            context.Entry(Scholarship).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Scholarship> Find(string ScholarshipId) => await context.Scholarships.FirstOrDefaultAsync(p => p.ScholarshipId == ScholarshipId && p.Status == true);

        public async Task<List<Scholarship>> FindAll() => await context.Scholarships.Where(p => p.Status == true).Take(10).ToListAsync();

        public string GetNewestId()
        {

            return (from Scholarships in context.Scholarships

                    orderby
                      Scholarships.ScholarshipId descending
                    select Scholarships.ScholarshipId).Take(1).SingleOrDefault();

        }

        public bool Exists(string ScholarshipId) => context.Scholarships.Any(e => e.ScholarshipId == ScholarshipId && e.Status == true);

        public async Task<Scholarship> Update(Scholarship Scholarship)
        {
            context.Entry(Scholarship).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return Scholarship;
        }

        public async Task<Scholarship> FindAjax(string scholarshipId)
        {
            return await context.Scholarships.FirstOrDefaultAsync(s => s.ScholarshipId == scholarshipId && s.Status == true);
        }
    }
}
