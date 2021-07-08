using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IExamsService
    {
        Task<List<Exam>> FindAll();
        Task<Exam> Find(string ExamId, string SubjectId);
        Task<int> CountId();
        Task<int> Create(Exam exam);
        Task Delete(string ExamId, string SubjectId);
        string GetNewestId();
        Task<Exam> Update(Exam exam);
        bool Exists(string ExamId, string SubjectId);

        public Task<Exam> FindAjax(string examId);
        public List<Exam> Search(string searchExam, string filterSubject);
    }
}
