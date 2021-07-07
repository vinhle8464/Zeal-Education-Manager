
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface ICoursesService
    {
        Task<List<Course>> FindAll();
        Task<Course> Find(string CourseId);
        Task<int> CountId();
        Task<dynamic> Create(Course course);
        Task Delete(string CourseId);
        string GetNewestId();
        Task<Course> Update(Course course);
        bool RoleExists(string CourseId);
        Task<List<string>> GetKeyWordByKeyword(string keyword);
        List<Course> Search(string searchCourse);
        Task<Course> FindAjax(string courseId);
    }
}
