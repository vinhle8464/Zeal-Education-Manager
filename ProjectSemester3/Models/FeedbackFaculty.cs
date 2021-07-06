using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class FeedbackFaculty
    {
        public int FeedbackId { get; set; }
        public string FacultyId { get; set; }
        public bool Status { get; set; }

        public virtual Account Faculty { get; set; }
        public virtual Feedback Feedback { get; set; }
    }
}
