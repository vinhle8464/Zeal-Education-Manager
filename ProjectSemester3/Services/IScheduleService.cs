using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectSemester3.Services
{
    public interface IScheduleService
    {
        public Task<List<Schedule>> SelectShedule(string classid);
        public Task<List<Class>> SelectClasses();
        public Task<Class> GetClass(string classid);
    //    public Task<List<Subject>> GetListSubject(string classid);

        public void Add(Schedule schedule);
        public Task CreateAttendance(Schedule schedule);
      //  public string GetNewestId();
        public Task<Account> GetFaculty(string facultyid);
        public Task<Schedule> GetBySubjectId(string subjectid, string classid);
        public List<Class> Search(string searchClassSchedule);

        public List<Account> GetListFaculty(string subjectId);

        public Task<Schedule> FindAjax(int scheduleid);

        public Task Delete(int id);
    }
}
