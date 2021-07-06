using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Batch
    {
        public string CourseId { get; set; }
        public string ClassId { get; set; }
        public bool Graduate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Status { get; set; }

        public virtual Class Class { get; set; }
        public virtual Course Course { get; set; }
    }
}
