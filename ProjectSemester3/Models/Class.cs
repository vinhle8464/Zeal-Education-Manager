using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Class
    {
        public Class()
        {
            Accounts = new HashSet<Account>();
            Batches = new HashSet<Batch>();
            ClassAssignments = new HashSet<ClassAssignment>();
            Schedules = new HashSet<Schedule>();
            TestSchedules = new HashSet<TestSchedule>();
        }

        public string ClassId { get; set; }
        public string ClassName { get; set; }
        public byte NumberOfStudent { get; set; }
        public string Desc { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<Account> Accounts { get; set; }
        public virtual ICollection<Batch> Batches { get; set; }
        public virtual ICollection<ClassAssignment> ClassAssignments { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<TestSchedule> TestSchedules { get; set; }
    }
}
