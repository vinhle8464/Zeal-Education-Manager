using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace ProjectSemester3.Models
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Attendance> Attendances { get; set; }
        public virtual DbSet<Batch> Batches { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<ClassAssignment> ClassAssignments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<CourseSubject> CourseSubjects { get; set; }
        public virtual DbSet<Enquiry> Enquiries { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<FeedbackFaculty> FeedbackFaculties { get; set; }
        public virtual DbSet<Mail> Mail { get; set; }
        public virtual DbSet<Mark> Marks { get; set; }
        public virtual DbSet<Pay> Pays { get; set; }
        public virtual DbSet<Professional> Professionals { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Scholarship> Scholarships { get; set; }
        public virtual DbSet<ScholarshipStudent> ScholarshipStudents { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }
        public virtual DbSet<TestSchedule> TestSchedules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_id");

                entity.Property(e => e.Active).HasColumnName("active");

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class_id");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("dob");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.RoleId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_account_class");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_account_role");
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.ToTable("attendance");

                entity.Property(e => e.AttendanceId).HasColumnName("attendance_id");

                entity.Property(e => e.Checked).HasColumnName("checked");

                entity.Property(e => e.ClassId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.FacultyId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("faculty_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("student_id");

                entity.Property(e => e.SubjectId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.AttendanceFaculties)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_attendance_faculty");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AttendanceStudents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_attendance_student");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Attendances)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_attendance_course");
            });

            modelBuilder.Entity<Batch>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.ClassId })
                    .HasName("PK__batch__F0C1B03680301E52");

                entity.ToTable("batch");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("course_id");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.Graduate).HasColumnName("graduate");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_batch_class");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_batch_course");
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("class");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class_id");

                entity.Property(e => e.ClassName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("class_name");

                entity.Property(e => e.Desc)
                    .HasMaxLength(250)
                    .HasColumnName("desc");

                entity.Property(e => e.NumberOfStudent).HasColumnName("number_of_student");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ClassAssignment>(entity =>
            {
                entity.ToTable("class_assignment");

                entity.Property(e => e.ClassAssignmentId).HasColumnName("class_assignment_id");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class_id");

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("faculty_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubjectName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("subject_name");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.ClassAssignments)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_class_assignment_class");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.ClassAssignments)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK_class_assignment_faculty");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("course");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("course_id");

                entity.Property(e => e.Certificate).HasColumnName("certificate");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(250)
                    .HasColumnName("course_name");

                entity.Property(e => e.Desc)
                    .HasMaxLength(250)
                    .HasColumnName("desc");

                entity.Property(e => e.Fee)
                    .HasColumnType("money")
                    .HasColumnName("fee");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Term)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("term");
            });

            modelBuilder.Entity<CourseSubject>(entity =>
            {
                entity.HasKey(e => new { e.CourseId, e.SubjectId })
                    .HasName("PK__course_s__9A1EB8C853D103CA");

                entity.ToTable("course_subject");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("course_id");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.CourseSubjects)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_course_subject_course");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.CourseSubjects)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_course_subject_subject");
            });

            modelBuilder.Entity<Enquiry>(entity =>
            {
                entity.ToTable("enquiry");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Answer).HasColumnName("answer");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.ToTable("exam");

                entity.Property(e => e.ExamId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("exam_id");

                entity.Property(e => e.Desc)
                    .HasMaxLength(250)
                    .HasColumnName("desc");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubjectId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Exams)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_exam_subject");
            });

            modelBuilder.Entity<Feedback>(entity =>
            {
                entity.ToTable("feedback");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(250)
                    .HasColumnName("note");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubjectId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Feedbacks)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_subject");
            });

            modelBuilder.Entity<FeedbackFaculty>(entity =>
            {
                entity.HasKey(e => new { e.FeedbackId, e.FacultyId })
                    .HasName("PK__feedback__ADDB2F9F483C8A1B");

                entity.ToTable("feedback_faculty");

                entity.Property(e => e.FeedbackId).HasColumnName("feedback_id");

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("faculty_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.FeedbackFaculties)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_faculty_faculty");

                entity.HasOne(d => d.Feedback)
                    .WithMany(p => p.FeedbackFaculties)
                    .HasForeignKey(d => d.FeedbackId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_feedback_faculty_feedback");
            });

            modelBuilder.Entity<Mail>(entity =>
            {
                entity.ToTable("mail");

                entity.Property(e => e.MailId).HasColumnName("mail_id");

                entity.Property(e => e.Check).HasColumnName("check");

                entity.Property(e => e.Content).HasColumnName("content");

                entity.Property(e => e.EmailUser)
                    .HasMaxLength(100)
                    .HasColumnName("email_user");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(100)
                    .HasColumnName("fullname");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .HasColumnName("phone_number");

                entity.Property(e => e.ReplyContent).HasColumnName("reply_content");

                entity.Property(e => e.ReplyDate)
                    .HasColumnType("datetime")
                    .HasColumnName("reply_date");

                entity.Property(e => e.SendDate)
                    .HasColumnType("datetime")
                    .HasColumnName("send_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Mark>(entity =>
            {
                entity.ToTable("mark");

                entity.Property(e => e.MarkId).HasColumnName("mark_id");

                entity.Property(e => e.ExamId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("exam_id");

                entity.Property(e => e.Mark1)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("mark");

                entity.Property(e => e.MaxMark)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("max_mark");

                entity.Property(e => e.Rate).HasColumnName("rate");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StatusMark)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status_mark");

                entity.Property(e => e.StudentId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("student_id");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.ExamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mark_exam");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Marks)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_mark_account");
            });

            modelBuilder.Entity<Pay>(entity =>
            {
                entity.ToTable("pay");

                entity.Property(e => e.PayId).HasColumnName("pay_id");

                entity.Property(e => e.AccountId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_id");

                entity.Property(e => e.DatePaid)
                    .HasColumnType("datetime")
                    .HasColumnName("date_paid");

                entity.Property(e => e.DateRequest)
                    .HasColumnType("datetime")
                    .HasColumnName("date_request");

                entity.Property(e => e.Discount)
                    .HasColumnType("money")
                    .HasColumnName("discount");

                entity.Property(e => e.Fee)
                    .HasColumnType("money")
                    .HasColumnName("fee");

                entity.Property(e => e.PayStatus).HasColumnName("pay_status");

                entity.Property(e => e.Payment)
                    .HasMaxLength(100)
                    .HasColumnName("payment");

                entity.Property(e => e.Title)
                    .HasMaxLength(250)
                    .HasColumnName("title");

                entity.Property(e => e.Total)
                    .HasColumnType("money")
                    .HasColumnName("total");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Pays)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pay_account");
            });

            modelBuilder.Entity<Professional>(entity =>
            {
                entity.HasKey(e => new { e.FacultyId, e.SubjectId })
                    .HasName("PK__professi__6E000E5A1A48B6CF");

                entity.ToTable("professional");

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("faculty_id");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Professionals)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_professional_faculty");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Professionals)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_professional_subject");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.RoleId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("role_id");

                entity.Property(e => e.Desc)
                    .HasMaxLength(250)
                    .HasColumnName("desc");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("role_name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("schedule");

                entity.Property(e => e.ScheduleId).HasColumnName("schedule_id");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class_id");

                entity.Property(e => e.EndDate)
                    .HasColumnType("date")
                    .HasColumnName("end_date");

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("faculty_id");

                entity.Property(e => e.StartDate)
                    .HasColumnType("date")
                    .HasColumnName("start_date");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudyDay)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("study_day");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.Property(e => e.TimeDay).HasColumnName("time_day");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_schedule_class");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK_schedule_account");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_schedule_course");
            });

            modelBuilder.Entity<Scholarship>(entity =>
            {
                entity.ToTable("scholarship");

                entity.Property(e => e.ScholarshipId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("scholarship_id");

                entity.Property(e => e.Desc).HasColumnName("desc");

                entity.Property(e => e.Discount)
                    .IsRequired()
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("discount")
                    .IsFixedLength(true);

                entity.Property(e => e.ScholarshipName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasColumnName("scholarship_name");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<ScholarshipStudent>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.ScholarshipId })
                    .HasName("PK__scholars__403E15072859B15B");

                entity.ToTable("scholarship_student");

                entity.Property(e => e.AccountId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_id");

                entity.Property(e => e.ScholarshipId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("scholarship_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ScholarshipStudents)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_scholarship_student_account");

                entity.HasOne(d => d.Scholarship)
                    .WithMany(p => p.ScholarshipStudents)
                    .HasForeignKey(d => d.ScholarshipId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_scholarship_student_scholarship");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.Property(e => e.SubjectId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("subject_id");

                entity.Property(e => e.Desc)
                    .HasMaxLength(250)
                    .HasColumnName("desc");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.SubjectName)
                    .HasMaxLength(250)
                    .HasColumnName("subject_name");
            });

            modelBuilder.Entity<TestSchedule>(entity =>
            {
                entity.ToTable("test_schedule");

                entity.Property(e => e.TestScheduleId).HasColumnName("test_schedule_id");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("class_id");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.ExamId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("exam_id");

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("faculty_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TestSchedules)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_test_schedule_class");

                entity.HasOne(d => d.Exam)
                    .WithMany(p => p.TestSchedules)
                    .HasForeignKey(d => d.ExamId)
                    .HasConstraintName("FK_test_schedule_exam");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.TestSchedules)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK_test_schedule_account");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
