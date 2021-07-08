using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IMarkService
    {
        public List<Mark> GetMarkByStudentId(string studentid);
        public List<string> CheckStatus(string studentid);
        public List<Mark> GetSubjectFaid(string studentid);
        public List<Mark> GetPass(string studentid);



    }
}
