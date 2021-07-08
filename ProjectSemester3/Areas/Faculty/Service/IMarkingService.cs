using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public interface IMarkingService
    {
        List<Class> classes(string facultyid);
        Account getaccount(string username);
        Account getfaculty(string accountid);
        List<Mark> students(string examid);
        List<Subject> subjects(string classid);
        List<Exam> exams(string subjectid,string classid);


        Task<dynamic> update(List<Mark> marks);
       
        Class getclass(string classid);
        Exam getexam(string examid);

    }
}
