using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Course
    {
        public Course()
        {
            Batches = new HashSet<Batch>();
            CourseSubjects = new HashSet<CourseSubject>();
        }

        public string CourseId { get; set; }
        public string CourseName { get; set; }
        public decimal? Fee { get; set; }
        public string Term { get; set; }
        public string Certificate { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<CourseSubject> CourseSubjects { get; set; }
    }
}
