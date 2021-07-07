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
  

  --add role for admin , faculty and student
  insert into [role] values('role01', 'admin', 'admin', 'true')
  insert into [role] values('role02', 'faculty', 'faculty', 'true')
  insert into [role] values('role03', 'student', 'student', 'true')


    --add class 
  insert into class values('class01', 'C1908I1', 20, '20 students', 'true')
  insert into class values('class02', 'C1908I2', 25, '25 students', 'true')
  insert into class values('class03', 'C1908I3', 15, '15 students', 'true')
  insert into class values('class04', 'C1908I4', 25, '25 students', 'true')

  
    --add subject 
  insert into [subject] values('subject01', 'JS', 'Learn how to use js','true')
  insert into [subject] values('subject02', 'PHP','Learn how to use PHP' ,'true')
  insert into [subject] values('subject03', 'C#','Learn how to use C#' ,'true')
   insert into [subject] values('subject04', 'HTML','Learn how to use HTML' ,'true')

   --add scholarship 
     insert into scholarship values('scholarship01', 'Scholarship 1', '10', 'discount 10 percent', 'true')
     insert into scholarship values('scholarship02', 'Scholarship 2', '20', 'discount 20 percent', 'true')
     insert into scholarship values('scholarship03', 'Scholarship 3', '30', 'discount 30 percent', 'true')
     insert into scholarship values('scholarship04', 'Scholarship 4', '50', 'discount 50 percent', 'true')
   
          --add course 
  insert into course values('course01', 'Course 1', 10, '2 years', 'web certificate', 'easy to learn', 'true')
  insert into course values('course02', 'Course 2', 20, '2 years', 'web certificate', 'easy to learn', 'true')
  insert into course values('course03', 'Course 3', 10, '2 years', 'web certificate', 'easy to learn', 'true')
  insert into course values('course04', 'Course 4', 20, '2 years', 'web certificate', 'easy to learn', 'true')
  insert into course values('course05', 'Course 5', 30, '2 years', 'web certificate', 'easy to learn', 'true')
 

  --// add account admin, faculty, student
 insert into account values('acc01', 'role01', null, 'admin', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'admin', 'admin@gmail.com', '2020-12-12', '', 'True', '', 'e06d45a9e14c42f8a7472f28bdd034b7.jpeg', 'true', 'true')
 --// faculty 

 insert into account values('acc02', 'role02', null, 'account2021002', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'teacher1', 'teacher1@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')
 insert into account values('acc03', 'role02', null, 'account2021003', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'teacher2', 'teacher2@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')
 insert into account values('acc04', 'role02', null, 'account2021004', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'teacher3', 'teacher3@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')
 insert into account values('acc05', 'role02', null, 'account2021005', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'teacher4', 'teacher3@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')
 insert into account values('acc06', 'role02', null, 'account2021006', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'teacher5', 'teacher3@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')

 --// student
 insert into account values('acc07', 'role03','class01', 'account2021007', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'student1', 'student1@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')
 insert into account values('acc08', 'role03','class01', 'account2021008', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'student2', 'student2@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')
 insert into account values('acc09', 'role03','class01', 'account2021009', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'student3', 'student3@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')
 insert into account values('acc10', 'role03','class01', 'account20210010', '$2b$10$v0VEYPQD9dCkBKyocENZrO007bQ1s5GRiUg15sKyDxsuVOVT/.YYG', 'student4', 'student4@gmail.com', '2021-06-11 20:40:00.000', 'address', 'true', '00000000000', 'null.jpg', 'true', 'true')

  --add professional 
  insert into professional values('acc02', 'subject01', 'true')
  insert into professional values('acc02', 'subject02', 'true')
  insert into professional values('acc03', 'subject03', 'true')
  insert into professional values('acc03', 'subject04', 'true')
  insert into professional values('acc05', 'subject01', 'true')
  insert into professional values('acc05', 'subject02', 'true')
  insert into professional values('acc06', 'subject03', 'true')
 


        --add enquiry 
  --insert into enquiry values( 'Bao nhieu tien?', 'Rat nhieu nha', 'true')
  --insert into enquiry values( 'Hoc bao lau?', 'Rat lau nha', 'true')
  --insert into enquiry values( 'Co gai dep khong?', 'Rat nhieu nha', 'true')
  --insert into enquiry values( 'Co hoc bong gi khong?', 'co nha', 'true')
  --insert into enquiry values( 'Bao nhieu tien?', 'Rat nhieu nha', 'true')
  --insert into enquiry values( 'Bao nhieu tien?', 'Rat nhieu nha', 'true')

  -- add exam
  --insert into exam values('exam1', 'subject1', 'Thi giua mon', 'Khong kho lam')
  --insert into exam values('exam2', 'subject1', 'Thi cuoi mon', 'Khong kho lam')
  --insert into exam values('exam3', 'subject2', 'Thi giua mon', 'Khong kho lam')
  --insert into exam values('exam4', 'subject3', 'Thi giua mon', 'Khong kho lam')
  --insert into exam values('exam5', 'subject4', 'Thi giua mon', 'Khong kho lam')

  -- add feedback

insert into feedback values( 'subject01', 20, 40, 20, 60, 80, 'ok', 'true')
insert into feedback values( 'subject01', 20, 60, 20, 20, 20, 'ok', 'true')
insert into feedback values( 'subject01', 20, 80, 40, 60, 60, 'ok', 'true')

  -- add mark 
  
  --add courese_subject 
  insert into course_subject values('course01', 'subject01', 'true')
  insert into course_subject values('course01', 'subject02', 'true')
  insert into course_subject values('course01', 'subject03', 'true')
  insert into course_subject values('course02', 'subject02', 'true')
  insert into course_subject values('course02', 'subject03', 'true')
  insert into course_subject values('course02', 'subject04', 'true')
  insert into course_subject values('course03', 'subject01', 'true')
  insert into course_subject values('course03', 'subject02', 'true')
  insert into course_subject values('course03', 'subject03', 'true')


  --add batch 
  insert into batch values('course01', 'class01', 'false', '2020-12-12', '2021-12-12', 'true')
  insert into batch values('course02', 'class02', 'false', '2020-12-12', '2021-12-12', 'true')
  insert into batch values('course03', 'class03', 'false', '2020-12-12', '2021-12-12', 'true')
  insert into batch values('course04', 'class04', 'false', '2020-12-12', '2021-12-12', 'true')
 

 
    --add class_assignment 
  insert into class_assignment values('acc02', 'class01', 'JS', 'true')
  insert into class_assignment values('acc03', 'class01', 'C#', 'true')
  insert into class_assignment values('acc04', 'class02', 'HTML', 'true')
  insert into class_assignment values('acc02', 'class02', 'JS', 'true')


	-- add enquiry
  insert into enquiry values('How many fee in this school?', 'Scope 3043$', 'true'),
							('How about environment?', 'Friendly, Faculty is very cute and good teach', 'true'),
							('What is this schoole specialize teaches?', 'Our school teaching IT', 'true')
	
	