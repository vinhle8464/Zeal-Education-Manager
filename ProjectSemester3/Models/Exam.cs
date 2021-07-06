using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Exam
    {
        public Exam()
        {
            Marks = new HashSet<Mark>();
            TestSchedules = new HashSet<TestSchedule>();
        }

        public string ExamId { get; set; }
        public string SubjectId { get; set; }
        public string Title { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        public virtual ICollection<TestSchedule> TestSchedules { get; set; }
    }
}
