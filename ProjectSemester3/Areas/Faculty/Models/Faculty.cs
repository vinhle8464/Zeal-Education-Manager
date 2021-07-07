using ProjectSemester3.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Areas.Faculty.Models
{
    public partial class Faculty
    {
        public Faculty()
        {
            Attendances = new HashSet<Attendance>();
            Batches = new HashSet<Batch>();
            ClassAssignments = new HashSet<ClassAssignment>();
        }

        public string FacultyId { get; set; }
        public string Professional { get; set; }

        public virtual Account FacultyNavigation { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<ClassAssignment> ClassAssignments { get; set; }
    }
}
