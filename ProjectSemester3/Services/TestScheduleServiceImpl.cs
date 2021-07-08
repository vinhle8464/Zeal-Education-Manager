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
            context.Entry(TestSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return await context.SaveChangesAsync();


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

        public async Task<TestSchedule> GetDetailTestSchedule(string examid, string classid)
        {
            return await context.TestSchedules.SingleOrDefaultAsync(t => t.Exam.ExamId == examid && t.ClassId == classid);
        }


        public  List<Class> Search(string searchClassSchudule)
        {
            var Classes = context.Classes.AsQueryable();

            if (searchClassSchudule != null) Classes = Classes.Where(s => s.ClassName.Contains(searchClassSchudule));

            var result = Classes.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }

        public List<Account> GetListFaculty(string examid)
        {
            var listFaculty = new List<Account>();
            var subjectId = context.Exams.FirstOrDefault(e => e.ExamId == examid);
            var listFacultyid = context.Professionals.Where(p => p.SubjectId == subjectId.SubjectId).Select(p => p.FacultyId).ToList();

            foreach (var item in listFacultyid)
            {
                var obj = context.Accounts.FirstOrDefault(a => a.AccountId == item);
                listFaculty.Add(obj);
            }

            return listFaculty;

        }

        public async Task<TestSchedule> FindAjax(int testscheduleid)
        {
            return await context.TestSchedules.FirstOrDefaultAsync(s => s.TestScheduleId == testscheduleid);
        }

        public async Task Delete(int id)
        {
            var obj = await context.TestSchedules.FirstOrDefaultAsync(s => s.TestScheduleId == id);
            obj.Status = false;
            context.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
