# ProjectSemester3New

This is a Education Management System project.

⦁	Technical:
  - ASP.Net Core
  - HTML, CSS, JS, Jquery, Ajax, C#
  - SQL server
  
  - Soft ware: Visual studio 2019 .Net frameword,  SQL Server Management Studio (SSMS) 18.7
  - Hard ware: 
     + A minimum computer system that will help you access all the tools in the
                courses is a Pentium 166 or better  

     + 128 Megabytes of RAM or better   
   


  
Feature: 
  -Admin:
    + Manage Student
    + Manage faculty
    + Manage Email from client
    + Manage Pay 
    + Create Role
    + Create Class
    + Create Course
    + Create Subject
    + Create Exam in Subjects
    + Create Scholarship
    + Create Batch 
    + Create Schedule and Test-Schedule in class
    + Create Account with two roles(student and faculty)
    + Create Professional for faculty
    + Create Class-assignment 
    + Create Feedback from student about faculty
    + Report about Pay, Batch and student
    + See admin's profile
    
-Faculty: 
    + Manage student in class-assignment
    + Check attendance
    + Mark
    + See Schedule
    + See Feedback from student
    + See basic student's infomation in class-assignment
    + See faculty's profile

-Student: 
    + See student's profile
    + See Schedule and Test-Schedule
    + See attendance
    + See mark
    + Pay Course's fee
    + Send feedback to admin
    + See enquiry about school
    + See report about batch , course, diligence, paid, discount
    
    
User manual:

  - First, open sql file and run in sql server localhost (SQL Server Management Studio)
    + Open project with file ProjectSemester3.sln
    + Open file appsettings.json and change your server, Gmail's infomation
    + -----
      "PayPal": {
    "AuthToken": "OQPiChkfE6WEmZjOwDOmvUFG1LoFTZYhta-zDU7lgAmf9eL87tzblcyglnK", // This is your Token from paypal
    "PostUrl": "https://www.sandbox.paypal.com/cgi-bin/webscr",  // This is posturl to paypal
    "Business": "sb-3wi9o6214446@business.example.com",   // This is your email Business's paypal
    "ReturnUrl": "http://localhost:58026/student/pays/success" // This is return link after pay successfully
  },
    
  - Second, after change setting you ran run project, The default admin's account in file SQLQUERY_1.sql you can see username and password in it.
  - In The Interface of admin page, Please make sure you manage admin in the following order:
   1. Create Role
   2. Create Class (limit student)
   3. Create Course
   4. Create Subject
   5. Create Exam
   6. Create Scholarship
   7. Create Course_Subject
   8. Create Batch: 1 Class jus have 1 Course
   9. Then Create Account(Facutly with Role-faculty, Student with Role-student)
   10. Create Professional for faculty
   11. Create Class-assignment 
   12. Create Schedule and test_schedule for all class

    
After that, you can login by faculty's account and student's account. ALl feature worked!
    
    
    
    
    
    
    
    
