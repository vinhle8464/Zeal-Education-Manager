using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class FeedbackServiceImpl : IFeedbackService
    { private readonly DatabaseContext context;
        public FeedbackServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public Feedback feedbacks(string subjectid,string facultyid)
        {
            List<Feedback> feedbacks = context.FeedbackFaculties.Where(m => m.FacultyId == facultyid).Select(m => m.Feedback).Where(m => m.SubjectId == subjectid).ToList();
            Feedback feedback = new Feedback();
            feedback.SubjectId = subjectid;
            feedback.Teaching = (feedbacks.Sum(m => m.Teaching)) / (feedbacks.Count());
            feedback.Exercises = (feedbacks.Sum(m => m.Exercises)) / (feedbacks.Count());
            feedback.TeacherEthics = (feedbacks.Sum(m => m.TeacherEthics)) / (feedbacks.Count());
            feedback.Specialize = (feedbacks.Sum(m => m.Specialize)) / (feedbacks.Count());
            feedback.Assiduous = (feedbacks.Sum(m => m.Assiduous)) / (feedbacks.Count());
            return feedback;
        }

        public List<Subject> subjects(string facultyid)
        {
            List<Subject> subjects = new List<Subject>();
            foreach (var subjectid in context.CourseSubjects.Where(m => m.Course.Batches.FirstOrDefault().Class.ClassAssignments.FirstOrDefault().FacultyId == facultyid).Distinct().Select(m => m.Subject.SubjectId).ToList())
            {
                subjects = subjects.Union(context.Subjects.Where(m => m.SubjectId == subjectid).ToList()).ToList();
            }
            
            return subjects ;

            
        }
    }
}
