using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Scholarship
    {
        public Scholarship()
        {
            ScholarshipStudents = new HashSet<ScholarshipStudent>();
        }

        public string ScholarshipId { get; set; }
        public string ScholarshipName { get; set; }
        public string Discount { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<ScholarshipStudent> ScholarshipStudents { get; set; }
    }
}
