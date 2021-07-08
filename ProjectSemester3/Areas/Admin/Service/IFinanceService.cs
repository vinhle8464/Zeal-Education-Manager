using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IFinanceService
    {
        Account getfaculty(string facultyid);
        Course course(string courseid);
        List<Course> courses();
        List<Account> students(Course course);
    }
}
