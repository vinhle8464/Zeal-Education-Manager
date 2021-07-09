using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
   public interface IAttendanceService
    {
        List<Subject> subjects(string classid);
        List<Class> classes(string facultyid);
        List<Attendance> attendances(string subjectid, string classid);
        dynamic update(int attendanceid,string check);
        List<Attendance> search(string subjectid,string search, string classid);
    }
}
