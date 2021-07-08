
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IFeedBackService
    {
        public Task<List<Feedback>> FindAll();
        public Task<Feedback> Find(int FeedbackId);
        public Task<Feedback> Create(Feedback Feedback);
        public Task CreateFeedbackFaculty(FeedbackFaculty feedbackFaculty);

        public Task Delete(int FeedbackId);
        public Task<Feedback> Update(Feedback Feedback);
        public Task<List<string>> GetListFaculty(string keyword);
        public List<Subject> GetListSubjectAsync(string facultyName);
        public List<Feedback> Search(string searchFeedback);

    }
}
