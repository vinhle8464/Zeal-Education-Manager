using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class TestScheduleServiceImpl : ITestScheduleService
    {
        private DatabaseContext context;

        public TestScheduleServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<dynamic> Add(TestSchedule TestSchedule)
        {
            if (context.TestSchedules.Any(p => p.ClassId.Equals(TestSchedule.ClassId) && p.ExamId == TestSchedule.ExamId))
            {
                return 0;
            }
            else
            {
                context.TestSchedules.Add(TestSchedule);
                await context.SaveChangesAsync();
                return 1;
            }

            
        }

        public async  Task<List<Class>> SelectClasses()
        {
            return await context.Classes.ToListAsync();
        }

        public async Task<List<TestSchedule>> SelectTestShedule(string classid)
        {
            return await context.TestSchedules.Where(s => s.ClassId == classid && s.Status == true).ToListAsync();
        }



        public async Task<Class> GetClass(string classid)
        {
            return await context.Classes.SingleOrDefaultAsync(c => c.ClassId == classid);
        }

        public async Task<Account> GetFaculty(string facultyid)
        {
            return await context.Accounts.SingleOrDefaultAsync(a => a.AccountId == facultyid);
        }



        public async Task CreateMarkBySubject(string ExamId, int maxmark, int rate)
        {
            Thread.Sleep(10);

            var subject = context.Exams.FirstOrDefault(e => e.ExamId == ExamId);

            List<string> CourseId = context.CourseSubjects.Where(m => m.SubjectId == subject.SubjectId).Select(c => c.CourseId).ToList();

            foreach (var courseId in CourseId)
            {
                Batch batch = context.Batches.FirstOrDefault(m => m.CourseId == courseId);

                List<string> listid = context.Accounts.Where(m => m.ClassId == batch.ClassId).Select(a => a.AccountId).ToList();
                foreach (var studentId in listid)
                {

                    context.Marks.Add(new Mark
                    {
                        ExamId = ExamId,
                        StudentId = studentId,
                        Mark1 = 0,
                        MaxMark = maxmark,
                        Rate = (byte?)rate,
                        StatusMark = "grading",
                        Status = true
                    });
                }
            }
            await context.SaveChangesAsync();

        }

        public async Task<TestSchedule> GetDetailTestSchedule(string subjectid, string classid)
        {
            return await context.TestSchedules.SingleOrDefaultAsync(t => t.Exam.SubjectId == subjectid && t.ClassId == classid);
        }
    }
}
