using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Mark
    {
        public int MarkId { get; set; }
        public string ExamId { get; set; }
        public string StudentId { get; set; }
        public decimal Mark1 { get; set; }
        public decimal MaxMark { get; set; }
        public byte? Rate { get; set; }
        public string StatusMark { get; set; }
        public bool Status { get; set; }

        public virtual Exam Exam { get; set; }
        public virtual Account Student { get; set; }
    }
}
