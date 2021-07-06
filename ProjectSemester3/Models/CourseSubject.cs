using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class CourseSubject
    {
        public string CourseId { get; set; }
        public string SubjectId { get; set; }
        public bool Status { get; set; }

        public virtual Course Course { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
