using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
   public interface IBatchService
    {
        Account getfaculty(string facultyid);
        Course course(string courseid);
        List<Account> students(Course course);
        List<Course> courses();
        List<Account> scholarship(Course course);
        List<Schedule> schedules(Course course);
    }
}
