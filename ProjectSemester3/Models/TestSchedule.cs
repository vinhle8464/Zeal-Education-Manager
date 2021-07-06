using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class TestSchedule
    {
        public int TestScheduleId { get; set; }
        public string ClassId { get; set; }
        public string ExamId { get; set; }
        public string FacultyId { get; set; }
        public DateTime? Date { get; set; }
        public bool Status { get; set; }

        public virtual Class Class { get; set; }
        public virtual Exam Exam { get; set; }
        public virtual Account Faculty { get; set; }
    }
}
