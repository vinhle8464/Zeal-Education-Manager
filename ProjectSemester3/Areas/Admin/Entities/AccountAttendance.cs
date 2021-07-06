using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Entities
{
    public class AccountAttendance
    {
        public string AccountId { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Class { get; set; }
        public string Facultty { get; set; }
        public string SubjectId { get; set; }
    }
}
