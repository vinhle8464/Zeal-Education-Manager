using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class ProfessionalsServiceImpl : IProfessionalsService
    {
        private readonly DatabaseContext context;
        public ProfessionalsServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }



        public async Task<dynamic> Create(Professional professional)
        {
            if (context.Professionals.Any(p => p.FacultyId == professional.FacultyId && p.SubjectId == professional.SubjectId && p.Status == true))
            {
                return 0;
            }
            else if (context.Professionals.Any(p => p.FacultyId == professional.FacultyId && p.SubjectId == p.SubjectId && p.Status == false))
            {
                professional.Status = true;
                context.Entry(professional).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                return await context.SaveChangesAsync();
            }
            else
            {
                context.Professionals.Add(professional);
                return await context.SaveChangesAsync();
            }

        }


        public async Task<Professional> Find(string FacultyId, string SubjectId) => await context.Professionals.FirstOrDefaultAsync(p => p.FacultyId == FacultyId && p.SubjectId == SubjectId);

        public async Task<List<Professional>> FindAll() => await context.Professionals.OrderBy(p => p.FacultyId).ToListAsync();




        public async Task<Professional> Update(Professional professional)
        {
            context.Entry(professional).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return professional;
        }

        // get all class do not have faculty
        public async Task<List<string>> GetAllFaculty(string keyword)
        {
            var result = new List<string>();
            var list = await context.Accounts.Where(c => c.Fullname.ToLower().Contains(keyword.ToLower()) && c.RoleId == "role02" && c.Status == true).ToListAsync();

            foreach (var item in list)
            {
                result.Add(item.Fullname);
            }

            return result;
        }

        // Get List Subject
        public List<Subject> GetListSubject(string facultyName)
        {
            var result = context.Subjects.Where(c => c.Status == true).ToList();
            var facultyId = context.Accounts.FirstOrDefault(c => c.Fullname == facultyName);


            var listSubjectId = new List<string>();
            try
            {
                listSubjectId = context.Professionals.Where(p => p.FacultyId == facultyId.AccountId && p.Status == true).Select(p => p.SubjectId).ToList();
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

        // get keyword by term
        public async Task<List<string>> GetKeyWordByKeyword(string keyword)
        {
            var list = new List<string>();

            var listFaculty = await context.Accounts
                .Where(c => c.Fullname.ToLower().Contains(keyword.ToLower()) && c.Status == true && c.RoleId == "role02")
                .Select(c => c.Fullname).ToListAsync();
            foreach (var item in listFaculty)
            {
                list.Add(item);
            }

            var listSubject = await context.Subjects
                .Where(c => c.SubjectName.ToLower().Contains(keyword.ToLower()) && c.Status == true)
                .Select(c => c.SubjectName).ToListAsync();
            foreach (var item in listSubject)
            {
                list.Add(item);
            }

            return list;
        }



        public List<Professional> Search(string searchKeyword1, string subjectKeyword)
        {
            var professionals = context.Professionals.AsQueryable();

            if (subjectKeyword != null) professionals = professionals.Where(s => s.Subject.SubjectName.Contains(subjectKeyword));

            if (searchKeyword1 != null) professionals = professionals.Where(b => b.Faculty.Fullname.Contains(searchKeyword1) || b.Subject.SubjectName.Contains(searchKeyword1));

            var result = professionals.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }
    }
}
