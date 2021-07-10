use master
drop database if exists projectsemester3
create database projectsemester3
GO

use projectsemester3
GO

create table account
(
    account_id varchar(50) PRIMARY KEY,
    role_id varchar(50) not null,
	class_id varchar(50),
    username varchar(50),
    [password] varchar(250) not null,
    fullname nvarchar(100),
    email varchar(100),
    dob date,
	[address] nvarchar(250),
    gender bit,
    phone varchar(20),
    avatar varchar(250),
	active bit not null,
	[status] bit not null
)

create table scholarship
(
	scholarship_id varchar(50) primary key,
	scholarship_name nvarchar(250) not null,
	discount char(2) not null,
	[desc] nvarchar(max),
	[status] bit not null
)

create table scholarship_student
(
	account_id varchar(50),
	scholarship_id varchar(50),
	[status] bit not null,
	primary key(account_id, scholarship_id)
)


create table [role]
(
    role_id varchar(50) PRIMARY KEY,
    [role_name] nvarchar(100) not null,
	[desc] nvarchar(250),
	[status] bit not null
)


create table class
(
    class_id varchar(50) PRIMARY KEY,
    [class_name] nvarchar(100) not null,
	number_of_student tinyint not null,
    [desc] nvarchar(250),
	[status] bit not null
)


create table schedule
(
	schedule_id int identity primary key,
	class_id varchar(50),
	subject_id varchar(50),
	faculty_id varchar(50),
	[time_day] time,
	[start_date] date,
	[end_date] date,
	study_day varchar(250),
	[status] bit not null
)	

create table test_schedule
(
	test_schedule_id int identity primary key,
	class_id varchar(50),
	exam_id varchar(50),
	faculty_id varchar(50),
	[date] datetime,
	[status] bit not null
)


create table attendance
(
    attendance_id int identity primary key,
	class_id varchar(50) not null,
    student_id varchar(50) not null,
	faculty_id varchar(50) not null,
	subject_id varchar(50) not null,
	[date] datetime,
	checked bit not null,
	[status] bit not null
)


create table class_assignment
(
    class_assignment_id int identity primary key,
    faculty_id varchar(50),
    class_id varchar(50),
	subject_name nvarchar(250) not null,
	[status] bit not null

)

create table pay
(
	 pay_id int identity PRIMARY KEY,
	 account_id varchar(50) not null,
	 payment nvarchar(100),
	 title nvarchar(250),
	 fee money not null,
	 discount money,
	  total money,
	   date_request datetime,
	  date_paid datetime,
		pay_status bit not null
)


create table batch
(
    course_id varchar(50)  not null,
    class_id varchar(50)  not null,
	graduate bit not null,
	[start_date] date not null,
    end_date date not null,
	[status] bit not null,
    PRIMARY KEY (course_id, class_id)
)

create table course
(
    course_id varchar(50) PRIMARY KEY,
    [course_name] nvarchar(250),
    fee money,
    term varchar(250),
	[certificate] nvarchar(max),
    [desc] nvarchar(250),
	[status] bit not null
)


create table course_subject
(
    course_id varchar(50) not null,
    subject_id varchar(50) not null,
	[status] bit not null,
	primary key(course_id, subject_id)
  
)

create table professional
(
    faculty_id varchar(50) not null,
    subject_id varchar(50) not null,
	[status] bit not null,
	primary key(faculty_id, subject_id)
  
)


create table [subject]
(
    subject_id varchar(50) primary key,
    subject_name nvarchar(250),
	[desc] nvarchar(250),
	[status] bit not null
)


create table exam 
(
    exam_id varchar(50) PRIMARY KEY,
	subject_id varchar(50) not null,
    title nvarchar(250),
    [desc] nvarchar(250),
   [status] bit not null

)


create table mark 
(
    mark_id int identity primary key,
	exam_id varchar(50) not null,
	student_id varchar(50) not null,
    mark DECIMAL(5,2)not null,
	max_mark DECIMAL(5,2) not null,
	rate tinyint,
	status_mark nvarchar(100) not null,
	[status] bit not null
   
)

create table enquiry
(
    id int identity PRIMARY KEY,
    title nvarchar(max),
    answer nvarchar(max),
	[status] bit not null
)


create table feedback(
feedback_id int identity primary key,
subject_id varchar(50) not null,
Teaching int not null,
Exercises int not null,
TeacherEthics int not null,
Specialize int not null,
Assiduous int not null,
note nvarchar(250),
[status] bit not null
)



create table feedback_faculty
(
    feedback_id int not null,
    faculty_id varchar(50) not null,
	[status] bit not null,
	primary key(feedback_id, faculty_id)
  
)



create table mail
(
    mail_id int identity PRIMARY KEY,
    title nvarchar(250),
    email_user nvarchar(100),
    fullname nvarchar(100),
    phone_number nvarchar(20),
    content nvarchar(max),
    reply_content nvarchar(max),
	send_date datetime,
	reply_date datetime,
	[check] bit,
	[status] bit not null
	
)

  ALTER TABLE professional 
ADD CONSTRAINT FK_professional_faculty
  FOREIGN KEY (faculty_id)
  REFERENCES account (account_id)

    ALTER TABLE professional 
ADD CONSTRAINT FK_professional_subject
  FOREIGN KEY (subject_id)
  REFERENCES [subject] (subject_id)


  ALTER TABLE account 
ADD CONSTRAINT FK_account_role
  FOREIGN KEY (role_id)
  REFERENCES [role] (role_id)

    ALTER TABLE account 
ADD CONSTRAINT FK_account_class
  FOREIGN KEY (class_id)
  REFERENCES class (class_id)


  ALTER TABLE class_assignment 
ADD CONSTRAINT FK_class_assignment_class
  FOREIGN KEY (class_id)
  REFERENCES [class] (class_id)

  ALTER TABLE class_assignment 
ADD CONSTRAINT FK_class_assignment_faculty
  FOREIGN KEY (faculty_id)
  REFERENCES account (account_id)


    ALTER TABLE pay 
ADD CONSTRAINT FK_pay_account
  FOREIGN KEY (account_id)
  REFERENCES account (account_id)


  ALTER TABLE batch 
ADD CONSTRAINT FK_batch_course
  FOREIGN KEY (course_id)
  REFERENCES course (course_id)

  ALTER TABLE batch 
ADD CONSTRAINT FK_batch_class
  FOREIGN KEY (class_id)
  REFERENCES class (class_id)


  
    ALTER TABLE course_subject 
	ADD CONSTRAINT FK_course_subject_course
  FOREIGN KEY (course_id)
  REFERENCES course (course_id)

      ALTER TABLE course_subject 
	ADd CONSTRAINT FK_course_subject_subject
  FOREIGN KEY (subject_id)
  REFERENCES [subject] (subject_id)


  ALTER TABLE attendance 
ADD CONSTRAINT FK_attendance_student
  FOREIGN KEY (student_id)
  REFERENCES account (account_id)

  
  ALTER TABLE attendance 
ADD CONSTRAINT FK_attendance_faculty
  FOREIGN KEY (faculty_id)
  REFERENCES account (account_id)

  
  ALTER TABLE attendance 
ADD CONSTRAINT FK_attendance_course
  FOREIGN KEY (subject_id)
  REFERENCES [subject] (subject_id)


  ALTER TABLE exam 
ADD CONSTRAINT FK_exam_subject
  FOREIGN KEY (subject_id)
  REFERENCES [subject] (subject_id)
  

  ALTER TABLE mark 
ADD CONSTRAINT FK_mark_exam
  FOREIGN KEY (exam_id)
  REFERENCES exam (exam_id)

    ALTER TABLE mark 
ADD CONSTRAINT FK_mark_account
  FOREIGN KEY (student_id)
  REFERENCES account (account_id)

    ALTER TABLE feedback 
ADD CONSTRAINT FK_feedback_subject
  FOREIGN KEY (subject_id)
  REFERENCES [subject] (subject_id)

   ALTER TABLE feedback_faculty 
	ADD CONSTRAINT FK_feedback_faculty_feedback
  FOREIGN KEY (feedback_id)
  REFERENCES feedback (feedback_id)

   ALTER TABLE feedback_faculty 
	ADD CONSTRAINT FK_feedback_faculty_faculty
  FOREIGN KEY (faculty_id)
  REFERENCES account (account_id)

ALTER TABLE schedule 
ADD CONSTRAINT FK_schedule_class
  FOREIGN KEY (class_id)
  REFERENCES class (class_id)

  ALTER TABLE schedule 
ADD CONSTRAINT FK_schedule_course
  FOREIGN KEY (subject_id)
  REFERENCES [subject] (subject_id)

    ALTER TABLE schedule 
ADD CONSTRAINT FK_schedule_account
  FOREIGN KEY (faculty_id)
  REFERENCES account (account_id)

  ALTER TABLE test_schedule 
ADD CONSTRAINT FK_test_schedule_class
  FOREIGN KEY (class_id)
  REFERENCES class (class_id)

  ALTER TABLE test_schedule 
ADD CONSTRAINT FK_test_schedule_exam
 FOREIGN KEY (exam_id)
  REFERENCES exam (exam_id)

    ALTER TABLE test_schedule 
ADD CONSTRAINT FK_test_schedule_account
  FOREIGN KEY (faculty_id)
  REFERENCES account (account_id)

      ALTER TABLE scholarship_student 
ADD CONSTRAINT FK_scholarship_student_account
  FOREIGN KEY (account_id)
  REFERENCES account (account_id)

      ALTER TABLE scholarship_student 
ADD CONSTRAINT FK_scholarship_student_scholarship
  FOREIGN KEY (scholarship_id)
  REFERENCES scholarship (scholarship_id)
  
	
	
INSERT [dbo].[role] ([role_id], [role_name], [desc], [status]) VALUES (N'role01', N'admin', N'This is admin', 1)
INSERT [dbo].[role] ([role_id], [role_name], [desc], [status]) VALUES (N'role02', N'faculty', N'This is faculty', 1)
INSERT [dbo].[role] ([role_id], [role_name], [desc], [status]) VALUES (N'role03', N'student', N'This is student', 1)
GO

INSERT [dbo].[class] ([class_id], [class_name], [number_of_student], [desc], [status]) VALUES (N'class02', N'ChemistryAB', 15, N'Class chemistry', 1)
INSERT [dbo].[class] ([class_id], [class_name], [number_of_student], [desc], [status]) VALUES (N'class03', N'GeographyBB', 15, N'Class geography', 1)
INSERT [dbo].[class] ([class_id], [class_name], [number_of_student], [desc], [status]) VALUES (N'class04', N'Class 1', 10, N'desc', 1)
GO

INSERT [dbo].[course] ([course_id], [course_name], [fee], [term], [certificate], [desc], [status]) VALUES (N'course01', N'Chem12021', 50.0000, N'3', N'Bachelor Of Chemistry Science', N'Take place within 3 years', 1)
INSERT [dbo].[course] ([course_id], [course_name], [fee], [term], [certificate], [desc], [status]) VALUES (N'course02', N'Geo2021', 40.0000, N'2', N'Bachelor Of Geography Science', N'Take place within 2 years', 1)
INSERT [dbo].[course] ([course_id], [course_name], [fee], [term], [certificate], [desc], [status]) VALUES (N'course03', N'Course 1', 200.0000, N'2', N'Certificate', N'desc', 1)
GO

INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject01', N'Chemistry Basic', N'chemistry basic', 1)
INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject02', N'Chemistry Advance 1', N'chemistry advance', 1)
INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject03', N'Chemistry Basic 1', N'chemistry basic 1', 1)
INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject04', N'Chemistry Basic 2', N'Chemistry Basic 2', 1)
INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject05', N'Chemistry Advance 2', N'Chemistry Advance 2', 1)
INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject06', N'Geography Basic 1', N'Geography Basic 1', 1)
INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject07', N'Geography Basic 2', N'Geography Basic 2', 1)
INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject08', N'Geography Advance', N'Geography Advance', 1)
INSERT [dbo].[subject] ([subject_id], [subject_name], [desc], [status]) VALUES (N'subject09', N'Subject 1', N'desc', 1)
GO

INSERT [dbo].[exam] ([exam_id], [subject_id], [title], [desc], [status]) VALUES (N'exam01', N'subject03', N'Exam chemistry basic 1st time', N'desc', 1)
INSERT [dbo].[exam] ([exam_id], [subject_id], [title], [desc], [status]) VALUES (N'exam02', N'subject06', N'Exam geography 1st time', N'desc', 1)
INSERT [dbo].[exam] ([exam_id], [subject_id], [title], [desc], [status]) VALUES (N'exam03', N'subject04', N'Exam chemistry basic 2nd time', N'desc', 1)
INSERT [dbo].[exam] ([exam_id], [subject_id], [title], [desc], [status]) VALUES (N'exam04', N'subject02', N'Exam chemistry advance 1st time', N'desc', 1)
INSERT [dbo].[exam] ([exam_id], [subject_id], [title], [desc], [status]) VALUES (N'exam05', N'subject05', N'Exam chemistry advance 2nd time', N'desc', 1)
INSERT [dbo].[exam] ([exam_id], [subject_id], [title], [desc], [status]) VALUES (N'exam06', N'subject07', N'Exam geography basic 1st time', N'desc', 1)
INSERT [dbo].[exam] ([exam_id], [subject_id], [title], [desc], [status]) VALUES (N'exam07', N'subject08', N'Exam geographt advance 1st time', N'desc', 1)
INSERT [dbo].[exam] ([exam_id], [subject_id], [title], [desc], [status]) VALUES (N'exam08', N'subject09', N'Exam 1', N'desc', 1)
GO

INSERT [dbo].[scholarship] ([scholarship_id], [scholarship_name], [discount], [desc], [status]) VALUES (N'scholarship01', N'Encouragement scholarships', N'10', N'encouragement scholarships', 1)
INSERT [dbo].[scholarship] ([scholarship_id], [scholarship_name], [discount], [desc], [status]) VALUES (N'scholarship02', N'Talent scholarships', N'20', N'talent scholarships', 1)
INSERT [dbo].[scholarship] ([scholarship_id], [scholarship_name], [discount], [desc], [status]) VALUES (N'scholarship03', N'Scholarship 1', N'10', N'desc', 1)
GO

INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course01', N'subject02', 1)
INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course01', N'subject03', 1)
INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course01', N'subject04', 1)
INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course01', N'subject05', 1)
INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course02', N'subject06', 1)
INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course02', N'subject07', 1)
INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course02', N'subject08', 1)
INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course03', N'subject02', 1)
INSERT [dbo].[course_subject] ([course_id], [subject_id], [status]) VALUES (N'course03', N'subject09', 1)
GO

INSERT [dbo].[batch] ([course_id], [class_id], [graduate], [start_date], [end_date], [status]) VALUES (N'course01', N'class02', 0, CAST(N'2021-07-09' AS Date), CAST(N'2021-08-09' AS Date), 1)
INSERT [dbo].[batch] ([course_id], [class_id], [graduate], [start_date], [end_date], [status]) VALUES (N'course02', N'class03', 0, CAST(N'2021-08-09' AS Date), CAST(N'2021-10-09' AS Date), 1)
INSERT [dbo].[batch] ([course_id], [class_id], [graduate], [start_date], [end_date], [status]) VALUES (N'course03', N'class04', 0, CAST(N'2021-07-10' AS Date), CAST(N'2021-07-30' AS Date), 1)
GO




INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc01', N'role01', NULL, N'admin', N'$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', N'admin', N'admin@gmail.com', CAST(N'2020-12-12' AS Date), N'Tay Ninh', 1, N'0000000000', N'e06d45a9e14c42f8a7472f28bdd034b7.jpeg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc02', N'role03', N'class02', N'account20627215', N'$2b$10$PYBdwa4TZVYZ.z6ijnpdceBiJdA5.wgUJ1vcRiH6TrgzI.BIYVYwW', N'Nguyen Hoang Ngoc Tran', N'a', CAST(N'2001-01-04' AS Date), N'Tay Ninh', 0, N'0999999999', N'null.jpg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc03', N'role02', NULL, N'account111111', N'$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', N'Tran Quoc Bao', N'gungin172@gmail.com', CAST(N'2001-10-18' AS Date), N'Dong Nai', 1, N'0123123123', N'null.jpg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc04', N'role03', N'class03', N'account05441294', N'$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', N'Le Phat Vinh', N'lephatvinh@gmail.com', CAST(N'2001-12-12' AS Date), N'Tay Ninh', 1, N'0101010101', N'null.jpg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc05', N'role03', N'class02', N'account05441295', N'$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', N'Tran Tran', N'trantran@gmail.com', CAST(N'2001-12-12' AS Date), N'Tay Ninh', 0, N'0101010101', N'null.jpg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc06', N'role03', N'class03', N'account20627216', N'$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', N'Mary', N'mary@gmail.com', CAST(N'2001-12-12' AS Date), N'Tay Ninh', 0, N'0101010101', N'null.jpg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc07', N'role03', N'class02', N'account05441296', N'$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', N'Peter', N'peter@gmail.com', CAST(N'2001-12-12' AS Date), N'TpHCM', 1, N'0101010101', N'null.jpg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc08', N'role03', N'class02', N'account16283255', N'$2b$10$wflOPwG/1X8Nj9z8Tv31fO4UMhP7EVlPOzAjk69CwDQRGsWCnk5Ce', N'Hellen', N'hellen@gmail.com', CAST(N'2001-01-01' AS Date), N'tp HCM', 0, N'0202020202', N'null.jpg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc09', N'role02', NULL, N'account88193836', N'$2b$10$SHnD.X8JFNR9N3xJmgR6xOimXNHxPfSzkZOmCk/ntJi3PWr8frp.2', N'bao tran', N'lephat8464@gmail.com', CAST(N'2001-07-12' AS Date), N'Address', 1, N'0123456789', N'null.jpg', 1, 1)
INSERT [dbo].[account] ([account_id], [role_id], [class_id], [username], [password], [fullname], [email], [dob], [address], [gender], [phone], [avatar], [active], [status]) VALUES (N'acc10', N'role03', N'class04', N'account77133007', N'$2b$10$plPXjpsqP.cOj05ro0zST.6GbqnOk/yg19g.iOLw6S4yCBE1K8HQe', N'Vinh', N'tranmini0401@gmail.com', CAST(N'2021-07-01' AS Date), N'address', 0, N'123456789', N'1c400639-8a4e-4e6a-909c-1339e8f84098.jpeg', 1, 1)
GO


INSERT [dbo].[scholarship_student] ([account_id], [scholarship_id], [status]) VALUES (N'acc02', N'scholarship02', 1)
INSERT [dbo].[scholarship_student] ([account_id], [scholarship_id], [status]) VALUES (N'acc10', N'scholarship03', 1)
GO

SET IDENTITY_INSERT [dbo].[pay] ON 

INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (12, N'acc02', N'Paypal', N'Coursefee', 50.0000, 10.0000, 40.0000, CAST(N'2021-07-09T02:48:15.120' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (13, N'acc04', N'Paypal', N'Coursefee', 50.0000, 0.0000, 50.0000, CAST(N'2021-07-09T02:58:52.270' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (14, N'acc08', N'Paypal', N'Coursefee', 50.0000, 0.0000, 50.0000, CAST(N'2021-07-09T03:22:05.773' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (15, N'acc05', N'Paypal', N'Finefee', 100.0000, 0.0000, 100.0000, CAST(N'2021-07-09T04:30:10.027' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (16, N'acc07', N'Paypal', N'Finefee', 100.0000, 0.0000, 100.0000, CAST(N'2021-07-09T04:30:13.267' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (17, N'acc08', N'Paypal', N'Finefee', 100.0000, 0.0000, 100.0000, CAST(N'2021-07-09T04:30:15.520' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (18, N'acc02', N'Paypal', N'Finefee', 100.0000, 0.0000, 100.0000, CAST(N'2021-07-09T04:30:51.883' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (19, N'acc05', N'Paypal', N'Finefee', 100.0000, 0.0000, 100.0000, CAST(N'2021-07-09T04:30:54.670' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (20, N'acc07', N'Paypal', N'Finefee', 100.0000, 0.0000, 100.0000, CAST(N'2021-07-09T04:30:57.697' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (21, N'acc08', N'Paypal', N'Finefee', 100.0000, 0.0000, 100.0000, CAST(N'2021-07-09T04:31:00.827' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (22, N'acc10', N'Paypal', N'Coursefee', 200.0000, 20.0000, 180.0000, CAST(N'2021-07-09T08:24:48.267' AS DateTime), NULL, 0)
INSERT [dbo].[pay] ([pay_id], [account_id], [payment], [title], [fee], [discount], [total], [date_request], [date_paid], [pay_status]) VALUES (23, N'acc10', N'Paypal', N'Finefee', 100.0000, 0.0000, 100.0000, CAST(N'2021-07-09T08:35:28.340' AS DateTime), NULL, 0)
SET IDENTITY_INSERT [dbo].[pay] OFF
GO

INSERT [dbo].[professional] ([faculty_id], [subject_id], [status]) VALUES (N'acc03', N'subject02', 1)
INSERT [dbo].[professional] ([faculty_id], [subject_id], [status]) VALUES (N'acc03', N'subject03', 1)
INSERT [dbo].[professional] ([faculty_id], [subject_id], [status]) VALUES (N'acc03', N'subject04', 1)
INSERT [dbo].[professional] ([faculty_id], [subject_id], [status]) VALUES (N'acc03', N'subject05', 1)
INSERT [dbo].[professional] ([faculty_id], [subject_id], [status]) VALUES (N'acc03', N'subject06', 1)
INSERT [dbo].[professional] ([faculty_id], [subject_id], [status]) VALUES (N'acc03', N'subject07', 1)
INSERT [dbo].[professional] ([faculty_id], [subject_id], [status]) VALUES (N'acc03', N'subject08', 1)
INSERT [dbo].[professional] ([faculty_id], [subject_id], [status]) VALUES (N'acc09', N'subject09', 1)
GO

SET IDENTITY_INSERT [dbo].[class_assignment] ON 

INSERT [dbo].[class_assignment] ([class_assignment_id], [faculty_id], [class_id], [subject_name], [status]) VALUES (38, N'acc03', N'class02', N'Chemistry Advance 1', 1)
INSERT [dbo].[class_assignment] ([class_assignment_id], [faculty_id], [class_id], [subject_name], [status]) VALUES (39, N'acc03', N'class02', N'Chemistry Basic 1', 1)
INSERT [dbo].[class_assignment] ([class_assignment_id], [faculty_id], [class_id], [subject_name], [status]) VALUES (40, N'acc03', N'class02', N'Chemistry Basic 2', 1)
INSERT [dbo].[class_assignment] ([class_assignment_id], [faculty_id], [class_id], [subject_name], [status]) VALUES (41, N'acc03', N'class02', N'Chemistry Advance 2', 1)
INSERT [dbo].[class_assignment] ([class_assignment_id], [faculty_id], [class_id], [subject_name], [status]) VALUES (42, N'acc03', N'class03', N'Geography Basic 1', 1)
INSERT [dbo].[class_assignment] ([class_assignment_id], [faculty_id], [class_id], [subject_name], [status]) VALUES (43, N'acc03', N'class03', N'Geography Basic 2', 1)
INSERT [dbo].[class_assignment] ([class_assignment_id], [faculty_id], [class_id], [subject_name], [status]) VALUES (44, N'acc03', N'class03', N'Geography Advance', 1)
INSERT [dbo].[class_assignment] ([class_assignment_id], [faculty_id], [class_id], [subject_name], [status]) VALUES (45, N'acc09', N'class04', N'Subject 1', 1)
SET IDENTITY_INSERT [dbo].[class_assignment] OFF
GO



SET IDENTITY_INSERT [dbo].[schedule] ON 

INSERT [dbo].[schedule] ([schedule_id], [class_id], [subject_id], [faculty_id], [time_day], [start_date], [end_date], [study_day], [status]) VALUES (29, N'class02', N'subject05', N'acc03', CAST(N'07:00:00' AS Time), CAST(N'2021-07-09' AS Date), CAST(N'2021-07-16' AS Date), N'Friday,Saturday,Sunday', 1)
INSERT [dbo].[schedule] ([schedule_id], [class_id], [subject_id], [faculty_id], [time_day], [start_date], [end_date], [study_day], [status]) VALUES (30, N'class02', N'subject04', N'acc03', CAST(N'08:00:00' AS Time), CAST(N'2021-07-17' AS Date), CAST(N'2021-07-23' AS Date), N'Monday,Tuesday,Saturday', 1)
INSERT [dbo].[schedule] ([schedule_id], [class_id], [subject_id], [faculty_id], [time_day], [start_date], [end_date], [study_day], [status]) VALUES (31, N'class02', N'subject03', N'acc03', CAST(N'09:00:00' AS Time), CAST(N'2021-07-24' AS Date), CAST(N'2021-07-30' AS Date), N'Monday,Tuesday,Wednesday', 1)
INSERT [dbo].[schedule] ([schedule_id], [class_id], [subject_id], [faculty_id], [time_day], [start_date], [end_date], [study_day], [status]) VALUES (32, N'class02', N'subject02', N'acc03', CAST(N'14:00:00' AS Time), CAST(N'2021-08-01' AS Date), CAST(N'2021-08-09' AS Date), N'Monday,Wednesday,Friday', 1)
INSERT [dbo].[schedule] ([schedule_id], [class_id], [subject_id], [faculty_id], [time_day], [start_date], [end_date], [study_day], [status]) VALUES (33, N'class03', N'subject08', N'acc03', CAST(N'13:00:00' AS Time), CAST(N'2021-08-09' AS Date), CAST(N'2021-08-24' AS Date), N'Monday,Wednesday,Friday', 1)
INSERT [dbo].[schedule] ([schedule_id], [class_id], [subject_id], [faculty_id], [time_day], [start_date], [end_date], [study_day], [status]) VALUES (34, N'class03', N'subject07', N'acc03', CAST(N'15:00:00' AS Time), CAST(N'2021-08-25' AS Date), CAST(N'2021-09-09' AS Date), N'Wednesday,Thursday,Friday', 1)
INSERT [dbo].[schedule] ([schedule_id], [class_id], [subject_id], [faculty_id], [time_day], [start_date], [end_date], [study_day], [status]) VALUES (35, N'class03', N'subject06', N'acc03', CAST(N'09:00:00' AS Time), CAST(N'2021-09-10' AS Date), CAST(N'2021-10-09' AS Date), N'Wednesday,Thursday,Friday', 1)
INSERT [dbo].[schedule] ([schedule_id], [class_id], [subject_id], [faculty_id], [time_day], [start_date], [end_date], [study_day], [status]) VALUES (36, N'class04', N'subject09', N'acc09', CAST(N'10:30:00' AS Time), CAST(N'2021-07-12' AS Date), CAST(N'2021-07-31' AS Date), N'Monday,Wednesday,Friday', 1)
SET IDENTITY_INSERT [dbo].[schedule] OFF
GO

SET IDENTITY_INSERT [dbo].[test_schedule] ON 

INSERT [dbo].[test_schedule] ([test_schedule_id], [class_id], [exam_id], [faculty_id], [date], [status]) VALUES (14, N'class02', N'exam05', N'acc03', CAST(N'2021-07-13T14:00:00.000' AS DateTime), 1)
INSERT [dbo].[test_schedule] ([test_schedule_id], [class_id], [exam_id], [faculty_id], [date], [status]) VALUES (15, N'class02', N'exam03', N'acc03', CAST(N'2021-07-31T08:00:00.000' AS DateTime), 1)
INSERT [dbo].[test_schedule] ([test_schedule_id], [class_id], [exam_id], [faculty_id], [date], [status]) VALUES (16, N'class02', N'exam01', N'acc03', CAST(N'2021-08-01T15:00:00.000' AS DateTime), 1)
INSERT [dbo].[test_schedule] ([test_schedule_id], [class_id], [exam_id], [faculty_id], [date], [status]) VALUES (17, N'class02', N'exam04', N'acc03', CAST(N'2021-07-09T07:00:00.000' AS DateTime), 1)
INSERT [dbo].[test_schedule] ([test_schedule_id], [class_id], [exam_id], [faculty_id], [date], [status]) VALUES (18, N'class03', N'exam07', N'acc03', CAST(N'2021-07-31T15:00:00.000' AS DateTime), 1)
INSERT [dbo].[test_schedule] ([test_schedule_id], [class_id], [exam_id], [faculty_id], [date], [status]) VALUES (19, N'class03', N'exam06', N'acc03', CAST(N'2021-08-31T04:27:00.000' AS DateTime), 1)
INSERT [dbo].[test_schedule] ([test_schedule_id], [class_id], [exam_id], [faculty_id], [date], [status]) VALUES (20, N'class03', N'exam02', N'acc03', CAST(N'2021-10-09T08:00:00.000' AS DateTime), 1)
INSERT [dbo].[test_schedule] ([test_schedule_id], [class_id], [exam_id], [faculty_id], [date], [status]) VALUES (21, N'class04', N'exam08', N'acc09', CAST(N'2021-07-10T10:30:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[test_schedule] OFF
GO

SET IDENTITY_INSERT [dbo].[attendance] ON 

INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (37, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (38, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-28T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (39, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-27T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (40, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-22T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (41, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-21T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (42, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (43, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-15T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (44, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-14T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (45, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (46, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-29T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (47, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-28T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (48, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-27T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (49, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-22T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (50, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-21T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (51, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (52, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-15T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (53, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-14T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (54, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (55, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-29T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (56, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-28T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (57, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-27T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (58, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-22T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (59, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-21T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (60, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (61, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-15T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (62, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-14T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (63, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-29T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (64, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (65, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-31T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (66, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-08-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (67, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-08-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (68, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-08-07T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (69, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (70, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (71, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (72, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (73, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (74, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (75, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (76, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (77, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-24T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (78, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (79, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-30T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (80, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-07-31T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (81, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-08-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (82, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-08-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (83, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-08-07T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (84, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-30T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (85, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (86, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-24T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (87, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (88, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (89, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (90, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (91, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (92, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (93, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (94, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-24T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (95, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (96, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-30T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (97, N'class02', N'acc06', N'acc03', N'subject05', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (98, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-31T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (99, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-08-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (100, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-08-07T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (101, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (102, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (103, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (104, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (105, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (106, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (107, N'class02', N'acc04', N'acc03', N'subject05', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (108, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-08-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (109, N'class02', N'acc02', N'acc03', N'subject04', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (110, N'class02', N'acc02', N'acc03', N'subject04', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (111, N'class02', N'acc02', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (112, N'class02', N'acc04', N'acc03', N'subject04', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (113, N'class02', N'acc04', N'acc03', N'subject04', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (114, N'class02', N'acc04', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (115, N'class02', N'acc06', N'acc03', N'subject04', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (116, N'class02', N'acc06', N'acc03', N'subject04', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (117, N'class02', N'acc06', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (118, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-18T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (119, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (120, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (121, N'class02', N'acc04', N'acc03', N'subject03', CAST(N'2021-07-18T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (122, N'class02', N'acc04', N'acc03', N'subject03', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (123, N'class02', N'acc04', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (124, N'class02', N'acc06', N'acc03', N'subject03', CAST(N'2021-07-18T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (125, N'class02', N'acc06', N'acc03', N'subject03', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (126, N'class02', N'acc06', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (127, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-07-25T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (128, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (129, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-07-29T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (130, N'class02', N'acc04', N'acc03', N'subject02', CAST(N'2021-07-25T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (131, N'class02', N'acc04', N'acc03', N'subject02', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (132, N'class02', N'acc04', N'acc03', N'subject02', CAST(N'2021-07-29T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (133, N'class02', N'acc06', N'acc03', N'subject02', CAST(N'2021-07-25T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (134, N'class02', N'acc06', N'acc03', N'subject02', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (135, N'class02', N'acc06', N'acc03', N'subject02', CAST(N'2021-07-29T00:00:00.000' AS DateTime), 0, 0)
GO
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (136, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (137, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (138, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (139, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (140, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (141, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-18T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (142, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (143, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (144, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (145, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (146, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (147, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-18T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (148, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-08-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (149, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-08-24T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (150, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-08-25T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (151, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-08-30T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (152, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-08-31T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (153, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-08-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (154, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-08-24T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (155, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-08-25T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (156, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-08-30T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (157, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-08-31T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (158, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-01T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (159, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (160, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (161, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-08T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (162, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (163, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-01T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (164, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (165, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (166, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-08T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (167, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (168, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (169, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (170, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (171, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (172, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (173, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (174, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (175, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (176, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (177, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-07-14T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (178, N'class02', N'acc05', N'acc03', N'subject02', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (179, N'class02', N'acc05', N'acc03', N'subject02', CAST(N'2021-07-14T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (180, N'class02', N'acc07', N'acc03', N'subject02', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (181, N'class02', N'acc07', N'acc03', N'subject02', CAST(N'2021-07-14T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (182, N'class02', N'acc08', N'acc03', N'subject02', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (183, N'class02', N'acc08', N'acc03', N'subject02', CAST(N'2021-07-14T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (184, N'class02', N'acc02', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (185, N'class02', N'acc05', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (186, N'class02', N'acc07', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (187, N'class02', N'acc08', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (188, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (189, N'class02', N'acc05', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (190, N'class02', N'acc07', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (191, N'class02', N'acc08', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (192, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (193, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (194, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (195, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (196, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (197, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (198, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (199, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (200, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (201, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (202, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (203, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (204, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (205, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (206, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (207, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (208, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (209, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (210, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (211, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (212, N'class02', N'acc05', N'acc03', N'subject03', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (213, N'class02', N'acc05', N'acc03', N'subject03', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (214, N'class02', N'acc05', N'acc03', N'subject03', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (215, N'class02', N'acc05', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (216, N'class02', N'acc07', N'acc03', N'subject03', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (217, N'class02', N'acc07', N'acc03', N'subject03', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (218, N'class02', N'acc07', N'acc03', N'subject03', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (219, N'class02', N'acc07', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (220, N'class02', N'acc08', N'acc03', N'subject03', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (221, N'class02', N'acc08', N'acc03', N'subject03', CAST(N'2021-07-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (222, N'class02', N'acc08', N'acc03', N'subject03', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (223, N'class02', N'acc08', N'acc03', N'subject03', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (224, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (225, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (226, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (227, N'class02', N'acc02', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (228, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (229, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (230, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (231, N'class02', N'acc05', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (232, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (233, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (234, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (235, N'class02', N'acc07', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
GO
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (236, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (237, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (238, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (239, N'class02', N'acc08', N'acc03', N'subject05', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (240, N'class02', N'acc02', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (241, N'class02', N'acc02', N'acc03', N'subject04', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (242, N'class02', N'acc02', N'acc03', N'subject04', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (243, N'class02', N'acc05', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (244, N'class02', N'acc05', N'acc03', N'subject04', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (245, N'class02', N'acc05', N'acc03', N'subject04', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (246, N'class02', N'acc07', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (247, N'class02', N'acc07', N'acc03', N'subject04', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (248, N'class02', N'acc07', N'acc03', N'subject04', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (249, N'class02', N'acc08', N'acc03', N'subject04', CAST(N'2021-07-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (250, N'class02', N'acc08', N'acc03', N'subject04', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (251, N'class02', N'acc08', N'acc03', N'subject04', CAST(N'2021-07-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (252, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (253, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-27T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (254, N'class02', N'acc02', N'acc03', N'subject03', CAST(N'2021-07-28T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (255, N'class02', N'acc05', N'acc03', N'subject03', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (256, N'class02', N'acc05', N'acc03', N'subject03', CAST(N'2021-07-27T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (257, N'class02', N'acc05', N'acc03', N'subject03', CAST(N'2021-07-28T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (258, N'class02', N'acc07', N'acc03', N'subject03', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (259, N'class02', N'acc07', N'acc03', N'subject03', CAST(N'2021-07-27T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (260, N'class02', N'acc07', N'acc03', N'subject03', CAST(N'2021-07-28T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (261, N'class02', N'acc08', N'acc03', N'subject03', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (262, N'class02', N'acc08', N'acc03', N'subject03', CAST(N'2021-07-27T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (263, N'class02', N'acc08', N'acc03', N'subject03', CAST(N'2021-07-28T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (264, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-08-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (265, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-08-04T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (266, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-08-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (267, N'class02', N'acc02', N'acc03', N'subject02', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (268, N'class02', N'acc05', N'acc03', N'subject02', CAST(N'2021-08-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (269, N'class02', N'acc05', N'acc03', N'subject02', CAST(N'2021-08-04T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (270, N'class02', N'acc05', N'acc03', N'subject02', CAST(N'2021-08-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (271, N'class02', N'acc05', N'acc03', N'subject02', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (272, N'class02', N'acc07', N'acc03', N'subject02', CAST(N'2021-08-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (273, N'class02', N'acc07', N'acc03', N'subject02', CAST(N'2021-08-04T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (274, N'class02', N'acc07', N'acc03', N'subject02', CAST(N'2021-08-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (275, N'class02', N'acc07', N'acc03', N'subject02', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (276, N'class02', N'acc08', N'acc03', N'subject02', CAST(N'2021-08-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (277, N'class02', N'acc08', N'acc03', N'subject02', CAST(N'2021-08-04T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (278, N'class02', N'acc08', N'acc03', N'subject02', CAST(N'2021-08-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (279, N'class02', N'acc08', N'acc03', N'subject02', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (280, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (281, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (282, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (283, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (284, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-18T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (285, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (286, N'class03', N'acc04', N'acc03', N'subject08', CAST(N'2021-08-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (287, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (288, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-11T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (289, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-13T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (290, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (291, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-18T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (292, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-20T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (293, N'class03', N'acc06', N'acc03', N'subject08', CAST(N'2021-08-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (294, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-08-25T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (295, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-08-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (296, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-08-27T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (297, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-09-01T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (298, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-09-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (299, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-09-03T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (300, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-09-08T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (301, N'class03', N'acc04', N'acc03', N'subject07', CAST(N'2021-09-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (302, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-08-25T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (303, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-08-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (304, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-08-27T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (305, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-09-01T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (306, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-09-02T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (307, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-09-03T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (308, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-09-08T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (309, N'class03', N'acc06', N'acc03', N'subject07', CAST(N'2021-09-09T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (310, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (311, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-10-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (312, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-10-01T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (313, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-30T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (314, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-29T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (315, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-24T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (316, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (317, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-22T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (318, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (319, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (320, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-15T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (321, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-09-10T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (322, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-10-08T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (323, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-10-07T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (324, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-10-06T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (325, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-10-01T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (326, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-30T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (327, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-29T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (328, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-24T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (329, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (330, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-22T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (331, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-17T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (332, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (333, N'class03', N'acc04', N'acc03', N'subject06', CAST(N'2021-09-15T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (334, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-10-07T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (335, N'class03', N'acc06', N'acc03', N'subject06', CAST(N'2021-10-08T00:00:00.000' AS DateTime), 0, 0)
GO
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (336, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-12T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (337, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-14T00:00:00.000' AS DateTime), 1, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (338, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-16T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (339, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-19T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (340, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-21T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (341, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-23T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (342, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-26T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (343, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-28T00:00:00.000' AS DateTime), 0, 0)
INSERT [dbo].[attendance] ([attendance_id], [class_id], [student_id], [faculty_id], [subject_id], [date], [checked], [status]) VALUES (344, N'class04', N'acc10', N'acc09', N'subject09', CAST(N'2021-07-30T00:00:00.000' AS DateTime), 0, 0)
SET IDENTITY_INSERT [dbo].[attendance] OFF
GO

SET IDENTITY_INSERT [dbo].[feedback] ON 

INSERT [dbo].[feedback] ([feedback_id], [subject_id], [Teaching], [Exercises], [TeacherEthics], [Specialize], [Assiduous], [note], [status]) VALUES (5, N'subject03', 12, 12, 12, 12, 12, N'asdf', 1)
INSERT [dbo].[feedback] ([feedback_id], [subject_id], [Teaching], [Exercises], [TeacherEthics], [Specialize], [Assiduous], [note], [status]) VALUES (6, N'subject04', 23, 23, 23, 12, 23, N'aa', 1)
INSERT [dbo].[feedback] ([feedback_id], [subject_id], [Teaching], [Exercises], [TeacherEthics], [Specialize], [Assiduous], [note], [status]) VALUES (7, N'subject09', 40, 30, 50, 60, 20, N'note', 1)
SET IDENTITY_INSERT [dbo].[feedback] OFF
GO

SET IDENTITY_INSERT [dbo].[mark] ON 

INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (13, N'exam05', N'acc02', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 30, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (14, N'exam05', N'acc05', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 30, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (15, N'exam05', N'acc07', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 30, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (16, N'exam05', N'acc08', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 30, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (17, N'exam03', N'acc02', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 40, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (18, N'exam03', N'acc05', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 40, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (19, N'exam03', N'acc07', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 40, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (20, N'exam03', N'acc08', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 40, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (21, N'exam01', N'acc02', CAST(3.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 30, N'fail', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (22, N'exam01', N'acc05', CAST(0.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 30, N'fail', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (23, N'exam01', N'acc07', CAST(0.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 30, N'fail', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (24, N'exam01', N'acc08', CAST(0.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 30, N'fail', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (25, N'exam04', N'acc02', CAST(8.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 40, N'pass', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (26, N'exam04', N'acc05', CAST(0.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 40, N'fail', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (27, N'exam04', N'acc07', CAST(0.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 40, N'fail', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (28, N'exam04', N'acc08', CAST(0.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 40, N'fail', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (29, N'exam07', N'acc04', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 30, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (30, N'exam07', N'acc06', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 30, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (31, N'exam06', N'acc04', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 30, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (32, N'exam06', N'acc06', CAST(0.00 AS Decimal(5, 2)), CAST(10.00 AS Decimal(5, 2)), 30, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (33, N'exam02', N'acc04', CAST(0.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 40, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (34, N'exam02', N'acc06', CAST(0.00 AS Decimal(5, 2)), CAST(20.00 AS Decimal(5, 2)), 40, N'grading', 1)
INSERT [dbo].[mark] ([mark_id], [exam_id], [student_id], [mark], [max_mark], [rate], [status_mark], [status]) VALUES (35, N'exam08', N'acc10', CAST(60.00 AS Decimal(5, 2)), CAST(100.00 AS Decimal(5, 2)), 40, N'pass', 1)
SET IDENTITY_INSERT [dbo].[mark] OFF
GO

INSERT [dbo].[feedback_faculty] ([feedback_id], [faculty_id], [status]) VALUES (6, N'acc03', 1)
INSERT [dbo].[feedback_faculty] ([feedback_id], [faculty_id], [status]) VALUES (7, N'acc09', 1)
GO



SET IDENTITY_INSERT [dbo].[enquiry] ON 

INSERT [dbo].[enquiry] ([id], [title], [answer], [status]) VALUES (1, NULL, NULL, 0)
INSERT [dbo].[enquiry] ([id], [title], [answer], [status]) VALUES (2, N'How about environment?', N'Friendly, Faculty is very cute and good teach', 1)
INSERT [dbo].[enquiry] ([id], [title], [answer], [status]) VALUES (3, NULL, NULL, 0)
INSERT [dbo].[enquiry] ([id], [title], [answer], [status]) VALUES (4, N'Fee', N'150$', 1)
SET IDENTITY_INSERT [dbo].[enquiry] OFF
GO
SET IDENTITY_INSERT [dbo].[mail] ON 

INSERT [dbo].[mail] ([mail_id], [title], [email_user], [fullname], [phone_number], [content], [reply_content], [send_date], [reply_date], [check], [status]) VALUES (7, N'abc, Class: class1', N'tranmini0401@gmail.com', N'tran nguyen', N'0123456789', N'abchfkdf', N'oke ', CAST(N'2021-07-09T08:45:44.000' AS DateTime), CAST(N'2021-07-09T08:46:27.767' AS DateTime), 1, 1)
INSERT [dbo].[mail] ([mail_id], [title], [email_user], [fullname], [phone_number], [content], [reply_content], [send_date], [reply_date], [check], [status]) VALUES (8, N'Y kien tap the | Class: Class 1', N'tranmini0401@gmail.com', N'Vinh', N'123456789', N'Em muon them may tinh cho phong hoc Class 1', N'nha truong se sap xep cho lop', CAST(N'2021-07-09T21:58:53.000' AS DateTime), CAST(N'2021-07-09T21:59:42.333' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[mail] OFF
GO



--Note: All Password in Account is '1'