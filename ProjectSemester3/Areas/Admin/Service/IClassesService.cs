using ProjectSemester3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IClassesService
    {
        Task<List<Class>> FindAll();
        Task<Class> Find(string ClassId);
        Task<int> CountId();
       // Task<int> CountIdById(string ClassId);
        Task<dynamic> Create(Class classes);
        Task Delete(string classId);
        string GetNewestId();
        Task<Class> Update(Class classes);
        bool RoleExists(string ClassId);
        public Task<Class> FindAjax(string classId);
        public List<Class> Search(string searchCLass, int filterNumber);

    }
}
