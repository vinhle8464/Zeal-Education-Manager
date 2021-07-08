using Microsoft.EntityFrameworkCore;
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace ProjectSemester3.Areas.Admin.Service
{
    public class ExamsServiceImpl : IExamsService
    {
        private readonly DatabaseContext context;
        public ExamsServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }

        public async Task<int> CountId()
        {
            return await context.Exams.CountAsync();
        }



        public async Task<int> Create(Exam exam)
        {
            if (context.Exams.Any(p => p.Title == exam.Title && p.SubjectId == exam.SubjectId && p.Status == true))
            {
                return 0;
            }
            else
            {
                context.Exams.Add(exam);
                await context.SaveChangesAsync();
                return 1;
            }

        }

        public async Task Delete(string ExamId, string SubjectId)
        {
            var exam = await context.Exams.FirstOrDefaultAsync(p => p.ExamId == ExamId && p.SubjectId == SubjectId && p.Status == true);
            exam.Status = false;
            context.Entry(exam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<Exam> Find(string ExamId, string SubjectId) => await context.Exams.FirstOrDefaultAsync(p => p.ExamId == ExamId && p.SubjectId == SubjectId);

        public async Task<List<Exam>> FindAll() => await context.Exams.OrderByDescending(e => e.SubjectId).Take(10).ToListAsync();

        public string GetNewestId()
        {

            return (from exams in context.Exams

                    orderby
                      exams.ExamId descending
                    select exams.ExamId).Take(1).SingleOrDefault();

        }

        public bool Exists(string ExamId, string SubjectId) => context.Exams.Any(p => p.ExamId == ExamId && p.SubjectId == SubjectId);

        public async Task<Exam> Update(Exam exam)
        {
            context.Entry(exam).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return exam;
        }

        public async Task<Exam> FindAjax(string examId)
        {
            return await context.Exams.FirstOrDefaultAsync(c => c.ExamId == examId && c.Status == true);
        }

        public List<Exam> Search(string searchExam, string filterSubject)
        {
            var exams = context.Exams.AsQueryable();

            if (filterSubject != null) exams = exams.Where(s => s.SubjectId.Contains(filterSubject));

            if (searchExam != null) exams = exams.Where(b => b.Title.Contains(searchExam)); ;

            var result = exams.Where(b => b.Status == true).ToList(); // execute query

            return result;
        }

    }
}
