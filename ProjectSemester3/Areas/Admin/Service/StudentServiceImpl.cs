using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class StudentServiceImpl:IStudentService

    {
        private readonly DatabaseContext context;
        public StudentServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public Account getfacultyid(string facultyid)
        {
            return context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }

        public List<Account> students(string facultyid, string searchfullname)
        {
            List<Account> accounts=new List<Account>();
            foreach (var classid in context.ClassAssignments.Where(m => m.FacultyId == facultyid).Select(m => m.ClassId).ToList())
            {
              accounts= accounts.Union(context.Accounts.Where(m => m.Fullname.StartsWith(searchfullname) && m.ClassId == classid).ToList()).ToList() ;
            }
            return accounts;
        }
        public List<Subject> subjects(string classid)
        {
            return context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == classid).CourseId).CourseId).Select(m => m.Subject).ToList();
        }
        public List<Mark> mark(string studentid)
        {
            return context.Marks.Where(m => m.Student.AccountId == studentid).ToList();

        }

        public string[] datasubjectlist(string studentid)
        {
            List<Mark> datas = new List<Mark>();
            List<string> datasubject = new List<string>();

            foreach (var subjects in context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == context.Accounts.FirstOrDefault(m=>m.AccountId==studentid).ClassId).CourseId).CourseId).Select(m => m.Subject).ToList())
            {
               datas=datas.Union( context.Marks.Where(m => m.Exam.Subject==subjects).ToList()).ToList();
            }
            Debug.WriteLine(context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == context.Accounts.FirstOrDefault(m => m.AccountId == studentid).ClassId).CourseId).CourseId).Select(m => m.Subject.SubjectName).ToArray());
            foreach(var subjects in context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == context.Accounts.FirstOrDefault(m => m.AccountId == studentid).ClassId).CourseId).CourseId).Select(m => m.Subject).ToList())
            {
                try {
                    datasubject.Add(((decimal)(datas.Where(m => m.Exam.Subject == subjects && m.StudentId == studentid).Sum(m => m.Mark1) / (decimal)datas.Where(m => m.Exam.Subject == subjects && m.StudentId == studentid).Count()) / datas.FirstOrDefault(m => m.Exam.Subject == subjects).MaxMark).ToString());
                }
                catch
                {
                    break;
                }
             }
            
           return datasubject.ToArray();
        }

        public string[] datafulllesson(string studentid)
        {
            List<Attendance> datas = new List<Attendance>();
            List<string> datapreabslesson = new List<string>();

            foreach (var subjects in context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == context.Accounts.FirstOrDefault(m => m.AccountId == studentid).ClassId).CourseId).CourseId).Select(m => m.Subject).ToList())
            {
                datas = datas.Union(context.Attendances.Where(m => m.StudentId == studentid&&m.SubjectId==subjects.SubjectId).ToList()).ToList();
            }
            foreach (var subjects in context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == context.Accounts.FirstOrDefault(m => m.AccountId == studentid).ClassId).CourseId).CourseId).Select(m => m.Subject).ToList())
            {
                try
                {
                    datapreabslesson.Add(datas.Where(m=>m.StudentId==studentid&&m.SubjectId==subjects.SubjectId).Count().ToString());
                }
                catch
                {
                    break;
                }
            }

            return datapreabslesson.ToArray();
        }

        public string[] dataabsentlesson(string studentid)
        {
            List<Attendance> datas = new List<Attendance>();
            List<string> datapreabslesson = new List<string>();

            foreach (var subjects in context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == context.Accounts.FirstOrDefault(m => m.AccountId == studentid).ClassId).CourseId).CourseId).Select(m => m.Subject).ToList())
            {
                datas = datas.Union(context.Attendances.Where(m => m.StudentId == studentid && m.SubjectId == subjects.SubjectId).ToList()).ToList();
            }
            foreach (var subjects in context.CourseSubjects.Where(m => m.CourseId == context.Courses.FirstOrDefault(m => m.CourseId == context.Batches.FirstOrDefault(m => m.ClassId == context.Accounts.FirstOrDefault(m => m.AccountId == studentid).ClassId).CourseId).CourseId).Select(m => m.Subject).ToList())
            {
                try
                {
                    datapreabslesson.Add(datas.Where(m => m.StudentId == studentid && m.SubjectId == subjects.SubjectId&&m.Checked==false).Count().ToString());
                }
                catch
                {
                    break;
                }
            }

            return datapreabslesson.ToArray();
        }

        public List<Account> allstudents(string classid)
        {
            
            return context.Accounts.Where(m=>m.ClassId==classid).ToList();
        }
        public List<Class> classes(string facultyid)
        {
            return context.ClassAssignments.Where(m => m.FacultyId == facultyid).Select(n => n.Class).ToList();
        }
        public Class getclass(string classid)
        {
            return context.Classes.FirstOrDefault(m => m.ClassId == classid);
        }
    }
}
