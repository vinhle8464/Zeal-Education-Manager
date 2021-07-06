using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Feedback
    {
        public Feedback()
        {
            FeedbackFaculties = new HashSet<FeedbackFaculty>();
        }

        public int FeedbackId { get; set; }
        public string SubjectId { get; set; }
        public int Teaching { get; set; }
        public int Exercises { get; set; }
        public int TeacherEthics { get; set; }
        public int Specialize { get; set; }
        public int Assiduous { get; set; }
        public string Note { get; set; }
        public bool Status { get; set; }

        public virtual Subject Subject { get; set; }
        public virtual ICollection<FeedbackFaculty> FeedbackFaculties { get; set; }
    }
}
