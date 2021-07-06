using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
   public interface IFeedbackService
    {
        List<Subject> subjects(string facultyid);
        Feedback feedbacks(string subjectid,string facultyid);
    }
}
