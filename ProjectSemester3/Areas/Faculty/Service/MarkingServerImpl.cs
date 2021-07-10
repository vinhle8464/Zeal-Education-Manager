using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class MarkingServerImpl : IMarkingService
    {
        private DatabaseContext _context;
        public MarkingServerImpl(DatabaseContext context)
        {
            _context = context;
        }

        public Account getaccount(string username)
        {
            return _context.Accounts.FirstOrDefault(m => m.Username == username);
        }

        public List<Class> classes(string facultyid)
        {
            return _context.ClassAssignments.Where(m => m.FacultyId == facultyid).Select(n => n.Class).ToList();
        }
        public List<Account> students(string exammid)
        {
            return _context.Marks.Where(m => m.Exam.ExamId == exammid).Select(m => m.Student).ToList();

        }

        public List<Subject> subjects(string classid)
        {
            return _context.CourseSubjects.Where(m => m.CourseId == _context.Courses.FirstOrDefault(m => m.CourseId == _context.Batches.FirstOrDefault(m => m.ClassId == classid).CourseId).CourseId).Select(m => m.Subject).ToList();
        }
        public List<Exam> exams(string subjectid, string classid)
        {
            

            return _context.Exams.Where(m => m.SubjectId == subjectid).ToList();
        }

        public Account getfaculty(string facultyid)
        {
            return _context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }

        public async Task<dynamic> update(List<Mark> mark)
        {
            _context.Entry(mark).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public Mark getmark(string examid)
        {
            return _context.Marks.First(m => m.ExamId == examid);
        }

        public Class getclass(string classid)
        {
            return _context.Classes.FirstOrDefault(m => m.ClassId == classid);
        }

        public Exam getexam(string examid)
        {
            return _context.Exams.FirstOrDefault(m => m.ExamId == examid);
        }

        List<Mark> IMarkingService.students(string examid)
        {
            throw new NotImplementedException();
        }

    }
}
