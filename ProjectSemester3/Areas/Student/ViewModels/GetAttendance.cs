using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Student.ViewModels
{
    public class GetAttendance
    {
        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }
        public int AttendanceId { get; set; }
        public string ClassId { get; set; }
        public string StudentId { get; set; }
        public string FacultyId { get; set; }
        public string FacultyName { get; set; }
        public DateTime Date { get; set; }
        public bool Checked { get; set; }
    }
}
