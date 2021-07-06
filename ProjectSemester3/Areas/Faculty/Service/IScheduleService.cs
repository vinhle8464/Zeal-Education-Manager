using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
   public interface IScheduleService
    {
        public List<Subject> subjects(string facultyid);
        public Account getfacultyid(string facultyid);
        public List<Schedule> schedules(string subjectid);
    }
}
