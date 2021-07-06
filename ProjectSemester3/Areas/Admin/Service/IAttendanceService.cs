using ProjectSemester3.Areas.Admin.Entities;
using ProjectSemester3.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectSemester3.Areas.Admin.Service
{
    public interface IAttendanceService
    {
        List<AccountAttendance> SelectAll(string classid);
        List<Class> SelectClass();
    }
}
