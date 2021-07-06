using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Professional
    {
        public string FacultyId { get; set; }
        public string SubjectId { get; set; }
        public bool Status { get; set; }

        public virtual Account Faculty { get; set; }
        public virtual Subject Subject { get; set; }
    }
}
