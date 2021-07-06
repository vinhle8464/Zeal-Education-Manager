using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class ExaminationServiceImpl : IExaminationService
    {
        private readonly DatabaseContext context;
        public ExaminationServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public Exam examdetail(string examid)
        {
            return context.Exams.FirstOrDefault(m => m.ExamId == examid);
        }

        public List<Exam> exams(string subjectid)
        {
            return context.Exams.Where(m => m.SubjectId == subjectid).ToList();
        }

        public Account getfacultyid(string facultyid)
        {
            return context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }

        public List<Subject> subjects()
        {
            return context.Subjects.ToList();
        }

        
    }
}
