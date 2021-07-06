using ProjectSemester3.Areas.Admin.Entities;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class AttendanceServiceImpl : IAttendanceService
    {
        private DatabaseContext context;

        public AttendanceServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public List<AccountAttendance> SelectAll(string classid)
        {
            //var listAccount = from a in context.Accounts
            //                  join b in context.Batches on a.AccountId equals b.StudentId
            //                  join c in context.Courses on b.CourseId equals c.CourseId
            //                  join cs in context.CourseSubjects on c.CourseId equals cs.CourseId
            //                  join s in context.Subjects on cs.SubjectId equals s.SubjectId
            //                  where classid == b.ClassId && subjectid == s.SubjectId
            //                  select new
            //                  {
            //                      Account_ID = a.AccountId,
            //                      Fullname = a.Fullname,
            //                      Email = a.Email,
            //                      Gender = a.Gender,
            //                      Class = b.Class.ClassName,
            //                      Faculty = b.Faculty.Fullname,
            //                      Subject = s.SubjectId
            //                  };

            //============================


            //var list = from batches in context.Batches
            //           from attendances in context.Attendances
            //           where
            //             batches.Class.ClassId == "ABC1"
            //           group new { batches.Student, context.Courses, batches, attendances } by new
            //           {
            //               batches.Student.AccountId,
            //               batches.Student.Fullname,
            //               batches.Student.Email,
            //               gender = (bool?)batches.Student.Gender,
            //               attendances.Subject.SubjectName,
            //               batches.FacultyId,
            //               attendances.Checked
            //           } into g
            //           select new
            //           {
            //               g.Key.AccountId,
            //               g.Key.Fullname,
            //               g.Key.Email,
            //               gender = (bool?)g.Key.gender,
            //               g.Key.SubjectName,
            //               g.Key.FacultyId,
            //               g.Key.Checked
            //           };


            //foreach (var item in list)
            //{
            //    Debug.WriteLine("ID: " + item.AccountId + " | Fullname: " + item.Fullname);
            //}

            //return (List<AccountAttendance>)list;

            return null;
        }

        public List<Class> SelectClass()
        {
            return context.Classes.ToList();
        }
    }
}
