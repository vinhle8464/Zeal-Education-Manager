using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class ClassAssignmentServiceImpl : IClassAssignmentService
    {
        private readonly DatabaseContext context;
        public ClassAssignmentServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        // Create Class assignment
        public async Task<dynamic> Create(string facultyId, string classId, string subjectName)
        {
            
            var obj = await context.ClassAssignments.FirstOrDefaultAsync(c => c.ClassId == classId && c.SubjectName == subjectName);
            obj.FacultyId = facultyId;
            obj.Status = true;

            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           return await context.SaveChangesAsync();

        }


        // Find Class assignment
        public async Task<ClassAssignment> Find(string FacultyId, string ClassId) => await context.ClassAssignments.FirstOrDefaultAsync(c => c.FacultyId == FacultyId && c.ClassId == ClassId && c.Status == true);

        // Find all Class assignment
        public async Task<List<ClassAssignment>> FindAll() => await context.ClassAssignments.Where(c => c.Status == true).OrderByDescending(c => c.ClassId).ToListAsync();


        // get all class do not have faculty
        public async Task<List<string>> GetAllClass(string keyword)
        {
            var result = new List<string>();
            var list = new List<ClassAssignment>();
            try
            {
                list = await context.ClassAssignments.Where(c => c.Class.ClassName.ToLower().Contains(keyword.ToLower()) && c.FacultyId == null && c.Status == false).ToListAsync();
            }
            catch (System.Exception)
            {

                return null;
            }

            var group = list.GroupBy(c => c.Class.ClassName).ToList();
            foreach (var item in group)
            {
                result.Add(item.Key);
            }

            return result;
    }

       // Get List Subject
        public List<Subject> GetListSubject(string className)
        {
            var listSubject = new List<Subject>();
            var classId = new Class();
            var listSubjectName = new List<string>();

            try
            {
                classId = context.Classes.FirstOrDefault(c => c.ClassName == className);
               listSubjectName = context.ClassAssignments.Where(c => c.ClassId == classId.ClassId && c.FacultyId == null).Select(c => c.SubjectName).ToList();
            }
            catch (System.Exception)
            {
                return null;
            }

            foreach (var item in listSubjectName)
            {
                var result = context.Subjects.FirstOrDefault(s => s.SubjectName == item);
                listSubject.Add(result);
            }
            return listSubject;
        }

        // Get List Faculty
        public List<Account> GetListFaculty(string subjectName)
        {
            var listFaculty = new List<Account>();
           
            var listFacultyId = context.Professionals.Where(c => c.Subject.SubjectName == subjectName).Select(c => c.FacultyId).ToList();

            foreach (var item in listFacultyId)
            {
                var result = context.Accounts.FirstOrDefault(s => s.AccountId == item);
                listFaculty.Add(result);
            }
            return listFaculty;
        }

        // update Class assignment
        public async Task<ClassAssignment> Update(ClassAssignment classAssignment)
        {
            context.Entry(classAssignment).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return classAssignment;
        }

        public List<ClassAssignment> Search(string searchClassAssignment)
        {
            var classAssignments = context.ClassAssignments.AsQueryable();

            if (searchClassAssignment != null) classAssignments = classAssignments.Where(b => b.Faculty.Fullname.Contains(searchClassAssignment) || b.Class.ClassName.Contains(searchClassAssignment) || b.SubjectName.Contains(searchClassAssignment));

            var result = classAssignments.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }
    }
}
