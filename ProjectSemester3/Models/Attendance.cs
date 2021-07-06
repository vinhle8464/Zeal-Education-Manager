using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Attendance
    {
        public int AttendanceId { get; set; }
        public string ClassId { get; set; }
        public string StudentId { get; set; }
        public string FacultyId { get; set; }
        public string SubjectId { get; set; }
        public DateTime? Date { get; set; }
        public bool Checked { get; set; }
        public bool Status { get; set; }

        public virtual Account Faculty { get; set; }
        public virtual Account Student { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
