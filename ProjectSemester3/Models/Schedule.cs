using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Schedule
    {
        public int ScheduleId { get; set; }
        public string ClassId { get; set; }
        public string SubjectId { get; set; }
        public string FacultyId { get; set; }
        public TimeSpan? TimeDay { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string StudyDay { get; set; }
        public bool Status { get; set; }

        public virtual Class Class { get; set; }
        public virtual Account Faculty { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
