using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public class MarkServiceImpl : IMarkService
    {
        private DatabaseContext context;

        public MarkServiceImpl(DatabaseContext _context)
        {
            context = _context;
        }
        public List<Mark> GetMarkByStudentId(string studentid)
        {
            return context.Marks.Where(m => m.StudentId == studentid).ToList();
        }

        public List<string> CheckStatus(string studentid)
        {
            var marks = context.Marks.Where(m => m.StudentId == studentid).ToList();
            var listRank = new List<string>();
            foreach (var mark in marks)
            {
                var total = (mark.Mark1 / mark.MaxMark) * 100;
                if (total >= mark.Rate && mark.Mark1 >=0 && mark.StatusMark == "pass")
                {
                    listRank.Add(mark.Exam.Title + ":    PASSED    |    Mark:    "+ mark.Mark1);
                }
                else if(mark.StatusMark == "fail")
                {
                    listRank.Add(mark.Exam.Title + ":    FAILED    |    Mark:    " + mark.Mark1);
                }

            }
            return listRank;
        }

        public List<Mark> GetSubjectFaid(string studentid)
        {
            return context.Marks.Where(m => m.StatusMark == "fail" && m.StudentId == studentid).ToList();
        }

        public List<Mark> GetPass(string studentid)
        {
            return context.Marks.Where(m => m.StatusMark == "pass" && m.StudentId == studentid).ToList();

        }
    }
}
