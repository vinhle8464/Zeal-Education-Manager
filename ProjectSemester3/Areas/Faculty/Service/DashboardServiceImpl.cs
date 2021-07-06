using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class DashboardServiceImpl : IDashboardService
    {
        private DatabaseContext context;
        public DashboardServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public Account getfaculty(string username)
        {
          
            return context.Accounts.FirstOrDefault(m => m.Username == username);

        }

        public List<string> getstudent(string fullname)
        {
            return context.Accounts.Where(m => m.Fullname.StartsWith(fullname)  && m.Role.RoleName == "student").Select(m=>m.Fullname).ToList();
        }
    }
}
