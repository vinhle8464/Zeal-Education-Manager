using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface ISubjectsService
    {
         Task<List<Subject>> FindAll();
         Task<Subject> Find(string SubjectId);
         Task<int> CountId();
         Task<dynamic> Create(Subject Subject);
         Task Delete(string SubjectId);
         string GetNewestId();
         Task<Subject> Update(Subject Subject);
         bool Exists(string SubjectId);
        Task<Subject> FindAjax(string subjectId);
    }
}
