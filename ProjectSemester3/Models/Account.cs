using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class Account
    {
        public Account()
        {
            AttendanceFaculties = new HashSet<Attendance>();
            AttendanceStudents = new HashSet<Attendance>();
            ClassAssignments = new HashSet<ClassAssignment>();
            FeedbackFaculties = new HashSet<FeedbackFaculty>();
            Marks = new HashSet<Mark>();
            Pays = new HashSet<Pay>();
            Professionals = new HashSet<Professional>();
            Schedules = new HashSet<Schedule>();
            ScholarshipStudents = new HashSet<ScholarshipStudent>();
            TestSchedules = new HashSet<TestSchedule>();
        }

        public string AccountId { get; set; }
        public string RoleId { get; set; }
        public string ClassId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public DateTime? Dob { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public bool Active { get; set; }
        public bool Status { get; set; }

        public virtual Class Class { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Attendance> AttendanceFaculties { get; set; }
        public virtual ICollection<Attendance> AttendanceStudents { get; set; }
        public virtual ICollection<ClassAssignment> ClassAssignments { get; set; }
        public virtual ICollection<FeedbackFaculty> FeedbackFaculties { get; set; }
        public virtual ICollection<Mark> Marks { get; set; }
        public virtual ICollection<Pay> Pays { get; set; }
        public virtual ICollection<Professional> Professionals { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<ScholarshipStudent> ScholarshipStudents { get; set; }
        public virtual ICollection<TestSchedule> TestSchedules { get; set; }
    }
}
