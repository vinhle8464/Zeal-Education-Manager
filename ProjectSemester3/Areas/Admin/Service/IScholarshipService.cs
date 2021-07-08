
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IScholarshipService
    {
         Task<List<Scholarship>> FindAll();
         Task<Scholarship> Find(string ScholarshipId);
         Task<int> CountId();
         Task<dynamic> Create(Scholarship Scholarship);
         Task Delete(string ScholarshipId);
         string GetNewestId();
         Task<Scholarship> Update(Scholarship Scholarship);
         bool Exists(string ScholarshipId);
        public Task<Scholarship> FindAjax(string scholarshipId);
    }
}
