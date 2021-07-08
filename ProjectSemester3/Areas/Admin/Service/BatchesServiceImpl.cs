using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public class BatchesServiceImpl : IBatchesService
    {
        private readonly DatabaseContext context;

        public BatchesServiceImpl(DatabaseContext context)
        {
            this.context = context;
        }

        public async Task<dynamic> Create(Batch batch)
        {
            if (context.Batches.Any(p => p.ClassId.Equals(batch.ClassId) && p.CourseId.Equals(batch.CourseId) && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.Batches.Add(batch);
                await context.SaveChangesAsync();
                return 1;
            }
        }

        public async Task<dynamic> CreateClassAssign(string courseId, string classId)
        {
            List<Subject> listSubject = await context.CourseSubjects.Where(c => c.CourseId == courseId).Select(c => c.Subject).ToListAsync();


            foreach (var item in listSubject)
            {
                var classAssign = new ClassAssignment
                {
                    FacultyId = null,
                    ClassId = classId,
                    Status = false
                };
                classAssign.SubjectName = item.SubjectName;
                context.ClassAssignments.Add(classAssign);
            }

            return await context.SaveChangesAsync();
        }

        public async Task<dynamic> CreateClassSchedule(string courseId, string classId)
        {
            var listSubject = await context.CourseSubjects.Where(c => c.CourseId == courseId).Select(c => c.Subject).ToListAsync();


            foreach (var item in listSubject)
            {
                var schedule = new Schedule
                {
                    ClassId = classId,
                    SubjectId = item.SubjectId,
                    StartDate = DateTime.Parse("1980/12/12"),
                    EndDate = DateTime.Parse("1980/12/12"),
                    Status = true

                };
                context.Schedules.Add(schedule);
            }

            return await context.SaveChangesAsync();
        }

        public async Task<dynamic> CreateTestSchedule(string courseId, string classId)
        {
            var listExamId = new List<string>();
            var listSubject = await context.CourseSubjects.Where(c => c.CourseId == courseId).Select(c => c.Subject).ToListAsync();
            foreach (var item in listSubject)
            {
                var obj = await context.Exams.Where(e => e.SubjectId == item.SubjectId && e.Status == true).Select(e => e.ExamId).ToListAsync();
                if(obj != null)
                {
                    foreach(var item1 in obj)
                    {
                        listExamId.Add(item1);
                    }

                }
            }
            foreach (var item in listExamId)
            {
                var testSchedule = new TestSchedule
                {
                    ClassId = classId,
                    ExamId = item,
                    Date = DateTime.Parse("1980/12/12"),
                    Status = true

                };
                context.TestSchedules.Add(testSchedule);
            }
            return await context.SaveChangesAsync();

        }

        public async Task Delete(string courseId, string classId)
        {
            var batch = await context.Batches.FirstOrDefaultAsync(b => b.CourseId == courseId && b.ClassId == classId && b.Status == true);
            batch.Status = false;
            context.Entry(batch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Batch> Find(string clasdId, string courseId) => await context.Batches.FirstOrDefaultAsync(b => b.ClassId == clasdId && b.CourseId == courseId && b.Status == true);


        public async Task<List<Batch>> FindAll() => await context.Batches
            .Where(b => b.Status == true)
            .OrderByDescending(b => b.StartDate).ToListAsync();

        public async Task<List<string>> GetKeyWord() => await context.Courses
            .Where(c => c.Status == true)
            .Select(c => c.CourseName).ToListAsync();

        public async Task<List<string>> GetKeyWordByKeyword(string keyword)
        {
            var list = new List<string>();

            var listCourse = await context.Courses
                .Where(c => c.CourseName.ToLower().Contains(keyword.ToLower()) && c.Status == true)
                .Select(c => c.CourseName).ToListAsync();
            foreach (var item in listCourse)
            {
                list.Add(item);
            }

            var listClass = await context.Classes
                .Where(c => c.ClassName.ToLower().Contains(keyword.ToLower()) && c.Status == true)
                .Select(c => c.ClassName).ToListAsync();
            foreach (var item in listClass)
            {
                list.Add(item);
            }

            return list;
        }

        public async Task<List<string>> ListClass(string keyword)
        {

            var result = await context.Classes.Where(c => c.ClassName.ToLower().Contains(keyword.ToLower()) && c.Status == true)
           .Select(c => c.ClassName).ToListAsync();

            var listInBatch = await context.Batches.Where(b => b.Status == true).Select(b => b.ClassId).ToListAsync();

            var listClassName = new List<string>();
            foreach (var item in listInBatch)
            {
                var a = await context.Classes.FirstOrDefaultAsync(c => c.ClassId == item && c.Status == true);
                if(a != null)
                {
                    listClassName.Add(a.ClassName);
                }
            }

            foreach (var item in listClassName)
            {
                result.Remove(item);
            }

            return result;

        }

        //public async Task<List<Course>> ListCourse(string keyword) => await context.Courses
        //    .Where(c => c.CourseName.ToLower().Contains(keyword.ToLower()) && c.Status == true)
        //    .ToListAsync();

        public List<Batch> Search(string searchKeyword, string courseKeyword, string classKeyword)
        {
            var batches = context.Batches.AsQueryable();
            if (courseKeyword != null) batches = batches.Where(s => s.Course.CourseName.Contains(courseKeyword));
            if (classKeyword != null) batches = batches.Where(s => s.Graduate == Boolean.Parse(classKeyword));

            if (searchKeyword != null) batches = batches.Where(b => b.Class.ClassName.Contains(searchKeyword) || b.Course.CourseName.Contains(searchKeyword));
           
            var result = batches.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }

        public async Task<dynamic> Update(Batch batch)
        {
            context.Entry(batch).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return batch;
        }


    }
}
