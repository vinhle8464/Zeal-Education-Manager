
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IScholarshipStudentService
    {
        Task<List<ScholarshipStudent>> FindAll();
        Task<ScholarshipStudent> Find(string AccountId, string ScholarShipId);
        Task<dynamic> Create(ScholarshipStudent ScholarshipStudent);
        Task<ScholarshipStudent> Update(ScholarshipStudent ScholarshipStudent);
       
    }
}
