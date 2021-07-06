using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Subject
    {
        public Subject()
        {
            Attendances = new HashSet<Attendance>();
            CourseSubjects = new HashSet<CourseSubject>();
            Exams = new HashSet<Exam>();
            Feedbacks = new HashSet<Feedback>();
            Professionals = new HashSet<Professional>();
            Schedules = new HashSet<Schedule>();
        }

        public string SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Attendance> Attendances { get; set; }
        public virtual ICollection<CourseSubject> CourseSubjects { get; set; }
        public virtual ICollection<Exam> Exams { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
        public virtual ICollection<Professional> Professionals { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
    }
}
