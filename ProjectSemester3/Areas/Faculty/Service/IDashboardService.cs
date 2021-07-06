using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
   public interface IDashboardService
    {
        Account getfaculty(string username);
      List<string> getstudent(string fullname);
    }
}
