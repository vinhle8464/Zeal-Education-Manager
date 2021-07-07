using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IBatchesService
    {
        public Task<Batch> Find(string clasdId, string courseId);

        public Task<List<Batch>> FindAll();
        public Task<dynamic> Create(Batch batch);
        public Task<dynamic> CreateClassAssign(string courseId, string classId);
        public Task<dynamic> CreateClassSchedule(string courseId, string classId);
        public Task<dynamic> CreateTestSchedule(string courseId, string classId);

        public Task<dynamic> Update(Batch batch);
        public Task Delete(string courseId, string classId);
        public List<Batch> Search(string searchKeyword, string courseKeyword, string classKeyword);
        public Task<List<string>> GetKeyWord();
        public Task<List<string>> GetKeyWordByKeyword(string keyword);
        //public Task<List<Course>> ListCourse(string keyword);
        public Task<List<string>> ListClass(string keyword);


    }
}
