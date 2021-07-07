using ProjectSemester3.Models;
using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Areas.Faculty.Models
{
    public partial class Student
    {
        public Student()
        {
            Attendances = new HashSet<Attendance>();
            Batches = new HashSet<Batch>();
        }

        public string StudentId { get; set; }
        public string ClassId { get; set; }

        public virtual Account StudentNavigation { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
    }
}
