using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public interface IStudentService
    {
        List<Account> students(string facultyid, string searchfullname);
        List<Account> allstudents(string classid);
        Account getfacultyid(string facultyid);
        List<Subject> subjects(string classid);
        List<Mark> mark(string studentid);
        string[] datasubjectlist(string studentid);
        string[] datafulllesson(string studentid);
        string[] dataabsentlesson(string studentid);
        List<Class> classes(string facultyid);
        Class getclass(string classid);
    }
}
