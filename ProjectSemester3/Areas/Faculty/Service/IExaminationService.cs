using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
   public interface IExaminationService
    {
        public List<Subject> subjects();
        public Account getfacultyid(string facultyid);
        public List<Exam> exams(string subjectid);
        public Exam examdetail(string examid);

    }
}
