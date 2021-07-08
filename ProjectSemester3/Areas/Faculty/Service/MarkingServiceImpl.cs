using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using ProjectSemester3.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Faculty.Service
{
    public class MarkingServiceImpl : IMarkingService
    {
        private DatabaseContext _context;
        private readonly IMailService mailService;
        public MarkingServiceImpl(DatabaseContext context, IMailService _mailService)
        {
            mailService = _mailService;
            _context = context;
        }

        public Account getaccount(string username)
        {
            return _context.Accounts.FirstOrDefault(m => m.Username == username);
        }

        public List<Class> classes(string facultyid)
        {
            List<Class> classes = new List<Class>();
            foreach (var classid in _context.ClassAssignments.Where(m => m.FacultyId == facultyid).Distinct().Select(n => n.Class.ClassId).ToList())
            {
                classes = classes.Union(_context.Classes.Where(m => m.ClassId == classid).ToList()).ToList();
            }
            return classes;
        }
        public List<Mark> students(string exammid)
        {
            return _context.Marks.Where(m => m.Exam.ExamId == exammid).ToList();

        }

        public List<Subject> subjects(string classid)
        {
            return _context.CourseSubjects.Where(m => m.CourseId == _context.Courses.FirstOrDefault(m => m.CourseId == _context.Batches.FirstOrDefault(m => m.ClassId == classid).CourseId).CourseId).Select(m => m.Subject).ToList();
        }
        public List<Exam> exams(string subjectid, string classid)
        {


            return _context.Exams.Where(m => m.SubjectId == subjectid).ToList();
        }

        public Account getfaculty(string facultyid)
        {
            return _context.Accounts.FirstOrDefault(m => m.AccountId == facultyid);
        }





        public Class getclass(string classid)
        {
            return _context.Classes.FirstOrDefault(m => m.ClassId == classid);
        }

        public Exam getexam(string examid)
        {

            return _context.Exams.FirstOrDefault(m => m.ExamId == examid);
        }



        public async Task<dynamic> update(List<Mark> marks)
        {
            foreach (var mark in marks)

            {
                if ((decimal)(mark.Mark1 / mark.MaxMark) * 100 >= mark.Rate)
                {
                    mark.StatusMark = "pass";
                    _context.Entry(mark).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    mark.StatusMark = "fail";
;
                    _context.Entry(mark).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _context.SaveChangesAsync();
                   await CreatePay(_context.Accounts.FirstOrDefault(m => m.AccountId == mark.StudentId).AccountId, _context.Accounts.FirstOrDefault(m => m.AccountId == mark.StudentId).Email);
                }


            }
          return 1;
            
        }
        public async Task<dynamic> CreatePay(string AccountId, string email)
        {

            var pay = new Pay();
            pay.AccountId = AccountId;
            pay.Payment = "Paypal";
            pay.Title = "Finefee";
            pay.Fee = 100;
            pay.Discount = 0;
            pay.Total = pay.Fee - pay.Discount;
            pay.DateRequest = DateTime.Now;
            pay.DatePaid = null;
            pay.PayStatus = false;
            _context.Pays.Add(pay);
            await _context.SaveChangesAsync();
            string content = "<h2>Your finfee is " + Convert.ToInt32(pay.Total) + ".</h2> <br/>" + "Please pay it in the shortest time!" + "<br/><br/>" + "Best regards," + "<br/>" + "<b>Zeal Education<b/>" + "<br/><br/> Link: http://localhost:58026/";
            mailService.Reply(email, pay.Title, content);
            return true;
        }
    }
}
