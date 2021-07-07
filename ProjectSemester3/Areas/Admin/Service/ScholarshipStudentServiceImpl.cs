using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class ScholarshipStudentServiceImpl : IScholarshipStudentService
    {
        private readonly DatabaseContext context;
        public ScholarshipStudentServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }


        public async Task<dynamic> Create(ScholarshipStudent ScholarshipStudent)
        {
            if (context.ScholarshipStudents.Any(p => p.AccountId == ScholarshipStudent.AccountId && p.ScholarshipId == ScholarshipStudent.ScholarshipId && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.ScholarshipStudents.Add(ScholarshipStudent);
              return await context.SaveChangesAsync();
            }
            
        }


       public async Task<ScholarshipStudent> Find(string AccountId, string ScholarShipId) => await context.ScholarshipStudents.FirstOrDefaultAsync(p => p.AccountId == AccountId && p.ScholarshipId == ScholarShipId);

        public async Task<List<ScholarshipStudent>> FindAll() => await context.ScholarshipStudents.OrderBy(p => p.ScholarshipId).ToListAsync();


        public async Task<ScholarshipStudent> Update(ScholarshipStudent ScholarshipStudent)
        {
            context.Entry(ScholarshipStudent).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return ScholarshipStudent;
        }


    }
}
