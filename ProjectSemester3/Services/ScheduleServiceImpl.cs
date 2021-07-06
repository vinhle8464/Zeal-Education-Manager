﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<dynamic> Add(Schedule schedule)
        {
            if (context.Schedules.Any(p => p.ClassId == schedule.ClassId && p.SubjectId == schedule.SubjectId))
            {
                return 0;
            }
            else
            {
                context.Schedules.Add(schedule);
                await context.SaveChangesAsync();
                return 1;
            }
           
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
        public async Task<List<Subject>> GetListSubject(string classid)
        {
            return null;
        }

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
    }
}