
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IProfessionalsService
    {
        Task<List<Professional>> FindAll();
        Task<Professional> Find(string FacultyId, string SubjectId);
        Task<dynamic> Create(Professional professional);
        Task<Professional> Update(Professional professional);
        public Task<List<string>> GetAllFaculty(string keyword);
        public List<Subject> GetListSubject(string facultyName);
        public Task<List<string>> GetKeyWordByKeyword(string keyword);
        public List<Professional> Search(string searchKeyword1, string subjectKeyword);

    }
}
