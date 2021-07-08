using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IClassAssignmentService
    {
        public Task<List<ClassAssignment>> FindAll();
        public Task<ClassAssignment> Find(string FacultyId, string ClassId);
        public Task<dynamic> Create(string facultyId, string classId, string subjectName);
        public Task<ClassAssignment> Update(ClassAssignment classAssignment);
        public Task<List<string>> GetAllClass(string keyword);
        public List<Subject> GetListSubject(string className);
        public List<Account> GetListFaculty(string subjectId);
        List<ClassAssignment> Search(string searchCourse);
    }
}
