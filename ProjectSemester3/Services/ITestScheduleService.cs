using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface ITestScheduleService
    {
        public Task<List<TestSchedule>> SelectTestShedule(string classid);
        public Task<List<Class>> SelectClasses();
        public Task<Class> GetClass(string classid);
        public Task<dynamic> Add(TestSchedule testSchedule);
        Task CreateMarkBySubject(string ExamId, int maxmark, int rate);

        public Task<Account> GetFaculty(string facultyid);
        public Task<TestSchedule> GetDetailTestSchedule(string examid, string classid);

        public List<Class> Search(string searchClassSchedule);

        public List<Account> GetListFaculty(string exanid);

        public Task<TestSchedule> FindAjax(int testscheduleid);
        public Task Delete(int id);
    }
}
