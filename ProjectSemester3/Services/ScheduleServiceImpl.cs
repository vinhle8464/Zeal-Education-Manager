using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class ScheduleServiceImpl : IScheduleService
    {
        private DatabaseContext context;

        public ScheduleServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public void Add(Schedule schedule)
        {
            context.Entry(schedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
             context.SaveChanges();

        }

        public async Task<List<Class>> SelectClasses()
        {
            return await context.Classes.ToListAsync();
        }

        public async Task<List<Schedule>> SelectShedule(string classid)
        {
            return await context.Schedules.Where(s => s.ClassId == classid && s.Status == true).ToListAsync();
        }

        //public string GetNewestId()
        //{
        //    return (from schedule in context.Schedules
        //            where
        //             schedule.Status == true
        //            orderby
        //              schedule.ScheduleId descending
        //            select schedule.ScheduleId).Take(1).SingleOrDefault();
        //}

        public async Task<Class> GetClass(string classid)
        {
            return await context.Classes.SingleOrDefaultAsync(c => c.ClassId == classid && c.Status == true);
        }
        //public async Task<List<Subject>> GetListSubject(string classid)
        //{
        //    return null;
        //}

        public async Task<Account> GetFaculty(string facultyid)
        {
            return await context.Accounts.SingleOrDefaultAsync(a => a.AccountId == facultyid);
        }

        public async Task<Schedule> GetBySubjectId(string subjectid, string classid)
        {
            return await context.Schedules.SingleOrDefaultAsync(s => s.SubjectId == subjectid && s.ClassId == classid);
        }

        public async Task CreateAttendance(Schedule schedule)
        {
            var listStudentId = context.Accounts.Where(a => a.ClassId == schedule.ClassId).Select(p => p.AccountId).ToList();

            var listDate = new List<DateTime>();
            string[] studyday = schedule.StudyDay.Split(',');
            while (schedule.EndDate >= schedule.StartDate)
            {
                foreach (var day in studyday)
                {
                    if (schedule.StartDate.ToString("dddd") == day)
                    {
                        listDate.Add(schedule.StartDate);
                    }
                }
                schedule.StartDate = schedule.StartDate.AddDays(1);
            }

            foreach (var studentId in listStudentId)
            {
                foreach (var date in listDate)
                {
                    context.Attendances.Add(new Attendance
                    {
                        ClassId = schedule.ClassId,
                        StudentId = studentId,
                        FacultyId = schedule.FacultyId,
                        SubjectId = schedule.SubjectId,
                        Date = date,
                        Checked = false

                    });
                }
            }

            await context.SaveChangesAsync();
        }

        public List<Class> Search(string searchClassSchudule)
        {
            var Classes = context.Classes.AsQueryable();

            if (searchClassSchudule != null) Classes = Classes.Where(s => s.ClassName.Contains(searchClassSchudule));

            var result = Classes.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }

        public List<Account> GetListFaculty(string subjectId)
        {
            var listFaculty = new List<Account>();
            var listFacultyid = context.Professionals.Where(p => p.SubjectId == subjectId).Select(p => p.FacultyId).ToList();

            foreach (var item in listFacultyid)
            {
                var obj = context.Accounts.FirstOrDefault(a => a.AccountId == item);
                listFaculty.Add(obj);
            }

            return listFaculty;

        }

        public async Task<Schedule> FindAjax(int scheduleid)
        {
            return await context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == scheduleid);
        }

        public async Task Delete(int id)
        {
            var obj = await context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == id);
            obj.Status = false;
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
