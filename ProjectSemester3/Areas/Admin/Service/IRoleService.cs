
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IRolesService
    {
        Task<List<Role>> FindAll();
        Task<Role> Find(string RoleId);
        Task<int> CountId();
        Task<dynamic> Create(Role role);
        Task Delete(string RoleId);
        string GetNewestId();
        Task<Role> Update(Role role);
        bool Exists(string RoleId);
        Task<Role> FindAjax(string RoleId);
    }
}
