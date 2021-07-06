using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class ScheduleServiceImpl:IScheduleService
    {
        private readonly DatabaseContext context;
        public ScheduleServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }
        public List<Subject> subjects(string facultyid)
        {
            return context.Professionals.Where(m=>m.FacultyId==facultyid).Select(m => m.Subject).ToList();
        }
        public Account getfacultyid(string facultyid)
        {
            return context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }

        public List< Schedule> schedules(string subjectid)
        {
            return context.Schedules.Where(m => m.SubjectId == subjectid).ToList();
        }
    }
}
