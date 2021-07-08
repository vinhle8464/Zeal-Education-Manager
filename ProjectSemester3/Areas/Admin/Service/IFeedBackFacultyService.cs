
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IFeedbackFacultyService
    {
        Task<List<FeedbackFaculty>> FindAll();
        Task<FeedbackFaculty> Find(int feedbackId, string facultyId);
        Task<dynamic> Create(FeedbackFaculty FeedbackFaculty);
        Task Delete(int feedbackId, string facultyId);
        Task<FeedbackFaculty> Update(FeedbackFaculty FeedbackFaculty);
        List<FeedbackFaculty> Search(string searchFeedbackFaculty);

    }
}
