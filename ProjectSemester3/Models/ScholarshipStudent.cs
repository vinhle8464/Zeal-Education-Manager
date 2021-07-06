using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class ScholarshipStudent
    {
        public string AccountId { get; set; }
        public string ScholarshipId { get; set; }
        public bool Status { get; set; }

        public virtual Account Account { get; set; }
        public virtual Scholarship Scholarship { get; set; }
    }
}
