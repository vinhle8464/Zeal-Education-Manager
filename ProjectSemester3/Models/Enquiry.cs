using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Enquiry
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Answer { get; set; }
        public bool Status { get; set; }
    }
}
