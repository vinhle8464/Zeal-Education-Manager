using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class ClassAssignment
    {
        public int ClassAssignmentId { get; set; }
        public string FacultyId { get; set; }
        public string ClassId { get; set; }
        public string SubjectName { get; set; }
        public bool Status { get; set; }

        public virtual Class Class { get; set; }
        public virtual Account Faculty { get; set; }
    }
}
